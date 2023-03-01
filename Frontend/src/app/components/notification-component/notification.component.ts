import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { DataHandlerService } from "src/app/services/data-handler.service";
import { NotificationDialog } from "../modals/dialogs/notification-dialogs/notification.dialog";
@Component({
    selector: 'app-notification',
    templateUrl: './notification.component.html',
    styleUrls: ['./notification.component.scss', '../common-component-styles.scss']
})
export class NotificationComponent implements OnInit {
    notifications: any[] = [];
    dataLoaded: Promise<boolean> | undefined;
    error: any = undefined;

    constructor(private dataHandler: DataHandlerService,
        private dialog: MatDialog) {

    }

    ngOnInit() {
        this.getNotifications();
    }

    updateNotification(notificationId: any) {
        const dialogRef = this.dialog.open(NotificationDialog);
        dialogRef.afterClosed().subscribe((res) => {
            if (res) {
                res.id = notificationId;
                this.dataHandler.updateNotification({ id: notificationId, timeBinding: res.timeBinding })
                    .subscribe({
                        error: () => this.error = 'Ошибка при изменении уведомления',
                        complete: () => {
                            this.getNotifications();
                        }
                    });
            }
        })
    }

    deleteNotification(id: string) {
        this.dataHandler.deleteNotification(id)
            .subscribe({
                error: () => this.error = 'Ошибка при удалении заметки',
                complete: () => this.getNotifications()
            });
    }

    clearErrors() {
        this.error = undefined
    }

    private getNotifications() {
        this.dataHandler.getNotifications()
            .subscribe({
                next: (res) => {
                    this.notifications = res as [];
                    this.dataLoaded = Promise.resolve(true);
                    this.dateTimeToLocal();
                },
                error: () => this.error = 'Ошибка при загрузке уведомлений',
                complete: () => {
                    this.dataLoaded = Promise.resolve(true)
                    this.error = undefined;
                }
            });
    }

    private dateTimeToLocal() {
        var browserLanguage = navigator.language;
        this.notifications.forEach(not => {
            not.date = new Date(not.timeBinding).toLocaleDateString(browserLanguage);
            not.time = new Date(not.timeBinding).toLocaleTimeString(browserLanguage);
        })
    }
}