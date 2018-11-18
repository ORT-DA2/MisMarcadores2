import { Component, OnInit } from '@angular/core';
import { SesionService } from '../servicios/sesion.service';
import { UsuarioService } from '../servicios/usuario.service';
import { LoginRequest } from '../interfaces/login-request';
import { Sesion } from '../interfaces/sesion';
import { Router } from '@angular/router';
import { NotificationService } from '../core/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: LoginRequest;
  submitted: boolean;

  constructor(
    private sesionService: SesionService,
    private userService: UsuarioService,
    private router: Router,
    private notificationService: NotificationService) {
    this.model = { NombreUsuario: '', Contrasena: '' };
  }

  ngOnInit() {
  }

  onSubmit() {
    this.userService.postLogin(this.model).subscribe((response: Sesion) => {
      if (response) {
        this.notificationService.display({ message: 'Usuario Logueado!', severity: 'info' });

        this.sesionService.setSesion(response);
        localStorage.setItem('nombreUsuario', this.model.NombreUsuario);

        if (this.sesionService.isAuthenticated) {
          this.router.navigate([this.sesionService.attemptedUrl]);
        }
      } else {
        this.notificationService.display({ message: 'Usuario o contraseña incorrectos.', severity: 'error' });
      }

    }, () => {
      this.notificationService.display({ message: 'Usuario o contraseña incorrectos.', severity: 'error' });
    });
  }

}
