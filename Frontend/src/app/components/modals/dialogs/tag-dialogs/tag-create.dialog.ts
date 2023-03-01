import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'tag-create-dialog',
    templateUrl: './tag-create.dialog.html',
    styleUrls: ['./tag-create.dialog.scss', '../../../common-component-styles.scss']
})
export class TagCreateDialog {
    tagCreateData: any = {};
    data_loaded: Promise<boolean> | undefined;

    constructor(
        @Inject(MAT_DIALOG_DATA) private data: any,
        private dialogRef: MatDialogRef<TagCreateDialog>) {
    }

    close() {
        this.dialogRef.close();
    }

    createTag() {
        return this.dialogRef.close(this.tagCreateData);
    }
}