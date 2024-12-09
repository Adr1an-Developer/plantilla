import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
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
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SelectFilterComponent } from '../../../../../shared/components/select-filter/select-filter.component';
import { IAddMenu, IMenu } from '../../../domain/menu.model';
import { MenuService } from '../../../application/menu.service';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../../core/services/auth.service';
import { GlobalService } from '../../../../../core/services/global.service';
import { LoadingBackdropService } from '../../../../../core/services/loading-backdrop.service';
import { NotificationService } from '../../../../../core/services/notification.service';
import { ConfirmDialogComponent } from '../../../../../shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-menu-detail',
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
  templateUrl: './menu-detail.component.html',
  styleUrl: './menu-detail.component.scss',
})
export class MenuDetailComponent implements OnInit {
  id: string;
  ppalForm!: FormGroup;
  title: string = 'Nuevo Menu';
  isNew: boolean = true;
  menuData!: IMenu;
  menuParentData!: IMenu;
  allMenuParentData!: IMenu[];
  menuParentSelected: string = '';
  activeParent: boolean = true;
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
    private menuService: MenuService
  ) {
    this.activatedRoute = activatedRoute;
    this.id = this.activatedRoute.snapshot.params['id'];
    this.initFormBuilder();
  }
  async ngOnInit(): Promise<void> {
    await this.onGetMenuParent();
    if (this.id) {
      this.title = 'Modificar Menu';
      await this.onGetById();
      this.isNew = false;
    }
    this.ActivateAgrupador();
  }

  private initFormBuilder() {
    this.ppalForm = new FormGroup({
      name: new FormControl(
        { value: null, disabled: false },
        Validators.required
      ), // 'name' is also required
      url: new FormControl(
        { value: null, disabled: false },
        Validators.required
      ), // 'url' is required
      title: new FormControl(
        { value: null, disabled: false },
        Validators.required
      ), // 'title' is required
      icon: new FormControl(
        { value: null, disabled: false },
        Validators.required
      ), // 'icon' might not be required
      parentMenuId: new FormControl({ value: null, disabled: false }), // Optional or nullable
      order: new FormControl({ value: null, disabled: false }, [
        Validators.required,
        Validators.min(1),
      ]), // 'order' is required with a minimum value
      isActive: new FormControl({ value: true, disabled: false }), // 'isActive' is required with a default true
      group: new FormControl({ value: false, disabled: false }), // 'isActive' is required with a default true
    });
  }

  public ActivateAgrupador() {
    this.activeParent = this.ppalForm.get('group')?.value;
  }

  private setFormData(data: IMenu) {
    this.ppalForm.setValue({
      name: data.name,
      url: data.url,
      title: data.title,
      icon: data.icon,
      parentMenuId: data.parentMenuId,
      order: data.order,
      isActive: data.isActive,
      group: data.group,
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
    this.menuService.getById(this.id).subscribe({
      next: (resp) => {
        if (resp.error) {
          this.notificationService.add({
            type: resp.messageType,
            title: 'Información',
            message: resp.messages[0],
          });
          return;
        }
        this.menuData = resp.result;
        this.setFormData(this.menuData);
        this.menuParentSelected = this.menuData.parentMenuId ?? '';
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

        let groupMenu: IMenu[] =
          resp.results.filter((rows) => rows.group == true) || [];

        this.allMenuParentData = groupMenu;
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
      parentMenuId: $event != null ? selectedItem.id : null,
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
    const formData: IMenu = this.ppalForm.getRawValue();
    let data: IAddMenu = {
      name: formData.name,
      title: formData.title,
      icon: formData.icon,
      group: formData.group,
      order: formData.order,
      url: formData.url,
      parentMenuId: formData.parentMenuId,
    };

    this.loadingBackdropService.show();
    this.menuService.Add(data).subscribe({
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
    const formData: IMenu = this.ppalForm.getRawValue();
    this.menuData.name = formData.name;
    this.menuData.title = formData.title;
    this.menuData.icon = formData.icon;
    this.menuData.url = formData.url;
    this.menuData.order = formData.order;
    this.menuData.group = formData.group;
    this.menuData.parentMenuId = formData.parentMenuId;
    this.menuData.isActive = formData.isActive;

    this.loadingBackdropService.show();
    this.menuService.Update(this.id, this.menuData).subscribe({
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
        body: '¿Está seguro de que desea eliminar este Menu?',
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
    this.menuService.Delete(this.id).subscribe({
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
