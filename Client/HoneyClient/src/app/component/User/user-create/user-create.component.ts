import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatSnackBar } from '@angular/material/snack-bar'; // Для отображения уведомлений
import { UserService } from '../../../data/service/user.service';
import { CreateUser } from '../../../data/interface/user/User';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatFormField } from '@angular/material/input';

@Component({
  selector: 'app-user-create',
   imports: [CommonModule, ReactiveFormsModule, MatInputModule, MatButtonModule, MatCardModule, MatIconModule, MatFormField],
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.scss'],
})
export class UserCreateComponent {
  registrationForm: FormGroup; // Форма для регистрации
  hidePassword = true; // Скрытие пароля
  hideConfirmPassword = true; // Скрытие подтверждения пароля

  constructor(
    private fb: FormBuilder, // Для создания формы
    private userService: UserService, // Сервис для работы с API
    private snackBar: MatSnackBar // Для уведомлений
  ) {
    // Инициализация формы
    this.registrationForm = this.fb.group(
      {
        username: ['', [Validators.required, Validators.minLength(3)]],
        fullName: ['', [Validators.required]],
        phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[0-9]{10,15}$/)]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required]],
      },
      {
        validators: this.passwordMatchValidator, // Кастомная валидация для проверки совпадения паролей
      }
    );
  }

  // Кастомная валидация для проверки совпадения паролей
  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  // Обработка отправки формы
  onSubmit() {
    if (this.registrationForm.invalid) {
      this.snackBar.open('Пожалуйста, заполните все поля корректно.', 'Закрыть', {
        duration: 3000,
      });
      return;
    }

    const userData: CreateUser = this.registrationForm.value;

    // Вызов сервиса для регистрации пользователя
    this.userService.createUser(userData).subscribe({
      next: (response: any) => {
        this.snackBar.open('Регистрация прошла успешно!', 'Закрыть', {
          duration: 3000,
        });
        this.registrationForm.reset(); // Очистка формы после успешной регистрации
      },
      error: (err: any) => {
        this.snackBar.open('Ошибка при регистрации. Пожалуйста, попробуйте снова.', 'Закрыть', {
          duration: 3000,
        });
        console.error('Registration error:', err);
      },
    });
  }
}