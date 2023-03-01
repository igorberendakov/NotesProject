import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss', '../common-component-styles.scss']
})
export class AuthenticationComponent {
  userLoginData: any = {};
  error: any;

  constructor(private authService: AuthService) {
  }

  registerUser() {
    this.authService.registerUser(this.userLoginData)
      .subscribe({
        next: (res) => {
          localStorage.setItem("token", res.token)
        },
        error: (err) => {
          if (err.error.message) {
            this.error = err.error.message
          }
          else {
            this.error = "Ошибка при регистрации. Попробуйте позже."
          }
        }
      })
  }

  authenticateUser() {
    this.authService.authenticateUser(this.userLoginData)
      .subscribe({
        next: (res) => {
          localStorage.setItem("token", res.token)
        },
        error: (err) => {
          console.log(err);
          if (err.error.message) {
            this.error = err.error.message
          }
          else {
            this.error = "Ошибка при входе. Попробуйте позже."
          }
        }
      })
  }
}
