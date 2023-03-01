import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss', '../common-component-styles.scss']
})
export class AuthenticationComponent {
  userLoginData: any = {}

  constructor(private authService: AuthService) {
  }

  registerUser() {
    this.authService.registerUser(this.userLoginData)
      .subscribe({
        next: (res) => {
          localStorage.setItem("token", res.token)
        },
        error: (err) => console.error(err)
      })
  }

  authenticateUser() {
    this.authService.authenticateUser(this.userLoginData)
      .subscribe({
        next: (res) => {
          localStorage.setItem("token", res.token)
        },
        error: (err) => console.error(err)
      })
  }
}
