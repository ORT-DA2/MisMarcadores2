import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Participante } from '../clases/participante';
import { ParticipanteService } from '../servicios/participante.service';

@Component({
    templateUrl: './participantes-list.component.html',
    styleUrls: ['./participantes-list.css']
})
export class ParticipantesListComponent implements OnInit {

    participantes: Array<Participante>;
    loading = false;

    constructor(private router: Router, private _participantesService: ParticipanteService) {
    }

    ngOnInit(): void {
        this.loading = true;
        this.obtenerParticipantes();
    }

    private result(data: Array<Participante>): void {
        this.participantes = data;
        this.loading = false;
    }

    obtenerParticipantes() {
        this._participantesService.obtenerParticipantes()
            .then(((data: Array<Participante>) => this.result(data)),
                ((error: any) => console.log(error))
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
