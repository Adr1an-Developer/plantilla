import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { INotification } from '../infractucture/INotification';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  notifications: INotification[] = [];
  delay = 6000;

  subject = new BehaviorSubject<INotification[]>([]);
  notifications$ = this.subject.asObservable();

  add(notification: INotification) {
    this.notifications = [notification, ...this.notifications];
    this.subject.next(this.notifications);
    let time = notification.time;

    switch (notification.type) {
      case 'Error':
        time = 10000;
        break;
      case 'Warning':
        time = 5000;
        break;
      case 'Info':
        time = 5000;
        break;
      default:
        time = 5000;
        break;
    }

    setTimeout(() => {
      this.notifications = this.notifications.filter((v) => v !== notification);
      this.subject.next(this.notifications);
    }, time);
  }

  remove(index: number) {
    this.notifications = this.notifications.filter(
      (notifications, i) => i !== index
    );
    this.subject.next(this.notifications);
  }
}
