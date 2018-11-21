import { ParticipanteEncuentro } from '../interfaces/participanteEncuentro.interface';

export class EncuentroRequest {
    id: string;
    nombreDeporte: string;
    fecha: Date;
    participanteEncuentro: Array<ParticipanteEncuentro>;
    constructor() {}
}
