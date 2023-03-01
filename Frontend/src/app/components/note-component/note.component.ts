import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { DataHandlerService } from "src/app/services/data-handler.service";
import { NoteCreateDialog } from "../modals/dialogs/note-dialogs/note-create.dialog";
import { NoteUpdateDialog } from "../modals/dialogs/note-dialogs/note-update.dialog";
import { NotificationDialog } from "../modals/dialogs/notification-dialogs/notification.dialog";
@Component({
    selector: 'app-note',
    templateUrl: './note.component.html',
    styleUrls: ['./note.component.scss', '../common-component-styles.scss']
})
export class NoteComponent implements OnInit {
    notes: any[] = [];
    dataLoaded: Promise<boolean> | undefined;
    error: any = undefined;

    constructor(private dataHandler: DataHandlerService,
        private dialog: MatDialog) {

    }

    ngOnInit() {
        this.getNotes();
    }

    addNote() {
        const dialogRef = this.dialog.open(NoteCreateDialog);

        dialogRef.afterClosed().subscribe((res) => {
            if (res) {
                this.dataHandler.createNote(res)
                    .subscribe({
                        error: () => this.error = this.error = 'Ошибка при создании заметки',
                        complete: () => {
                            this.getNotes();
                        }
                    });
            }
        })
    }

    updateNote(note: any) {
        const dialogRef = this.dialog.open(NoteUpdateDialog, {
            data: { note: Object.assign({}, note) }
        });

        dialogRef.afterClosed().subscribe((res) => {
            if (res) {
                this.dataHandler.updateNote(res)
                    .subscribe({
                        error: () => this.error = 'Ошибка при изменении заметки',
                        complete: () => {
                            this.getNotes();
                        }
                    });
            }
        })
    }

    deleteNote(id: string) {
        this.dataHandler.deleteNote(id)
            .subscribe({
                error: () => this.error = 'Ошибка при удалении заметки',
                complete: () => this.getNotes()
            });
    }

    addNotificationToNote(noteId: string) {
        const dialogRef = this.dialog.open(NotificationDialog);
        dialogRef.afterClosed().subscribe((res) => {
            if (res) {
                res.noteId = noteId;
                this.dataHandler.createNotification(res)
                    .subscribe({
                        next: (res) => console.info(res),
                        error: (err) => console.error(err),
                        complete: () => {
                            console.info('complete');
                        }
                    });
            }
        })
    }

    clearErrors() {
        this.error = undefined
    }

    private getNotes() {
        this.dataHandler.getNotes()
            .subscribe({
                next: (res) => {
                    this.notes = res as [];
                },
                error: () => this.error = 'Ошибка при загрузке заметок',
                complete: () => {
                    this.dataLoaded = Promise.resolve(true)
                    this.error = undefined;
                }
            });
    }
}