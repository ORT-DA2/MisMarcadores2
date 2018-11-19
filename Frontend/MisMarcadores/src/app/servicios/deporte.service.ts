import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginRequest } from '../interfaces/login-request';
import { BaseApiService } from './base-api.service';
import { Sesion } from '../interfaces/sesion';
import { SesionService } from '../servicios/sesion.service';
import { DeporteRequest } from '../interfaces/deporte-request.interface';

@Injectable()
export class DeporteService {

  constructor(
    private http: HttpClient,
    private baseApiService: BaseApiService,
    private auth: SesionService,
  ) { }

  agregarDeporte(request: DeporteRequest): Observable<any> {
    return this.baseApiService.post<DeporteRequest, any>('deportes', request, true);
  }

  obtenerDeporte(nombre: string): Observable<DeporteRequest> {
    return this.baseApiService.get<DeporteRequest>(`deportes/${nombre}`, true);
  }

  obtenerDeportes(): Observable<Array<DeporteRequest>> {
    return this.baseApiService.get<Array<DeporteRequest>>('deportes', true);
  }

  editarDeporte(nombre: string, request: DeporteRequest) {
    return this.baseApiService.put<DeporteRequest, any>(`deportes/${nombre}`, request, true);
  }

  borrarDeporte(nombre: string) {
    return this.baseApiService.delete(`deportes/${nombre}`, true);
  }

}
