import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginRequest } from '../interfaces/login-request';
import { BaseApiService } from './base-api.service';
import { Session } from '../interfaces/session';
import { UsuarioRequest } from '../interfaces/usuario-request.interface';


@Injectable()
export class UserService {

  constructor(private baseApiService: BaseApiService) { }

  postLogin(request: LoginRequest): Observable<Session> {
    return this.baseApiService.post<LoginRequest, Session>('login', request);
  }

  registerUser(request: UsuarioRequest): Observable<any> {
    return this.baseApiService.post<LoginRequest, Session>('users', request, true);
  }

}
