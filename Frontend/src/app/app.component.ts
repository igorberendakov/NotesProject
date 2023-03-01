import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'NotesApp';
  activeTab = 4;

  constructor(public authService: AuthService) {

  }

  ngOnInit(): void {
    if (this.authService.loggedIn()) {
      this.activeTab = 1
    }
    else {
      this.activeTab = 4
    }
  }

  logout() {
    this.authService.logout();
    this.activeTab = 4;
  }
}