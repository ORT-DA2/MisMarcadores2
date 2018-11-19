import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioRequest } from '../interfaces/usuario-request.interface';
import { UsuarioService } from '../servicios/usuario.service';

@Component({
    templateUrl: './usuarios-list.component.html',
    styleUrls: ['./usuarios-list.css']
})
export class UsuariosListComponent implements OnInit {

    usuarios: Array<UsuarioRequest>;
    loading = false;

    constructor(private router: Router, private _usuariosService: UsuarioService) {
    }

    ngOnInit(): void {
        this.loading = true;
        this.obtenerUsuarios();
    }

    private result(data: Array<UsuarioRequest>): void {
        this.usuarios = data;
        this.loading = false;
    }

    obtenerUsuarios() {
        this._usuariosService.obtenerUsuarios()
            .subscribe(((data: Array<UsuarioRequest>) => this.result(data)),
                ((error: any) => console.log(error))
            );
    }

    agregarUsuario() {
        this.router.navigate(['/usuarios/agregar']);
    }

    editarUsuario(nombreUsuario: string) {
        this.router.navigate(['/usuarios', nombreUsuario]);
    }

    borrarUsuario(nombreUsuario: string) {
        this._usuariosService.borrarUsuario(nombreUsuario).subscribe(
            ((data: any) => this.obtenerUsuarios()),
            ((error: any) => console.log(error))
        );
    }
}
