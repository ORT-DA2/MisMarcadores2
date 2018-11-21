import { ParticipanteEncuentro } from '../interfaces/participanteEncuentro.interface';

export interface EncuentroRequest {
    id: string;
    fecha: Date;
    nombreDeporte: string;
    participanteEncuentro: Array<ParticipanteEncuentro>;
}
