import { Puntaje } from '../interfaces/puntaje.interface';

export class EncuentroResponse {
    id: string;
    nombreDeporte: string;
    fecha: Date;
    puntajes: Array<Puntaje>;
    constructor() {}
}
