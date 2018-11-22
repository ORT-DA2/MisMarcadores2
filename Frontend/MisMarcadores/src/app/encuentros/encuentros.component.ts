import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { EncuentroResponse } from '../clases/encuentro-response';
import { Participante } from '../clases/participante';
import { EncuentroRequest } from '../clases/encuentro-request';
import { DeporteRequest } from '../interfaces/deporte-request.interface';
import { ParticipanteEncuentro } from '../clases/participanteEncuentro';
import { EncuentroService } from '../servicios/encuentro.service';
import { DeporteService } from '../servicios/deporte.service';
import { ParticipanteService } from '../servicios/participante.service';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from '../core/notification.service';

@Component({
    templateUrl: './encuentros.component.html',
    styleUrls: ['./encuentros.css']
})

export class EncuentrosComponent implements OnInit {
    pageTitle = 'Nuevo encuentro';
    model: any = {};
    encuentro: EncuentroResponse;
    deportes: Array<DeporteRequest>;
    posicion: Array<any>;
    resultados: Array<ParticipanteEncuentro> = new Array<ParticipanteEncuentro>();
    participantes: Array<Participante> = new Array<Participante>();
    deporteActual: DeporteRequest;
    encuentroRequest: EncuentroRequest;
    isEditing: boolean;
    idActual: string;
    btnText = 'Agregar';

    constructor(private route: ActivatedRoute,
        private router: Router,
        private _encuentrosService: EncuentroService,
        private _participantesService: ParticipanteService,
        private notificationService: NotificationService,
        private _deportesService: DeporteService) { }

    ngOnInit(): void {
        this.idActual = this.route.snapshot.params['idEncuentro'];
        if (this.idActual != null) {
            this.pageTitle = 'Editar encuentro';
            this.isEditing = true;
            this.btnText = 'Actualizar';
            this.obtenerDatosEncuentro(this.idActual);
        }
        this.obtenerDeportes();
    }

    onChange(deporteSeleccionado) {
        this.participantes = new Array<Participante>();
        this.obtenerDeporte(deporteSeleccionado);
    }

    obtenerDeporte(nombre: string) {
        this._deportesService.obtenerDeporte(nombre)
            .then((obtainedDeporte: DeporteRequest) => {
                this.deporteActual = obtainedDeporte;
                this.obtenerParticipantes();
            },
            error => {
                this.handleError(error);
            }
        );
    }

    obtenerDeportes() {
        this._deportesService.obtenerDeportes()
            .then(((data: Array<DeporteRequest>) => this.deportes = data),
                ((error: any) => console.log(error))
            );
    }

    obtenerParticipantes(ids: Array<string> = null) {
        let participantesActuales: Array<Participante> = new Array<Participante>();
        this._participantesService.obtenerParticipantes()
        .then((data: Array<Participante>) => {
                participantesActuales = data;
                participantesActuales.forEach(p => {
                    if (p.deporte.toString() === this.deporteActual.nombre) {
                        if (ids == null) {
                            console.log(p);
                            this.participantes.push(p);
                        } else {
                            if (p.id in ids) {
                                this.participantes.push(p);
                            }
                        }
                    }
                });
            },
            error => {
                this.handleError(error);
            }
        );
    }

    obtenerDatosEncuentro(id: string) {
        this._encuentrosService.obtenerEncuentro(id)
            .then((obtainedEncuentro: EncuentroResponse) => {
                this.encuentro = obtainedEncuentro;
                this.obtenerDeporte(this.encuentro.nombreDeporte);
                this.setearModelo();
            },
            error => {
                this.handleError(error);
            }
        );
    }

    submitForm() {
        this.setearEncuentro();
        if (this.isEditing) {
            this.editarEncuentro();
        } else {
            this.crearEncuentro();
        }
    }

    crearEncuentro() {
        this._encuentrosService.agregarEncuentro(this.encuentroRequest)
            .subscribe(
                data => {
                    this.router.navigate(['/encuentros']);
                },
                error => {
                    this.handleError(error);
                }
            );
    }

    editarEncuentro() {
        this._encuentrosService.editarEncuentro(this.idActual, this.encuentroRequest)
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

    setearEncuentro() {
        this.encuentroRequest = new EncuentroRequest();
        this.encuentroRequest.fecha = this.model.fecha;
        this.encuentroRequest.nombreDeporte = this.model.deporte;
        if (!this.deporteActual.esIndividual) {
            const encuentroLocal = new ParticipanteEncuentro();
            encuentroLocal.participanteId = this.model.participanteLocal;
            encuentroLocal.puntosObtenidos = this.model.puntosLocal;
            const encuentroVisitante = new ParticipanteEncuentro();
            encuentroVisitante.participanteId = this.model.participanteVisitante;
            encuentroVisitante.puntosObtenidos = this.model.puntosVisitante;
            this.resultados.push(encuentroLocal);
            this.resultados.push(encuentroVisitante);
        }
        this.encuentroRequest.participanteEncuentro = this.resultados;
    }

    setearModelo() {
        this.model.deporte = this.encuentro.nombreDeporte;
    }
}
