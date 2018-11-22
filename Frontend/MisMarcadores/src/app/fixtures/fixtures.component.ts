import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute} from '@angular/router';
import { Participante } from '../clases/participante';
import { Posicion } from '../clases/posicion';
import { Fixture } from '../clases/fixture';
import { DeporteService } from '../servicios/deporte.service';
import { EncuentroService } from '../servicios/encuentro.service';
import { DeporteRequest } from '../interfaces/deporte-request.interface';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from '../core/notification.service';

@Component({
    templateUrl: './fixtures.component.html',
    styleUrls: ['./fixtures.css']
})

export class FixtureComponent implements OnInit {

    model: any = {};
    loading = false;
    fixture: Fixture;
    deportes: Array<DeporteRequest>;
    deporteActual: DeporteRequest;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private notificationService: NotificationService,
        private _encuentrosService: EncuentroService,
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

    submitForm() {
        this.setearFixture();
        console.log(this.deporteActual);
        if (!this.deporteActual.esIndividual) {
            this.generarFixture();
        } else {
            this.notificationService.display({ message: 'El fixture solo puede ser generado para '
             + 'deportes que no son individuales.', severity: 'error' });
        }
    }

    setearFixture() {
        this.fixture = new Fixture();
        this.fixture.fechaInicio = this.model.fecha;
        this.fixture.deporte = this.model.deporte;
        this.fixture.tipo = this.model.tipo;
    }

    generarFixture() {
        this._encuentrosService.generarFixture(this.fixture)
        .subscribe(
            data => {
                this.router.navigate(['/encuentros']);
            },
            error => {
                this.handleError(error);
            }
        );
    }

    handleError(error: HttpErrorResponse): void {
        this.notificationService.display({ message: error.error, severity: 'error' });
    }

}
