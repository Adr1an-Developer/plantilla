import { Component } from '@angular/core';
import { NotificationService } from '../../../../core/services/notification.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {
  /**
   *
   */
  constructor(public notificationService: NotificationService) {}

  showSuccessToaster() {
    this.notificationService.add({
      type: 'Success',
      title: 'Well done!',
      message: 'This is a success alert',
    });
  }
}
