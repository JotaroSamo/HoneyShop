import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductService } from '../../../shared/service/product.service';
import { FileService } from '../../../shared/service/file.service';
import { ProductStatusEnum, ProductStatusEnumLabels } from '../../../data/enum/ProductStatusEnum';
import { CreateProduct } from '../../../data/interface/product/Product';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-create-product',
  imports: [CommonModule,ReactiveFormsModule,MatInputModule, 
    MatButtonModule, MatCardModule,  
    MatSelectModule, MatIcon],
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss']
})
export class CreateProductComponent {
  productForm: FormGroup;
  uploadedFileIds: number[] = [];
  selectedFiles: File[] = []; // Храним выбранные файлы как массив
  fileUrls: string[] = []; // Храним URL для превью изображений
  fileUploadError: string | null = null;

  // В компоненте
  statuses = Object.values(ProductStatusEnum).filter(value => typeof value === 'number');
  statusLabels = ProductStatusEnumLabels; // Пары чисел и текстов


  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private fileService: FileService
  ) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', Validators.maxLength(100)],
      price: [0, [Validators.required, Validators.min(0), Validators.max(1000000000)]],
      status: [ProductStatusEnum.InStock, Validators.required],
      files: [[]]
    });
  }

  onFileSelected(event: any): void {
    const files: FileList = event.target.files;
    const fileArray: File[] = Array.from(files);  // Преобразуем FileList в File[]
  
    if (fileArray.length > 0) {
      this.selectedFiles = []; // Очищаем старые файлы
      this.fileUrls = []; // Очищаем старые URL
  
      for (let i = 0; i < fileArray.length; i++) {
        const file = fileArray[i];
        if (file.type.startsWith('image/')) { // Проверка на изображение
          this.selectedFiles.push(file); // Добавляем только изображение
          const reader = new FileReader();
          reader.onload = (e: any) => {
            this.fileUrls.push(e.target.result); // Создаем URL для отображения
          };
          reader.readAsDataURL(file);
        } else {
          this.fileUploadError = 'Можно загружать только изображения!';
        }
      }
  
      // Преобразование File[] в FileList
      const fileList = this.createFileListFromArray(this.selectedFiles);
  
      if (fileList.length > 0) {
        this.fileService.uploadFiles(fileList).subscribe({
          next: (fileIds) => {
            this.uploadedFileIds.push(...fileIds);
            this.productForm.patchValue({ files: this.uploadedFileIds });
            this.fileUploadError = null;
          },
          error: (err) => {
            console.error('Ошибка загрузки файлов:', err);
            this.fileUploadError = 'Ошибка загрузки файлов';
          }
        });
      }
    }
  }
  
  // Хак для создания FileList из File[]
  createFileListFromArray(files: File[]): FileList {
    const dataTransfer = new DataTransfer();
    files.forEach(file => {
      dataTransfer.items.add(file);
    });
    return dataTransfer.files;  // Возвращаем FileList
  }
  

  removeFile(index: number): void {
    this.selectedFiles.splice(index, 1); // Удаляем файл из списка
    this.fileUrls.splice(index, 1); // Удаляем URL изображения
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const productData: CreateProduct = this.productForm.value;
      this.productService.createProduct(productData).subscribe({
        next: (response) => {
          console.log('Продукт создан:', response);
          this.selectedFiles = [];
          this.productForm.reset();
        },
        error: (err) => {
          console.error('Ошибка создания продукта:', err);
        }
      });
    }
  }
}
