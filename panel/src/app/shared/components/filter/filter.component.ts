import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-filter',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatExpansionModule,
    MatTooltipModule,
    FormsModule,
  ],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.scss',
})
export class FilterComponent {
  @Input() activatedRoute: ActivatedRoute | any;
  @Input() showNew: boolean | null = false;
  @Input() showAdvancedFilters: boolean | null = false;
  @Input() filters: string = '';
  @Output('filter') filterValue: EventEmitter<string> = new EventEmitter();
  activateFilter: boolean = false;
  value: string = '';

  constructor(private _activatedRoute: ActivatedRoute, private router: Router) {
    _activatedRoute = this.activatedRoute;
  }

  onClear() {
    this.value = '';
    this.filterValue.emit(this.value);
  }

  onApplyFilter(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.filterValue.emit(value);
  }
  onAddNavigate() {
    this.router.navigate(['new'], { relativeTo: this._activatedRoute });
  }
}
