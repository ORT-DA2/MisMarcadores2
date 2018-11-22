import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute} from '@angular/router';
import { EncuentroResponse } from '../../clases/encuentro-response';
import { Participante } from '../../clases/participante';
import { ParticipanteRequest } from '../../clases/participante-request';
import { EncuentroService } from '../../servicios/encuentro.service';
import { ParticipanteService } from 'src/app/servicios/participante.service';

@Component({
    templateUrl: './reporte-participante.component.html',
    styleUrls: ['./reporte-participante.css']
})

export class ReporteParticipanteComponent implements OnInit {

    model: any = {};
    loading = false;
    encuentros: Array<EncuentroResponse>;
    participantes: Array<Participante>;
    participanteActual: Participante;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private _participantesService: ParticipanteService,
        private _encuentrosService: EncuentroService) {
    }

    ngOnInit(): void {
        this.loading = true;
        this.obtenerParticipantes();
    }

    private result(data: Array<Participante>): void {
        this.participantes = data;
        this.loading = false;
    }

    onChange(participanteSeleccionado) {
        console.log(participanteSeleccionado);
        this.obtenerParticipante(participanteSeleccionado);
        this.obtenerEncuentros(participanteSeleccionado);
    }

    toArray(answers: object) {
        return Object.keys(answers).map(key => answers[key])
    }

    obtenerParticipante(id: string) {
        this._participantesService.obtenerParticipante(id)
            .then((obtainedParticipante: Participante) => {
                this.participanteActual = obtainedParticipante;
            },
            error => {
                console.log(error);
            }
        );
    }

    obtenerParticipantes() {
        this._participantesService.obtenerParticipantes()
            .then(((data: Array<Participante>) => this.result(data)),
                ((error: any) => console.log(error))
            );
    }

    obtenerEncuentros(id: string) {
        this._encuentrosService.obtenerEncuentrosPorParticipante(id)
            .then((data: any) => {
                this.encuentros = data;
            },
            error => {
                console.log(error);
            }
        );
    }
}
