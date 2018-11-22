import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute} from '@angular/router';
import { EncuentroResponse } from '../../clases/encuentro-response';
import { DeporteService } from '../../servicios/deporte.service';
import { EncuentroService } from '../../servicios/encuentro.service';
import { DeporteRequest } from '../../interfaces/deporte-request.interface';

@Component({
    templateUrl: './reporte-deporte.component.html',
    styleUrls: ['./reporte-deporte.css']
})

export class ReporteDeporteComponent implements OnInit {

    model: any = {};
    loading = false;
    encuentros: Array<EncuentroResponse>;
    deportes: Array<DeporteRequest>;
    deporteActual: DeporteRequest;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private _deportesService: DeporteService,
        private _encuentrosService: EncuentroService) {
    }

    ngOnInit(): void {
        this.loading = true;
        this.obtenerDeportes();
    }

    private result(data: Array<DeporteRequest>): void {
        this.deportes = data;
        this.loading = false;
    }

    onChange(deporteSeleccionado) {
        this.obtenerDeporte(deporteSeleccionado);
        this.obtenerEncuentros(deporteSeleccionado);
    }

    toArray(answers: object) {
        return Object.keys(answers).map(key => answers[key])
    }

    obtenerDeporte(nombre: string) {
        this._deportesService.obtenerDeporte(nombre)
            .then((obtainedDeporte: DeporteRequest) => {
                this.deporteActual = obtainedDeporte;
            },
            error => {
                console.log(error);
            }
        );
    }

    obtenerDeportes() {
        this._deportesService.obtenerDeportes()
            .then(((data: Array<DeporteRequest>) => this.result(data)),
                ((error: any) => console.log(error))
            );
    }

    obtenerEncuentros(nombre: string) {
        this._encuentrosService.obtenerEncuentrosPorDeporte(nombre)
            .then((data: any) => {
                this.encuentros = data;
            },
            error => {
                console.log(error);
            }
        );
    }
}
