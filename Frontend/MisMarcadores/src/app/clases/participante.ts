import { Deporte } from './deporte';

export class Participante {
    id: string;
    nombre: string;
    foto: string;
    deporte: Deporte;
    constructor() {
        this.deporte = new Deporte();
    }
}
