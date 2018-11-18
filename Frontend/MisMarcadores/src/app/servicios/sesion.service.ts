import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Sesion } from '../interfaces/sesion';

const tokenKey = 'currentToken';

@Injectable()
export class SesionService {

    attemptedUrl: string;
    tokenChanged = new Subject<string>();

    constructor() {
        this.attemptedUrl = '/home';
    }

    isAuthenticated(): boolean {
        return this.getToken() !== 'no-token';
    }

    setToken(token: string): void {
        localStorage.setItem('token', token);
    }

    removeToken(): void {
        localStorage.removeItem('token');
    }

    getToken(): string {
        return localStorage.getItem('token') || 'no-token';
    }

    setSesion(sesion: Sesion) {
        localStorage.setItem('token', sesion.token);
    }

    logOff() {
        localStorage.removeItem('token');
    }
}
