import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { SessionService } from './session.service';
import { environment } from '../../environments/environment';
import { Participante } from '../interfaces/participante-request.interface';

@Injectable()
export class ParticipanteService {

  private headers: HttpHeaders;

    constructor(private http: HttpClient, private sessionService: SessionService) {
    }

    getHeader(): HttpHeaders {
      return new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': this.sessionService.getToken() });
    }

    getParticipantes(): Observable<Participante[]> {
      return this.http.get<Participante[]>(`${environment.apiUrl}/participantes`, { headers: this.getHeader() });
    }

    getParticipante(id: string): Observable<Participante> {
      if (id) {
        return this.http.get<Participante>(`${environment.apiUrl}/participantes/${id}`, { headers: this.getHeader() });
      }
      return of(null);
    }

    post(dto: Participante): Observable<any> {
      return this.http.post(`${environment.apiUrl}/participantes`, dto, { headers: this.getHeader(), observe: 'response' });
    }

    put(dto: Participante): Observable<any> {
      return this.http.put(`${environment.apiUrl}/participantes`, dto, { headers: this.getHeader(), observe: 'response' });
    }

}
