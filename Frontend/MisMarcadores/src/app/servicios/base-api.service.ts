import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { SessionService } from './session.service';

@Injectable()
export class BaseApiService {

    constructor(private http: HttpClient, private sessionService: SessionService) { }

    private basicHeaderConfig = new HttpHeaders({ 'Content-Type': 'application/json' });

    post<T, Y>(url: string, request: T, isTokenRequired: boolean = false): Observable<Y> {
        return this.http.post<any>(`${environment.apiUrl}/${url}`, request, { headers: this.getHeader(isTokenRequired) });
    }

    private getHeader(tokenRequired: boolean): HttpHeaders {
        return tokenRequired ?
            new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': this.sessionService.getToken() }) :
            new HttpHeaders({ 'Content-Type': 'application/json' });
    }

}
