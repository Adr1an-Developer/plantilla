import { CommonModule } from '@angular/common';
import {
  AfterViewInit,
  Component,
  DoCheck,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewEncapsulation,
  input,
} from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-select-filter',
  standalone: true,
  imports: [
    CommonModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatSelectModule,
    ReactiveFormsModule,
  ],
  templateUrl: './select-filter.component.html',
  styleUrl: './select-filter.component.scss',
  encapsulation: ViewEncapsulation.None,
})
export class SelectFilterComponent implements OnInit, DoCheck {
  @Input() data: any[] = [];
  @Input() label: string = 'label';
  @Input() setValue: any = {};
  @Input() setDisable: boolean = false;
  @Output() ItemSelected = new EventEmitter<any | null>();

  ItemId = new FormControl('');
  ItemFiltroControl = new FormControl('');
  FilteredItems: any[] = [];
  Id: string = '';
  /**
   *
   */
  constructor() {}
  ngDoCheck(): void {
    this.loadData();
    this.setIdValue();

    // if (!this.setDisable) {
    //   this.clear();
    //   this.setValue = {};
    // }
    this.SetDisabled();
  }

  ngOnInit(): void {
    // Suscribirse a cambios en el FormControl para actualizar las opciones filtradas
    this.ItemFiltroControl.valueChanges.subscribe((valor) => {
      this.filtrarOpciones(valor || '');
    });
  }

  filtrarOpciones(filtro: string): void {
    if (this.data == undefined) return;
    const filtroEnMinusculas = filtro.toLowerCase();
    this.FilteredItems = this.data.filter((opcion) =>
      opcion.name.toLowerCase().includes(filtroEnMinusculas)
    );
  }

  loadData() {
    let text: string = this.ItemFiltroControl.value || '';
    this.filtrarOpciones(text);
  }

  getItem(event: any | null) {
    const selectedType = this.data.find((row) => row.id === event.value);

    this.ItemSelected.emit(selectedType || null);
  }

  clear() {
    this.ItemFiltroControl.setValue('');
  }

  setIdValue() {
    let row = this.setValue;
    if (row) this.Id = row ?? '';
  }

  SetDisabled() {
    if (this.setDisable) {
      this.ItemId.disable();
    } else {
      this.ItemId.enable();
    }
  }
}
