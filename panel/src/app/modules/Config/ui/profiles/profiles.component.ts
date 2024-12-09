import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import {
  MatPaginatorModule,
  MatPaginatorIntl,
  MatPaginator,
} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { GlobalService } from '../../../../core/services/global.service';
import { LoadingBackdropService } from '../../../../core/services/loading-backdrop.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { FilterComponent } from '../../../../shared/components/filter/filter.component';
import { SpanishPaginatorIntl } from '../../../../shared/material.paginator.es';
import { ProfileService } from '../../application/profile.service';
import { IProfile } from '../../domain/profile.model';

@Component({
  selector: 'app-profiles',
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
  templateUrl: './profiles.component.html',
  styleUrl: './profiles.component.scss',
  providers: [
    {
      provide: MatPaginatorIntl,
      useClass: SpanishPaginatorIntl,
    },
  ],
})
export class ProfilesComponent implements OnInit {
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  _activatedRoute!: ActivatedRoute;
  dataSource = new MatTableDataSource<IProfile>();
  displayedColumns: string[] = ['name', 'abbreviation', 'isActive', 'actions'];
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    public globalService: GlobalService,
    private loadingBackdropService: LoadingBackdropService,
    private notificationService: NotificationService,
    private profileService: ProfileService
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
    this.profileService.getAll().subscribe({
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
