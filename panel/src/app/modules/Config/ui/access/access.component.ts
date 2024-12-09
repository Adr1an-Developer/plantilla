import { Component, OnInit, ViewChild } from '@angular/core';
import { ProfileService } from '../../application/profile.service';
import {
  MatPaginator,
  MatPaginatorIntl,
  MatPaginatorModule,
} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { GlobalService } from '../../../../core/services/global.service';
import { LoadingBackdropService } from '../../../../core/services/loading-backdrop.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { IProfile } from '../../domain/profile.model';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { FilterComponent } from '../../../../shared/components/filter/filter.component';
import { SpanishPaginatorIntl } from '../../../../shared/material.paginator.es';
import { AccessService } from '../../application/access.service';
import { IAccess } from '../../domain/access.model';

@Component({
  selector: 'app-access',
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
  templateUrl: './access.component.html',
  styleUrl: './access.component.scss',
  providers: [
    {
      provide: MatPaginatorIntl,
      useClass: SpanishPaginatorIntl,
    },
  ],
})
export class AccessComponent implements OnInit {
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  _activatedRoute!: ActivatedRoute;
  dataSource = new MatTableDataSource<IAccess>();
  displayedColumns: string[] = [
    'profileName',
    'menuName',
    'canView',
    'canAdd',
    'canEdit',
    'canDelete',
    'canExport',
    'canAuthorize',
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
    private profileService: ProfileService,
    private accessService: AccessService
  ) {
    this._activatedRoute = activatedRoute;
  }

  async ngOnInit(): Promise<void> {
    await this.onList();
  }

  applyFilter(event: any) {
    this.dataSource.filter = event.trim().toLowerCase();
  }

  onDetailNavigate(id: any) {
    this.router.navigate([id], { relativeTo: this._activatedRoute });
  }

  async onList() {
    this.loadingBackdropService.show();
    this.accessService.getAll().subscribe({
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
