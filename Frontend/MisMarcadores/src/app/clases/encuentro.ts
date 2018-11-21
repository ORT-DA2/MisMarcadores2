import { ParticipanteEncuentro } from '../interfaces/participanteEncuentro.interface';

export class Encuentro {
    id: string;
    nombreDeporte: string;
    fecha: Date;
    participanteEncuentro: Array<ParticipanteEncuentro>;
    constructor() {}
}
