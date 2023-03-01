import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'NotesApp';
  activeTab = 4;

  constructor(public authService: AuthService) {

  }

  logout(){
    this.authService.logout();
    this.activeTab = 4;
  }
}