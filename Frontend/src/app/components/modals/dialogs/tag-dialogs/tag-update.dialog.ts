import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DataHandlerService } from 'src/app/services/data-handler.service';
@Component({
    selector: 'tag-create-dialog',
    templateUrl: './tag-update.dialog.html',
    styleUrls: ['./tag-update.dialog.scss', '../../../common-component-styles.scss']
})
export class TagUpdateDialog {
    tagUpdateData: any = {};
    data_loaded: Promise<boolean> | undefined;

    constructor(
        private dataHandler: DataHandlerService,
        @Inject(MAT_DIALOG_DATA) private data: { tag: any },
        private dialogRef: MatDialogRef<TagUpdateDialog>) {
        this.tagUpdateData = data.tag;
    }

    close() {
        this.dialogRef.close();
    }

    updateTag() {
        return this.dialogRef.close(this.tagUpdateData);
    }
}