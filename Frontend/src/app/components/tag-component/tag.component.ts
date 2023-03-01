import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { DataHandlerService } from "src/app/services/data-handler.service";
import { TagCreateDialog } from "../modals/dialogs/tag-dialogs/tag-create.dialog";
import { TagUpdateDialog } from "../modals/dialogs/tag-dialogs/tag-update.dialog";
@Component({
    selector: 'app-tag',
    templateUrl: './tag.component.html',
    styleUrls: ['./tag.component.scss', '../common-component-styles.scss']
})
export class TagComponent implements OnInit {
    tags: any[] = [];
    dataLoaded: Promise<boolean> | undefined;
    error: any = undefined;

    constructor(private dataHandler: DataHandlerService,
        private dialog: MatDialog) {

    }

    ngOnInit() {
        this.getTags();
    }

    addTag() {
        const dialogRef = this.dialog.open(TagCreateDialog);

        dialogRef.afterClosed().subscribe((res) => {
            if (res) {
                this.dataHandler.createTag(res)
                    .subscribe({
                        error: () => this.error = this.error = 'Ошибка при создании тэга',
                        complete: () => {
                            this.getTags();
                        }
                    });
            }
        })
    }

    updateTag(tag: any) {
        const dialogRef = this.dialog.open(TagUpdateDialog, {
            data: { tag: Object.assign({}, tag) }
        });

        dialogRef.afterClosed().subscribe((res) => {
            if (res) {
                this.dataHandler.updateTag(res)
                    .subscribe({
                        error: () => this.error = 'Ошибка при изменении тэга',
                        complete: () => {
                            this.getTags();
                        }
                    });
            }
        })
    }

    deleteTag(id: string) {
        this.dataHandler.deleteTag(id)
            .subscribe({
                error: () => this.error = 'Ошибка при удалении тэга',
                complete: () => this.getTags()
            });
    }

    clearErrors() {
        this.error = undefined
    }

    private getTags() {
        this.dataHandler.getTags()
            .subscribe({
                next: (res) => {
                    this.tags = res as [];
                },
                error: () => this.error = 'Ошибка при загрузке тэгов',
                complete: () => {
                    this.dataLoaded = Promise.resolve(true)
                    this.error = undefined;
                }
            });
    }
}