import { CommonModule } from '@angular/common';
import { Component, DoCheck, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalService } from '../../../../../core/services/global.service';
import { LoadingBackdropService } from '../../../../../core/services/loading-backdrop.service';
import { NotificationService } from '../../../../../core/services/notification.service';
import { MatDialog } from '@angular/material/dialog';
import { ProfileService } from '../../../application/profile.service';
import { UserService } from '../../../application/user.service';
import { IAddUser, IUser } from '../../../domain/user.model';
import { IProfile } from '../../../domain/profile.model';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SelectFilterComponent } from '../../../../../shared/components/select-filter/select-filter.component';
import { AuthService } from '../../../../../core/services/auth.service';
import { ConfirmDialogComponent } from '../../../../../shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-user-detail',
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
    SelectFilterComponent,
  ],
  templateUrl: './user-detail.component.html',
  styleUrl: './user-detail.component.scss',
})
export class UserDetailComponent implements OnInit {
  id: string;
  ppalForm!: FormGroup;
  title: string = 'Nuevo Usuario';
  isNew: boolean = true;
  userData!: IUser;
  profileData!: IProfile[];
  profileSelected: string = '';

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
    private profileService: ProfileService,
    private userService: UserService
  ) {
    this.activatedRoute = activatedRoute;
    this.id = this.activatedRoute.snapshot.params['id'];
    this.initFormBuilder();
  }
  async ngOnInit(): Promise<void> {
    await this.onGetProfiles();
    if (this.id) {
      this.title = 'Modificar Usuario';
      await this.onGetById();
      this.isNew = false;
    }
  }

  private initFormBuilder() {
    this.ppalForm = new FormGroup({
      userName: new FormControl(
        { value: '', disabled: false },
        Validators.required
      ),
      firstName: new FormControl(
        { value: '', disabled: false },
        Validators.required
      ),
      lastName: new FormControl(
        { value: '', disabled: false },
        Validators.required
      ),
      email: new FormControl({ value: '', disabled: false }, [
        Validators.email,
        Validators.required,
      ]),
      profileId: new FormControl(
        { value: '', disabled: false },
        Validators.required
      ),
      externalCode: new FormControl({ value: '', disabled: false }),
      isActive: new FormControl({ value: true, disabled: false }),
    });
  }

  private setFormData(data: IUser) {
    this.ppalForm.setValue({
      userName: data.userName,
      profileId: data.profileId,
      firstName: data.firstName,
      lastName: data.lastName,
      email: data.email,
      externalCode: data.externalCode,
      isActive: data.isActive,
    });
  }

  public isFormInvalid(): boolean {
    return this.ppalForm.invalid;
  }

  public isFormPristine(): boolean {
    return this.ppalForm.pristine;
  }

  async onGetById() {
    this.loadingBackdropService.show();
    this.userService.getById(this.id).subscribe({
      next: (resp) => {
        if (resp.error) {
          this.notificationService.add({
            type: resp.messageType,
            title: 'Información',
            message: resp.messages[0],
          });
          return;
        }
        this.userData = resp.result;
        this.setFormData(this.userData);
        this.profileSelected = this.userData.profileId;
      },
      error: (e) => {
        console.error(e);

        let err = e.error;
        this.notificationService.add({
          type: 'Error',
          title: 'Información',
          message: err.messages,
          time: 6000,
        });
        this.loadingBackdropService.hide();
      },
      complete: () => {
        this.loadingBackdropService.hide();
      },
    });
  }

  async onGetProfiles() {
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
        this.profileData = resp.results;
      },
      error: (e) => {
        console.error(e);

        let err = e.error;
        this.notificationService.add({
          type: 'Error',
          title: 'Información',
          message: err.messages,
          time: 6000,
        });
        this.loadingBackdropService.hide();
      },
      complete: () => {
        this.loadingBackdropService.hide();
      },
    });
  }
  onProfileSelected($event: any) {
    let selectedItem: IProfile = $event ?? '';
    this.ppalForm.patchValue({
      profileId: selectedItem.id,
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
    const formData: IUser = this.ppalForm.getRawValue();
    let data: IAddUser = {
      firstName: formData.firstName,
      lastName: formData.lastName,
      email: formData.email,
      userName: formData.userName,
      profileId: formData.profileId,
      externalCode: formData.externalCode,
    };

    this.loadingBackdropService.show();
    this.userService.Add(data).subscribe({
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
    const formData: IUser = this.ppalForm.getRawValue();
    this.userData.firstName = formData.firstName;
    this.userData.lastName = formData.lastName;
    this.userData.email = formData.email;
    this.userData.userName = formData.userName;
    this.userData.externalCode = formData.externalCode;
    this.userData.isActive = formData.isActive;

    this.loadingBackdropService.show();
    this.userService.Update(this.id, this.userData).subscribe({
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
    this.userService.Delete(this.id).subscribe({
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
