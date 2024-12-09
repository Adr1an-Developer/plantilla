import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import {
  MatPaginator,
  MatPaginatorIntl,
  MatPaginatorModule,
} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { IUser } from '../../domain/user.model';
import { SpanishPaginatorIntl } from '../../../../shared/material.paginator.es';
import { FilterComponent } from '../../../../shared/components/filter/filter.component';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalService } from '../../../../core/services/global.service';
import { LoadingBackdropService } from '../../../../core/services/loading-backdrop.service';
import { UserService } from '../../application/user.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { AuthService } from '../../../../core/services/auth.service';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    FilterComponent,
    MatIconModule,
    MatMenuModule,
    MatButtonModule,
  ],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss',
  providers: [
    {
      provide: MatPaginatorIntl,
      useClass: SpanishPaginatorIntl,
    },
  ],
})
export class UsersComponent implements OnInit {
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  _activatedRoute!: ActivatedRoute;
  dataSource = new MatTableDataSource<IUser>();
  displayedColumns: string[] = [
    'username',
    'Fullname',
    'email',
    'profileName',
    'isActive',
    'actions',
  ];

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    public globalService: GlobalService,
    private loadingBackdropService: LoadingBackdropService,
    private notificationService: NotificationService,
    private userService: UserService
  ) {
    this._activatedRoute = activatedRoute;
  }
  async ngOnInit(): Promise<void> {
    await this.onListUsers();
  }

  applyFilter(event: any) {
    this.dataSource.filter = event.trim().toLowerCase();
  }

  onDetailNavigate(id: any) {
    this.router.navigate([id], { relativeTo: this._activatedRoute });
  }

  async onListUsers() {
    this.loadingBackdropService.show();
    this.userService.getAll().subscribe({
      next: (resp) => {
        if (resp.error) {
          this.notificationService.add({
            type: resp.messageType,
            title: 'Información',
            message: resp.messages[0],
          });
        }
        this.dataSource.data = resp.results;
      },
      error: (e) => {
        console.error(e);

        let err = e.error;
        this.notificationService.add({
          type: 'Error',
          title: 'Error Grave',
          message: e.message,
          time: 10000,
        });
        this.authService.logout();
        this.loadingBackdropService.hide();
      },
      complete: () => {
        this.loadingBackdropService.hide();
      },
    });
  }
}
