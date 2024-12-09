// import { MatPaginatorIntl } from '@angular/material/paginator';
// export function getPaginatorIntl() {
//   const paginatorIntl = new MatPaginatorIntl();
//   paginatorIntl.itemsPerPageLabel = 'Registros por página:';
//   paginatorIntl.nextPageLabel = 'Página siguiente ';
//   paginatorIntl.previousPageLabel = 'Página anterior';
//   paginatorIntl.firstPageLabel = 'Primera página';
//   paginatorIntl.lastPageLabel = 'Última página';
//   return paginatorIntl;

import { Injectable } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { Subject } from 'rxjs';

@Injectable()
export class SpanishPaginatorIntl extends MatPaginatorIntl {
  override changes = new Subject<void>();

  override itemsPerPageLabel = 'Items por Página:';
  override nextPageLabel = 'Siguiente página';
  override previousPageLabel = 'Página anterior';

  override getRangeLabel = (
    page: number,
    pageSize: number,
    length: number
  ): string => {
    if (length === 0) {
      return `Página 1 de 1`;
    }
    const amountPages = Math.ceil(length / pageSize);
    return `Página ${page + 1} de ${amountPages}`;
  };
}
