  import { Component } from '@angular/core';
  import { AuthService } from '../../../services/auth.service';
  import { Router } from '@angular/router';
  import { MatSnackBar } from '@angular/material/snack-bar';

  @Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
  })
  export class RegisterComponent {
    user = {
      full_name: '',
      username: '',
      email: '',
      password: '',
      phone: ''
    };

    constructor(
      private authService: AuthService,
      private router: Router,
      private snackBar: MatSnackBar
    ) {}

    register() {
      console.log('Attempting registration with data:', this.user);
      this.authService.register(this.user).subscribe({
        next: (res) => {
          console.log('Registration response:', res);
          if (res) {
            this.snackBar.open('Registration successful!', 'Close', {
              duration: 3000,
              panelClass: ['snackbar-success']
            });
            this.router.navigate(['/login']);
          } else {
            console.warn('Empty response from server');
            this.snackBar.open('Registration completed. Please check your email to verify your account.', 'Close', {
              duration: 5000,
              panelClass: ['snackbar-info']
            });
          }
        },
        error: (err) => {
          console.error('Registration error:', err);
          let errorMessage = 'Registration failed. Please try again.';
          if (err.error?.error === 'Email already exists') {
            errorMessage = 'This email is already registered. Please use a different email.';
          }
          if (err.error?.message) {
            errorMessage = err.error.message;
          } else if (err.status === 0) {
            errorMessage = 'Unable to connect to server. Please check your internet connection.';
          }
          this.snackBar.open(errorMessage, 'Close', {
            duration: 5000,
            panelClass: ['snackbar-error']
          });
        }
      });
    }
  }
