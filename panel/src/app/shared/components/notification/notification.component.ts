import { Component } from '@angular/core';
import { NotificationService } from '../../../core/services/notification.service';
import { INotification } from '../../../core/infractucture/INotification';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.scss',
})
export class NotificationComponent {
  notifications: INotification[] = [];

  constructor(public notificationService: NotificationService) {
    notificationService.notifications$.subscribe((notifications) => {
      this.notifications = notifications;
    });
  }

  remove(index: number) {
    this.notificationService.remove(index);
  }
}
