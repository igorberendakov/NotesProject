import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'notification-dialog',
    templateUrl: './notification.dialog.html',
    styleUrls: ['./notification.dialog.scss', '../../../common-component-styles.scss']
})
export class NotificationDialog {
    notificationData: any = {};
    data_loaded: Promise<boolean> | undefined;

    constructor(
        @Inject(MAT_DIALOG_DATA) private data: any,
        private dialogRef: MatDialogRef<NotificationDialog>) {
    }

    close() {
        this.dialogRef.close();
    }

    confirm() {
        return this.dialogRef.close(this.notificationData);
    }
}