import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AuthenticationComponent } from './components/authentication-component/authentication.component';
import { NoteComponent } from './components/note-component/note.component';
import { NoteCreateDialog } from './components/modals/dialogs/note-dialogs/note-create.dialog';
import { NoteUpdateDialog } from './components/modals/dialogs/note-dialogs/note-update.dialog';
import { TagComponent } from './components/tag-component/tag.component';
import { TagCreateDialog } from './components/modals/dialogs/tag-dialogs/tag-create.dialog';
import { TagUpdateDialog } from './components/modals/dialogs/tag-dialogs/tag-update.dialog';
import { NotificationComponent } from './components/notification-component/notification.component';

import { AuthService } from './services/auth.service';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { NotificationDialog } from './components/modals/dialogs/notification-dialogs/notification.dialog';



@NgModule({
  declarations: [
    AppComponent,
    AuthenticationComponent,
    NoteComponent,
    NoteCreateDialog,
    NoteUpdateDialog,
    TagComponent,
    TagCreateDialog,
    TagUpdateDialog,
    NotificationComponent,
    NotificationDialog
  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    HttpClientModule,
    MatDialogModule
  ],
  providers: [AuthService, {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
