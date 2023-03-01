import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiPath, environment } from "src/environment";

@Injectable()
export class AuthService {
    private registerUrl = environment.baseApiUrl + ApiPath.Registration;
    private authenticateUrl = environment.baseApiUrl + ApiPath.Login;

    constructor(private http: HttpClient) {
    }

    registerUser(user: any) {
        return this.http.post<any>(this.registerUrl, user);
    }

    authenticateUser(user: any) {
        return this.http.post<any>(this.authenticateUrl, user);
    }

    loggedIn() {
        return !!localStorage.getItem('token')
    }

    getToken() {
        return localStorage.getItem('token')
    }

    logout() {
        localStorage.removeItem('token')
    }
}