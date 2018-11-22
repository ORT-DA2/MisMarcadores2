import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginRequest } from '../interfaces/login-request';
import { BaseApiService } from './base-api.service';
import { Sesion } from '../interfaces/sesion';
import { UsuarioRequest } from '../interfaces/usuario-request.interface';

@Injectable()
export class UsuarioService {

  constructor(
    private http: HttpClient,
    private baseApiService: BaseApiService) { }

  postLogin(request: LoginRequest): Observable<Sesion> {
    return this.baseApiService.post<LoginRequest, Sesion>('sesiones', request);
  }

  obtenerUsuario(nombreUsuario: string): Observable<UsuarioRequest> {
    return this.baseApiService.get<UsuarioRequest>(`usuarios/${nombreUsuario}`, true);
  }

  obtenerUsuarioActual(): Observable<UsuarioRequest> {
    const usuarioActual = localStorage.getItem('nombreUsuario');
    return this.baseApiService.get<UsuarioRequest>(`usuarios/${usuarioActual}`, true);
  }

  agregarUsuario(request: UsuarioRequest): Observable<any> {
    return this.baseApiService.post<UsuarioRequest, any>('usuarios', request, true);
  }

  obtenerUsuarios(): Observable<Array<UsuarioRequest>> {
    return this.baseApiService.get<Array<UsuarioRequest>>('usuarios', true);
  }

  obtenerFavoritos(): Promise<Array<string>> {
    return this.baseApiService.getPromise<Array<string>>('favoritos', true);
  }

  editarUsuario(request: UsuarioRequest) {
    const nombreUsuario = request.nombreUsuario;
    return this.baseApiService.put<UsuarioRequest, any>(`usuarios/${nombreUsuario}`, request, true);
  }

  borrarUsuario(nombreUsuario: string) {
    return this.baseApiService.delete(`usuarios/${nombreUsuario}`, true);
  }

}
