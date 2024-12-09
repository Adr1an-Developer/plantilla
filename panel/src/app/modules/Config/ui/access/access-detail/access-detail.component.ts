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
import { SelectFilterComponent } from '../../../../../shared/components/select-filter/select-filter.component';
import { MenuService } from '../../../application/menu.service';
import { IMenu } from '../../../domain/menu.model';
import { IAccess, IAddAccess } from '../../../domain/access.model';
import { AccessService } from '../../../application/access.service';
import { IProfile } from '../../../domain/profile.model';
import { ProfileService } from '../../../application/profile.service';

@Component({
  selector: 'app-access-detail',
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
  templateUrl: './access-detail.component.html',
  styleUrl: './access-detail.component.scss',
})
export class AccessDetailComponent implements OnInit {
  id: string;
  ppalForm!: FormGroup;
  title: string = 'Nuevo Acceso';
  isNew: boolean = true;
  accessData!: IAccess;
  menuData!: IMenu;
  allMenuParentData!: IMenu[];
  menuParentSelected: string = '';
  allProfileData!: IProfile[];
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
    private menuService: MenuService,
    private profileService: ProfileService,
    private accessService: AccessService
  ) {
    this.activatedRoute = activatedRoute;
    this.id = this.activatedRoute.snapshot.params['id'];
    this.initFormBuilder();
  }
  async ngOnInit(): Promise<void> {
    await this.onGetMenuParent();
    await this.onGetProfiles();
    if (this.id) {
      this.title = 'Modificar Acceso';
      await this.onGetById();
      this.isNew = false;
    }
  }

  private initFormBuilder() {
    this.ppalForm = new FormGroup({
      menuId: new FormControl(
        { value: null, disabled: false },
        Validators.required
      ), // 'name' is also required
      profileId: new FormControl(
        { value: null, disabled: false },
        Validators.required
      ), // 'url' is required
      canView: new FormControl(
        { value: true, disabled: false },
        Validators.required
      ), // 'title' is required
      canAdd: new FormControl(
        { value: false, disabled: false },
        Validators.required
      ), // 'icon' might not be required
      canEdit: new FormControl(
        { value: false, disabled: false },
        Validators.required
      ), // 'icon' might not be required
      canDelete: new FormControl(
        { value: false, disabled: false },
        Validators.required
      ), // 'icon' might not be required
      canExport: new FormControl(
        { value: false, disabled: false },
        Validators.required
      ), // 'icon' might not be required
      canAuthorize: new FormControl(
        { value: false, disabled: false },
        Validators.required
      ), // 'icon' might not be required
      isActive: new FormControl({ value: true, disabled: false }), // 'isActive' is required with a default true
    });
  }

  private setFormData(data: IAccess) {
    this.ppalForm.setValue({
      menuId: data.menuId,
      profileId: data.profileId,
      canView: data.canView,
      canAdd: data.canAdd,
      canEdit: data.canEdit,
      canDelete: data.canDelete,
      canExport: data.canExport,
      canAuthorize: data.canAuthorize,
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
    this.accessService.getById(this.id).subscribe({
      next: (resp) => {
        if (resp.error) {
          this.notificationService.add({
            type: resp.messageType,
            title: 'Información',
            message: resp.messages[0],
          });
          return;
        }
        this.accessData = resp.result;
        this.setFormData(this.accessData);
        this.menuParentSelected = this.accessData.menuId ?? '';
        this.profileSelected = this.accessData.profileId ?? '';
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

  async onGetMenuParent() {
    this.loadingBackdropService.show();
    this.menuService.getAll().subscribe({
      next: (resp) => {
        if (resp.error) {
          this.notificationService.add({
            type: resp.messageType,
            title: 'Información',
            message: resp.messages[0],
          });
        }

        this.allMenuParentData = resp.results;
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

  onMenuSelected($event: any) {
    let selectedItem: IMenu = $event ?? null;
    this.ppalForm.patchValue({
      menuId: $event != null ? selectedItem.id : null,
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

        this.allProfileData = resp.results;
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
    let selectedItem: IAccess = $event ?? null;
    this.ppalForm.patchValue({
      profileId: $event != null ? selectedItem.id : null,
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
    const formData: IAccess = this.ppalForm.getRawValue();
    let data: IAddAccess = {
      menuId: formData.menuId,
      profileId: formData.profileId,
      canView: formData.canView,
      canAdd: formData.canAdd,
      canEdit: formData.canEdit,
      canDelete: formData.canDelete,
      canExport: formData.canExport,
      canAuthorize: formData.canAuthorize,
    };

    this.loadingBackdropService.show();
    this.accessService.Add(data).subscribe({
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
    const formData: IAccess = this.ppalForm.getRawValue();
    this.accessData.menuId = formData.menuId;
    this.accessData.profileId = formData.profileId;
    this.accessData.canView = formData.canView;
    this.accessData.canAdd = formData.canAdd;
    this.accessData.canEdit = formData.canEdit;
    this.accessData.canDelete = formData.canDelete;
    this.accessData.canExport = formData.canExport;
    this.accessData.canAuthorize = formData.canAuthorize;
    this.accessData.isActive = formData.isActive;

    this.loadingBackdropService.show();
    this.accessService.Update(this.id, this.accessData).subscribe({
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
        body: '¿Está seguro de que desea eliminar este Permiso de Acceso?',
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
    this.accessService.Delete(this.id).subscribe({
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
