import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Deporte } from '../clases/deporte';
import { DeporteService } from '../servicios/deporte.service';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from '../core/notification.service';

@Component({
  selector: 'app-deportes',
  templateUrl: './deporte.component.html',
  styleUrls: ['./deporte.component.css']
})
export class DeporteComponent implements OnInit {

  pageTitle = 'Nuevo deporte';
  model: any = {};
  deporte: Deporte;
  isEditing: boolean;
  nombreActual: string;
  btnText = 'Agregar';

  constructor(private route: ActivatedRoute,
      private router: Router,
      private _deportesService: DeporteService,
      private notificationService: NotificationService) { }

  ngOnInit(): void {
      this.nombreActual = this.route.snapshot.params['nombre'];
      if (this.nombreActual != null) {
          this.pageTitle = 'Editar deporte';
          this.isEditing = true;
          this.btnText = 'Actualizar';
          this.obtenerDatosDeporte(this.nombreActual);
      }
  }

  obtenerDatosDeporte(nombre: string) {
      this._deportesService.obtenerDeporte(nombre)
          .subscribe((obtainedDeporte: Deporte) => {
              this.deporte = obtainedDeporte;
              this.setearModelo();
          },
          error => {
              this.handleError(error);
          }
      );
  }

  submitForm() {
      this.setearDeporte();
      if (this.isEditing) {
          this.editarDeporte();
      } else {
          this.crearDeporte();
      }
  }

  crearDeporte() {
      this._deportesService.agregarDeporte(this.deporte)
          .subscribe(
              data => {
                  this.router.navigate(['/deportes']);
              },
              error => {
                  this.handleError(error);
              }
          );
  }

  editarDeporte() {
      this._deportesService.editarDeporte(this.nombreActual, this.deporte)
          .subscribe(
              data => {
                  this.router.navigate(['/deportes']);
              },
              error => {
                  this.handleError(error);
              }
          );
  }

  handleError(error: HttpErrorResponse): void {
      this.notificationService.display({ message: error.error, severity: 'error' });
}

  setearDeporte() {
      this.deporte = new Deporte();
      this.deporte.nombre = this.model.name;
      this.deporte.esIndividual = this.model.individual;
  }

  setearModelo() {
      this.model.name = this.deporte.nombre;
      this.model.individual = this.deporte.esIndividual;
  }

}
