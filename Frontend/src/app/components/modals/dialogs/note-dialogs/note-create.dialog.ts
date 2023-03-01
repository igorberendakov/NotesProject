import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DataHandlerService } from 'src/app/services/data-handler.service';

@Component({
    selector: 'note-create-dialog',
    templateUrl: './note-create.dialog.html',
    styleUrls: ['./note-create.dialog.scss', '../../../common-component-styles.scss']
})
export class NoteCreateDialog implements OnInit {
    noteCreateData: any = {};
    tags: any[] = [];
    data_loaded: Promise<boolean> | undefined;
    tagGuids: string[] = [];

    constructor(
        private dataHandler: DataHandlerService,
        @Inject(MAT_DIALOG_DATA) private data: any,
        private dialogRef: MatDialogRef<NoteCreateDialog>) {
    }

    ngOnInit(): void {
        this.dataHandler.getTags()
            .subscribe({
                next: (res) => {
                    this.tags = res as []
                },
                error: (err) => console.error(err),
                complete: () => this.data_loaded = Promise.resolve(true)
            });
    }

    close() {
        this.dialogRef.close();
    }

    createNote() {
        return this.dialogRef.close(this.noteCreateData);
    }

    eventCheck(event: any, id: string) {
        if (event.target.checked) {
            this.addTag(id)
        }
        else {
            this.removeTag(id)
        }
    }

    private addTag(id: string) {
        this.tagGuids.push(id);
        this.noteCreateData.tagGuids = this.tagGuids;
    }

    private removeTag(id: string) {
        this.noteCreateData.tagGuids
            .forEach((value: string, index: any) => {
                if (value == id) this.noteCreateData.tagGuids.splice(index, 1);
            });
    }
}