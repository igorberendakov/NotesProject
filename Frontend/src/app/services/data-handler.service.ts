import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiPath, environment } from '../../environment';
@Injectable({
    providedIn: 'root'
})
export class DataHandlerService {
    constructor(private http: HttpClient) {
    }

    private baseUrl = environment.baseApiUrl;

    getNotes() {
        return this.http.get(this.baseUrl + ApiPath.Notes);
    }

    getTags() {
        return this.http.get(this.baseUrl + ApiPath.Tags);
    }

    getNotifications() {
        return this.http.get(this.baseUrl + ApiPath.Notifications);
    }

    createNote(noteData: any) {
        return this.http.post(this.baseUrl + ApiPath.Notes, noteData)
    }

    updateNote(noteData: any) {
        return this.http.put(this.baseUrl + ApiPath.Notes, noteData)
    }

    deleteNote(id: string) {
        return this.http.delete(this.baseUrl + ApiPath.Notes + '?id=' + id);
    }

    addTagToNote(noteTag: any) {
        return this.http.post(this.baseUrl + ApiPath.NoteTag, noteTag)
    }

    removeTagFromNote(noteTag: any) {
        return this.http.delete(this.baseUrl + ApiPath.NoteTag, { body: noteTag })
    }

    createTag(tagData: any) {
        return this.http.post(this.baseUrl + ApiPath.Tags, tagData)
    }

    updateTag(noteTag: any) {
        return this.http.put(this.baseUrl + ApiPath.Tags, noteTag)
    }

    deleteTag(id: string) {
        return this.http.delete(this.baseUrl + ApiPath.Tags + '?id=' + id);
    }

    createNotification(notificationData: any) {
        return this.http.post(this.baseUrl + ApiPath.Notifications, notificationData)
    }

    updateNotification(notificationData: any) {
        return this.http.put(this.baseUrl + ApiPath.Notifications, notificationData);
    }

    deleteNotification(id: string) {
        return this.http.delete(this.baseUrl + ApiPath.Notifications + '?id=' + id);
    }
}