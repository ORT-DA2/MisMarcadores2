import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginRequest } from '../interfaces/login-request';
import { BaseApiService } from './base-api.service';
import { Sesion } from '../interfaces/sesion';
import { SesionService } from '../servicios/sesion.service';
import { UsuarioRequest } from '../interfaces/usuario-request.interface';

@Injectable()
export class UsuarioService {
  private nombreUsuario: string;

  constructor(
    private http: HttpClient, 
    private baseApiService: BaseApiService,
    private auth: SesionService,
  ) { }

  postLogin(request: LoginRequest): Observable<Sesion> {
    return this.baseApiService.post<LoginRequest, Sesion>('sesiones', request);
  }

  getUsuario(): Observable<UsuarioRequest> {
    this.nombreUsuario = localStorage.getItem('nombreUsuario');
    console.log(this.nombreUsuario);
    return this.baseApiService.get<UsuarioRequest>(`usuarios/${this.nombreUsuario}`, true);
  }

  agregarUsuario(request: UsuarioRequest): Observable<any> {
    return this.baseApiService.post<UsuarioRequest, any>('usuarios', request, true);
  }

}
