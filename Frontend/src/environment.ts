export const environment: any = {
    production: false,
    baseApiUrl: 'https://localhost:3001/api'
}

export enum ApiPath {
    Registration = '/account/register',
    Login = '/account/login',
    Notes = '/notes',
    Tags = '/tags',
    Notifications = '/notifications',
    NoteTag = '/tags/notetag'
}