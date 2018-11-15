import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Session } from '../interfaces/session';

const tokenKey = 'currentToken';

@Injectable()
export class SessionService {

    attemptedUrl: string;
    tokenChanged = new Subject<string>();

    constructor() {
        this.attemptedUrl = 'home';
    }

    isAuthenticated(): boolean {
        return this.getToken() !== null;
    }

    setToken(token: string): void {
        localStorage.setItem('token', token);
    }

    removeToken(): void {
        localStorage.removeItem('token');
    }

    getToken(): string {
        return localStorage.getItem('token');
    }

    setSession(session: Session) {
        localStorage.setItem('token', session.id);
        localStorage.setItem('role', session.admin);
    }

    isAdmin(): boolean {
        return localStorage.getItem('role') === 'Admin';
    }

    logOff() {
        localStorage.removeItem('token');
        localStorage.removeItem('role');
    }
}
