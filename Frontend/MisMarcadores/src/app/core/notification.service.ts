import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { NotificationMessage } from './notification-message.interface';

@Injectable()
export class NotificationService {
    messageIn: Subject<NotificationMessage>;


    constructor() {
        this.messageIn = new Subject();
    }

    display(notification: NotificationMessage) {
        this.messageIn.next(notification);
    }

    clear() {
        this.messageIn.next({
            message: undefined,
            severity: undefined
        });
    }
}
