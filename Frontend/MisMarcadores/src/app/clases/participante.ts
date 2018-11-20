import { Deporte } from './deporte';

export class Participante {
    id: string;
    nombre: string;
    foto: string;
    deporte: Deporte;
    esEquipo: boolean;
    constructor() {
        this.deporte = new Deporte();
    }
}
