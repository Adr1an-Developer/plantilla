import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormsModule,
  ReactiveFormsModule,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../../core/services/auth.service';
import { GlobalService } from '../../../../../core/services/global.service';
import { LoadingBackdropService } from '../../../../../core/services/loading-backdrop.service';
import { NotificationService } from '../../../../../core/services/notification.service';
import { ConfirmDialogComponent } from '../../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { ProfileService } from '../../../application/profile.service';
import { IProfile, IAddProfile } from '../../../domain/profile.model';

@Component({
  selector: 'app-profiles-detail',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule,
  ],
  templateUrl: './profiles-detail.component.html',
  styleUrl: './profiles-detail.component.scss',
})
export class ProfilesDetailComponent implements OnInit {
  id: string;
  ppalForm!: FormGroup;
  title: string = 'Nuevo Perfil';
  isNew: boolean = true;
  profile!: IProfile;

  /**
   *
   */
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public globalService: GlobalService,
    private authService: AuthService,
    private loadingBackdropService: LoadingBackdropService,
    private notificationService: NotificationService,
    private dialog: MatDialog,
    private profileService: ProfileService
  ) {
    this.activatedRoute = activatedRoute;
    this.id = this.activatedRoute.snapshot.params['id'];
    this.initFormBuilder();
  }
  async ngOnInit(): Promise<void> {
    if (this.id) {
      this.title = 'Modificar Perfil';
      await this.onGetbyId();
      this.isNew = false;
    }
  }

  public isFormInvalid(): boolean {
    return this.ppalForm.invalid;
  }

  public isFormPristine(): boolean {
    return this.ppalForm.pristine;
  }

  private initFormBuilder() {
    this.ppalForm = new FormGroup({
      name: new FormControl(
        { value: null, disabled: false },
        Validators.required
      ),
      abbreviation: new FormControl(
        { value: null, disabled: false },
        Validators.required
      ),
      is_active: new FormControl(
        { value: true, disabled: false },
        Validators.required
      ),
    });
  }

  private setFormData(data: IProfile) {
    this.ppalForm.setValue({
      name: data.name,
      abbreviation: data.abbreviation,
      is_active: data.isActive,
    });
  }

  async onGetbyId() {
    this.loadingBackdropService.show();
    this.profileService.getById(this.id ?? '').subscribe({
      next: (resp) => {
        if (resp.error) {
          this.notificationService.add({
            type: resp.messageType,
            title: 'Información',
            message: resp.messages[0],
          });
          return;
        }
        this.profile = resp.result;

        this.setFormData(resp.result);
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

  async onSave() {
    if (this.isNew) {
      await this.onAdd();
    } else {
      await this.onUpdate();
    }
  }

  async onAdd() {
    const formData = this.ppalForm.getRawValue();
    let data: IAddProfile = {
      name: formData.name,
      abbreviation: formData.abbreviation,
    };

    this.loadingBackdropService.show();
    this.profileService.Add(data).subscribe({
      next: (resp) => {
        if (resp.error) {
          this.notificationService.add({
            type: resp.messageType,
            title: 'Información',
            message: resp.messages[0],
          });
          return;
        }
        this.router.navigate(['..'], { relativeTo: this.activatedRoute });
        this.notificationService.add({
          type: resp.messageType,
          title: 'Información',
          message: resp.messages[0],
        });
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
  async onUpdate() {
    const formData = this.ppalForm.getRawValue();
    this.profile.name = formData.name;
    this.profile.abbreviation = formData.abbreviation;
    this.profile.isActive = formData.is_active;

    this.loadingBackdropService.show();
    this.profileService.Update(this.id, this.profile).subscribe({
      next: (resp) => {
        if (resp.error) {
          this.notificationService.add({
            type: resp.messageType,
            title: 'Información',
            message: resp.messages[0],
          });
          return;
        }
        this.router.navigate(['..'], { relativeTo: this.activatedRoute });
        this.notificationService.add({
          type: resp.messageType,
          title: 'Información',
          message: resp.messages[0],
        });
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

  confirmDelete() {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Confirmar',
        body: '¿Está seguro de que desea eliminar este Usuario?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.onDelete();
      }
    });
  }

  async onDelete() {
    this.loadingBackdropService.show();
    this.profileService.Delete(this.id).subscribe({
      next: (resp) => {
        if (resp.error) {
          this.notificationService.add({
            type: resp.messageType,
            title: 'Información',
            message: resp.messages[0],
          });
          return;
        }
        this.router.navigate(['..'], { relativeTo: this.activatedRoute });
        this.notificationService.add({
          type: resp.messageType,
          title: 'Información',
          message: resp.messages[0],
        });
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
