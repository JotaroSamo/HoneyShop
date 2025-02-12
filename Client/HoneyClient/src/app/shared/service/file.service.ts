import { Injectable } from '@angular/core';
import { environment } from '../../constant/enviroment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  private fileApiUrl = environment.apiUrl + 'File';
  constructor(private http: HttpClient) {}

   // Загрузка файлов
   uploadFiles(files: FileList): Observable<number[]> {
    const formData = new FormData();
    for (let i = 0; i < files.length; i++) {
      formData.append('files', files[i]);
    }
    return this.http.post<number[]>(`${this.fileApiUrl}/upload`, formData);
  }

  // Получение файла по ID
  getFileById(id: number): Observable<Blob> {
    return this.http.get(`${this.fileApiUrl}/get/${id}`, { responseType: 'blob' });
  }

  // Удаление файла
  deleteFile(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.fileApiUrl}/delete/${id}`);
  }
}
