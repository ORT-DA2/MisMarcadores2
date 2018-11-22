import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Participante } from '../clases/participante';
import { UsuarioService } from '../servicios/usuario.service';
import { ParticipanteService } from '../servicios/participante.service';

@Component({
    templateUrl: './participantes-list.component.html',
    styleUrls: ['./participantes-list.css']
})
export class ParticipantesListComponent implements OnInit {

    participantes: Array<Participante> = new Array<Participante>();
    favoritos: Array<string>;
    toDos: string[];
    loading = false;

    constructor(private router: Router,
        private _participantesService: ParticipanteService,
        private _usuariosService: UsuarioService) {
    }

    ngOnInit(): void {
        this.loading = true;
        this.obtenerParticipantes();
    }

    private result(data: Array<Participante>): void {
        this.participantes = data;
        this.loading = false;
    }

    checkValue(id: string, event: any) {
        console.log(id);
        console.log(event);
    }

    obtenerParticipantes() {
        this._participantesService.obtenerParticipantes()
            .then((data: Array<Participante>) => {
                this.result(data);
                this.obtenerFavoritos();
                console.log(this.participantes);
            },
            error => {
                console.log(error);
            }
        );
    }

    obtenerFavoritos() {
        this._usuariosService.obtenerFavoritos()
            .then((data: Array<string>) => {
                this.favoritos = data;
            },
            error => {
                console.log(error);
            }
        );
    }

    agregarParticipante() {
        this.router.navigate(['/participantes/agregar']);
    }

    editarParticipante(id: string) {
        this.router.navigate(['/participantes', id]);
    }

    borrarParticipante(id: string) {
        this._participantesService.borrarParticipante(id).subscribe(
            ((data: any) => this.obtenerParticipantes()),
            ((error: any) => console.log(error))
        );
    }
}
