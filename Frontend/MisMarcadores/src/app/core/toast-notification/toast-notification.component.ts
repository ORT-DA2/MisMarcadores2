import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../notification.service';
import { NotificationMessage } from '../notification-message.interface';

@Component({
  selector: 'app-toast-notification',
  templateUrl: './toast-notification.component.html',
  styleUrls: ['./toast-notification.component.css']
})
export class ToastNotificationComponent implements OnInit {

  message: string;
  messageClass: string;

  constructor(private notifyService: NotificationService) {
    this.messageClass = 'alert alert-success';
    this.message = 'pepepepepepe';
  }

  ngOnInit() {
    this.notifyService.messageIn.subscribe((n: NotificationMessage) => {
      this.message = n.message;
      this.messageClass = this.getClass(n.severity);

    });
  }

  delete() {
    this.message = undefined;
  }

  private getClass(s: string) {
    switch (s) {
      case 'info':
        return 'alert alert-success';
      case 'error':
        return 'alert alert-danger';
      case 'warn':
        return 'alert alert-warning';
      default:
        return 'alert alert-info';
    }
  }

}
