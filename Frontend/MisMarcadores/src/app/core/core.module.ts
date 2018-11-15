import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastNotificationComponent } from './toast-notification/toast-notification.component';
import { NotificationService } from './notification.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [ToastNotificationComponent],
  providers: [
    NotificationService
  ],
  exports: [
    ToastNotificationComponent
  ]
})
export class CoreModule { }
