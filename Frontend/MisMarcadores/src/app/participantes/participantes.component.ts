import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Participante } from '../clases/participante';
import { ParticipanteRequest } from '../clases/participante-request';
import { ParticipanteService } from '../servicios/participante.service';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from '../core/notification.service';
import { DeporteRequest } from '../interfaces/deporte-request.interface';
import { DeporteService } from '../servicios/deporte.service';

@Component({
    templateUrl: './participantes.component.html',
    styleUrls: ['./participantes.css']
})

export class ParticipantesComponent implements OnInit {
    pageTitle = 'Nuevo participante';
    model: any = {};
    participante: Participante;
    deportes: Array<DeporteRequest>;
    participanteRequest: ParticipanteRequest;
    selectedFile: File;
    foto: string;
    isEditing: boolean;
    idActual: string;
    btnText = 'Agregar';

    constructor(private route: ActivatedRoute,
        private router: Router,
        private _participantesService: ParticipanteService,
        private _deportesService: DeporteService,
        private notificationService: NotificationService) { }

    ngOnInit(): void {
        this.idActual = this.route.snapshot.params['idParticipante'];
        if (this.idActual != null) {
            this.pageTitle = 'Editar participante';
            this.isEditing = true;
            this.btnText = 'Actualizar';
            this.obtenerDatosParticipante(this.idActual);
        }
        this.obtenerDeportes();
    }

    private result(data: Array<DeporteRequest>): void {
        this.deportes = data;
    }

    obtenerDeportes() {
        this._deportesService.obtenerDeportes()
            .then(((data: Array<DeporteRequest>) => this.result(data)),
                ((error: any) => console.log(error))
            );
    }


    onFileChanged(event) {
        this.selectedFile = event.target.files[0];
        const myReader: FileReader = new FileReader();
        myReader.onloadend = (e) => {
            const loader: string = myReader.result as string;
            this.foto = loader;
        };
        myReader.readAsDataURL(this.selectedFile);
    }

    obtenerDatosParticipante(id: string) {
        this._participantesService.obtenerParticipante(id)
            .subscribe((obtainedParticipante: Participante) => {
                this.participante = obtainedParticipante;
                this.setearModelo();
            },
            error => {
                this.handleError(error);
            }
        );
    }

    submitForm() {
        this.setearParticipante();
        if (this.isEditing) {
            this.editarParticipante();
        } else {
            this.crearParticipante();
        }
    }

    crearParticipante() {
        this._participantesService.agregarParticipante(this.participanteRequest)
            .subscribe(
                data => {
                    this.router.navigate(['/participantes']);
                },
                error => {
                    this.handleError(error);
                }
            );
    }

    editarParticipante() {
        this._participantesService.editarParticipante(this.idActual, this.participanteRequest)
            .subscribe(
                data => {
                    this.router.navigate(['/participantes']);
                },
                error => {
                    this.handleError(error);
                }
            );
    }

    handleError(error: HttpErrorResponse): void {
        this.notificationService.display({ message: error.error, severity: 'error' });
      }

    setearParticipante() {
        this.participanteRequest = new ParticipanteRequest();
        this.participanteRequest.nombre = this.model.nombre;
        this.participanteRequest.foto = this.foto;
        this.participanteRequest.nombreDeporte = this.model.deporte;
    }

    setearModelo() {
        this.model.nombre = this.participante.nombre;
        this.model.foto = this.participante.foto;
        this.model.deporte = this.participante.deporte.nombre;
    }
}
