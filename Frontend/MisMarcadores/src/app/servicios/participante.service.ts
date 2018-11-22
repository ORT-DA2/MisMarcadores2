import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { BaseApiService } from './base-api.service';
import { Participante } from '../clases/participante';
import { ParticipanteRequest } from '../interfaces/participante-request.interface';

@Injectable()
export class ParticipanteService {

  private headers: HttpHeaders;

    constructor(
      private http: HttpClient,
      private baseApiService: BaseApiService) {
    }

    obtenerParticipantes(): Promise<Array<Participante>> {
      return this.baseApiService.getPromise<Array<Participante>>('participantes', true);
    }

    obtenerParticipante(id: string): Promise<Participante> {
      return this.baseApiService.getPromise<Participante>(`participantes/${id}`, true);
    }

    agregarParticipante(request: ParticipanteRequest): Observable<any> {
      return this.baseApiService.post<ParticipanteRequest, any>('participantes', request, true);
    }

    editarParticipante(id: string, request: ParticipanteRequest) {
      return this.baseApiService.put<ParticipanteRequest, any>(`participantes/${id}`, request, true);
    }

    borrarParticipante(id: string) {
      return this.baseApiService.delete(`participantes/${id}`, true);
    }

}
