import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Encuentro } from '../clases/encuentro';
import { EncuentroService } from '../servicios/encuentro.service';

@Component({
    templateUrl: './encuentros-list.component.html',
    styleUrls: ['./encuentros-list.css']
})

export class EncuentrosListComponent implements OnInit {

    encuentros: Array<Encuentro>;
    loading = false;

    constructor(
        private router: Router, 
        private _encuentrosService: EncuentroService) {
    }

    ngOnInit(): void {
        this.loading = true;
        this.obtenerEncuentros();
    }

    private result(data: Array<Encuentro>): void {
        this.encuentros = data;
        this.loading = false;
    }

    obtenerEncuentros() {
        this._encuentrosService.obtenerEncuentros()
            .subscribe(((data: Array<Encuentro>) => this.result(data)),
                ((error: any) => console.log(error))
            );
    }

    toArray(answers: object) {
        return Object.keys(answers).map(key => answers[key])
    }

    agregarEncuentro() {
        this.router.navigate(['/encuentros/agregar']);
    }

    editarEncuentro(id: string) {
        this.router.navigate(['/encuentros', id]);
    }

    borrarEncuentro(id: string) {
        this._encuentrosService.borrareEncuentro(id).subscribe(
            ((data: any) => this.obtenerEncuentros()),
            ((error: any) => console.log(error))
        );
    }
}
