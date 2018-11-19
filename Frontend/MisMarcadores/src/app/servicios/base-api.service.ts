import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { SesionService } from './sesion.service';

@Injectable()
export class BaseApiService {

    constructor(private http: HttpClient, private sesionService: SesionService) { }

    private basicHeaderConfig = new HttpHeaders({ 'Content-Type': 'application/json' });

    get<T>(url: string, isTokenRequired: boolean = false): Observable<T> {
        console.log(this.sesionService.getToken());
        return this.http.get<T>(`${environment.apiUrl}/${url}`, { headers: this.getHeader(isTokenRequired) });
    }

    post<T, Y>(url: string, request: T, isTokenRequired: boolean = false): Observable<Y> {
        return this.http.post<any>(`${environment.apiUrl}/${url}`, request, { headers: this.getHeader(isTokenRequired) });
    }

    put<T, Y>(url: string, request: T, isTokenRequired: boolean = false): Observable<Y> {
        return this.http.put<any>(`${environment.apiUrl}/${url}`, request, { headers: this.getHeader(isTokenRequired) });
    }

    delete(url: string, isTokenRequired: boolean = false) {
        return this.http.delete<any>(`${environment.apiUrl}/${url}`, { headers: this.getHeader(isTokenRequired) });
    }

    private getHeader(tokenRequired: boolean): HttpHeaders {
        return tokenRequired ?
            new HttpHeaders({ 'Content-Type': 'application/json', 'tokenSesion': this.sesionService.getToken() }) :
            new HttpHeaders({ 'Content-Type': 'application/json' });
    }

}
