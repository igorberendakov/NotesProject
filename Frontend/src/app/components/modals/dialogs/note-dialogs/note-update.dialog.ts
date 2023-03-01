import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DataHandlerService } from 'src/app/services/data-handler.service';
@Component({
    selector: 'note-create-dialog',
    templateUrl: './note-update.dialog.html',
    styleUrls: ['./note-update.dialog.scss', '../../../common-component-styles.scss']
})
export class NoteUpdateDialog implements OnInit {
    noteUpdateData: any = {};
    allTags: any[] = [];
    data_loaded: Promise<boolean> | undefined;
    tagGuids: string[] | undefined = [];
    note: any;

    constructor(
        private dataHandler: DataHandlerService,
        @Inject(MAT_DIALOG_DATA) private data: { note: any },
        private dialogRef: MatDialogRef<NoteUpdateDialog>) {
        this.note = data.note;
        this.tagGuids = this.note.tags?.map((x: any) => x.id);
    }

    ngOnInit(): void {
        this.dataHandler.getTags()
            .subscribe({
                next: (res) => {
                    this.allTags = res as []
                    this.allTags.forEach((tag) => {
                        if (this.tagGuids && this.tagGuids.includes(tag.id)) {
                            tag.select = true;
                        }
                        else {
                            tag.select = false;
                        }
                    })
                },
                error: (err) => console.error(err),
                complete: () => this.data_loaded = Promise.resolve(true)
            });

    }

    close() {
        this.dialogRef.close();
    }

    updateNote() {
        this.noteUpdateData.id = this.note.id;
        this.noteUpdateData.text = this.note.text;
        this.noteUpdateData.title = this.note.title;
        return this.dialogRef.close(this.noteUpdateData);
    }

    eventCheck(event: any, id: string) {
        if (event.target.checked) {
            this.addTagToNote(this.note.id, id);
        }
        else {
            this.removeTagFromNote(this.note.id, id);
        }
    }

    private addTagToNote(noteId: string, tagId: string) {
        let noteTag = { noteId, tagId }
        this.dataHandler.addTagToNote(noteTag)
            .subscribe({
                next: (res) => { console.info(res) },
                error: (err) => console.error(err),
            });
    }

    private removeTagFromNote(noteId: string, tagId: string) {
        let noteTag = { noteId, tagId }
        this.dataHandler.removeTagFromNote(noteTag)
            .subscribe({
                next: (res) => { console.info(res) },
                error: (err) => console.error(err),
            });
    }
}