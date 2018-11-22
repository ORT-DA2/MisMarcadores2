import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute} from '@angular/router';
import { Participante } from '../clases/participante';
import { Posicion } from '../clases/posicion';
import { DeporteService } from '../servicios/deporte.service';
import { DeporteRequest } from '../interfaces/deporte-request.interface';

@Component({
    templateUrl: './posiciones-list.component.html',
    styleUrls: ['./posiciones-list.css']
})

export class PosicionesComponent implements OnInit {

    model: any = {};
    loading = false;
    posiciones: Array<Posicion>;
    deportes: Array<DeporteRequest>;
    deporteActual: DeporteRequest;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private _deportesService: DeporteService) {
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
        this.obtenerPosiciones(deporteSeleccionado);
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

    obtenerPosiciones(nombre: string) {
        this._deportesService.obtenerPosicionesDeporte(nombre)
            .then((data: any) => {
                this.posiciones = data;
            },
            error => {
                console.log(error);
            }
        );
    }
}
