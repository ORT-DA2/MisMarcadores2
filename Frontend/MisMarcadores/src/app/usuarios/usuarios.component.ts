import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Usuario } from '../clases/usuario';
import { UsuarioService } from '../servicios/usuario.service';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from '../core/notification.service';

@Component({
    templateUrl: './usuarios.component.html',
    styleUrls: ['./usuarios.css']
})

export class UsuariosComponent implements OnInit {
    pageTitle = 'Nuevo usuario';
    model: any = {};
    usuario: Usuario;
    isEditing: boolean;
    btnText = 'Agregar';

    constructor(private route: ActivatedRoute,
        private router: Router,
        private _usuariosService: UsuarioService,
        private notificationService: NotificationService) { }

    ngOnInit(): void {
        const nombreUsuario = this.route.snapshot.params['nombreUsuario'];
        if (nombreUsuario != null) {
            this.pageTitle = 'Editar usuario';
            this.isEditing = true;
            this.btnText = 'Actualizar';
            this.obtenerDatosUsuario(nombreUsuario);
        }
    }

    obtenerDatosUsuario(nombreUsuario: string) {
        this._usuariosService.obtenerUsuario(nombreUsuario)
            .subscribe((obtainedUsuario: Usuario) => {
                this.usuario = obtainedUsuario;
                this.setearModelo();
            },
            error => {
                this.handleError(error);
            }
        );
    }

    submitForm() {
        this.setearUsuario();
        if (this.isEditing) {
            this.editarUsuario();
        } else {
            this.crearUsuario();
        }
    }

    crearUsuario() {
        this._usuariosService.agregarUsuario(this.usuario)
            .subscribe(
                data => {
                    this.router.navigate(['/usuarios']);
                },
                error => {
                    this.handleError(error);
                }
            );
    }

    editarUsuario() {
        this._usuariosService.editarUsuario(this.usuario)
            .subscribe(
                data => {
                    this.router.navigate(['/usuarios']);
                },
                error => {
                    this.handleError(error);
                }
            );
    }

    handleError(error: HttpErrorResponse): void {
        this.notificationService.display({ message: error.error, severity: 'error' });
      }

    setearUsuario() {
        this.usuario = new Usuario();
        this.usuario.nombreUsuario = this.model.userName;
        this.usuario.nombre = this.model.name;
        this.usuario.apellido = this.model.lastName;
        this.usuario.contraseña = this.model.pass;
        this.usuario.mail = this.model.mail;
        this.usuario.administrador = this.model.admin;
    }

    setearModelo() {
        this.model.userName = this.usuario.nombreUsuario;
        this.model.name = this.usuario.nombre;
        this.model.lastName = this.usuario.apellido;
        this.model.pass = this.usuario.contraseña;
        this.model.mail = this.usuario.mail;
        this.model.admin = this.usuario.administrador;
    }
}
