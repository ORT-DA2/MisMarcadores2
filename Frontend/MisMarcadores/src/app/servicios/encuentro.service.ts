import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { BaseApiService } from './base-api.service';
import { Encuentro } from '../clases/encuentro';
import { EncuentroRequest } from '../interfaces/encuentro-request.interface';

@Injectable()
export class EncuentroService {

    constructor(
      private http: HttpClient,
      private baseApiService: BaseApiService,
      ) {
    }

    obtenerEncuentros(): Observable<Array<Encuentro>> {
      return this.baseApiService.get<Array<Encuentro>>('encuentros', true);
    }

    obtenerEncuentro(id: string): Observable<Encuentro> {
      return this.baseApiService.get<Encuentro>(`encuentros/${id}`, true);
    }

    agregarEncuentro(request: EncuentroRequest): Observable<any> {
      return this.baseApiService.post<EncuentroRequest, any>('encuentros', request, true);
    }

    editarEncuentro(id: string, request: EncuentroRequest) {
      return this.baseApiService.put<EncuentroRequest, any>(`encuentros/${id}`, request, true);
    }

    borrareEncuentro(id: string) {
      return this.baseApiService.delete(`encuentros/${id}`, true);
    }

}
