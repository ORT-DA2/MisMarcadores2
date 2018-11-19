import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DeporteRequest } from '../interfaces/deporte-request.interface';
import { DeporteService } from '../servicios/deporte.service';

@Component({
    templateUrl: './deportes-list.component.html',
    styleUrls: ['./deportes-list.css']
})
export class DeportesListComponent implements OnInit {

    deportes: Array<DeporteRequest>;
    loading = false;

    constructor(private router: Router, private _deportesService: DeporteService) {
    }

    ngOnInit(): void {
        this.loading = true;
        this.obtenerDeportes();
    }

    private result(data: Array<DeporteRequest>): void {
        this.deportes = data;
        this.loading = false;
    }

    obtenerDeportes() {
        this._deportesService.obtenerDeportes()
            .subscribe(((data: Array<DeporteRequest>) => this.result(data)),
                ((error: any) => console.log(error))
            );
    }

    agregarDeporte() {
        this.router.navigate(['/deportes/agregar']);
    }

    editarDeporte(nombre: string) {
        this.router.navigate(['/deportes', nombre]);
    }

    borrarDeporte(nombre: string) {
        this._deportesService.borrarDeporte(nombre).subscribe(
            ((data: any) => this.obtenerDeportes()),
            ((error: any) => console.log(error))
        );
    }
}
