import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSortModule, MatSort, Sort } from '@angular/material/sort';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

import { OrderService } from '@core/services/order.service';
import {
  OrderPredictionParametersDto,
  PaginatedResult,
  CustomerOrderPredictionDto,
  OrderPredictionSortColumnOptions,
  OrderPredictionSortOrderOptions
} from '@core/models/order.models';
import { CustomerOrdersComponent } from '../customer-orders/customer-orders.component';
import { NewOrderComponent } from '../new-order/new-order.component';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-customer-order-prediction-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatIconModule
  ],
  templateUrl: './customer-order-prediction-list.component.html',
  styleUrls: ['./customer-order-prediction-list.component.scss']
})
export class CustomerOrderPredictionListComponent implements OnInit {
  displayedColumns: string[] = ['companyName', 'lastOrderDate', 'nextPredictedOrder', 'actions'];
  dataSource = new MatTableDataSource<CustomerOrderPredictionDto>([]);
  totalItems = 0;
  pageSize = 10;
  pageIndex = 0;
  searchControl = new FormControl('');

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private orderService: OrderService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.loadPredictions();

    this.searchControl.valueChanges
      .pipe(
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.pageIndex = 0;
        this.loadPredictions();
      });
  }

  loadPredictions(): void {
    const params: OrderPredictionParametersDto = {
      pageNumber: this.pageIndex + 1,
      pageSize: this.pageSize,
      search: this.searchControl.value || undefined,
      sortColumn: this.sort?.active as OrderPredictionSortColumnOptions,
      sortOrder: this.getSortOrder(this.sort?.direction)
    };

    this.orderService.getOrderPredictions(params).subscribe({
      next: (result: PaginatedResult<CustomerOrderPredictionDto>) => {
        this.dataSource.data = result.items;
        this.totalItems = result.totalCount;
      },
      error: (error) => console.error('Error loading predictions:', error)
    });
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadPredictions();
  }

  onSortChange(sortState: Sort): void {
    this.loadPredictions();
  }

  private getSortOrder(direction: string): OrderPredictionSortOrderOptions | undefined {
    if (!direction) return undefined;
    return direction === 'asc' ? OrderPredictionSortOrderOptions.Ascending : OrderPredictionSortOrderOptions.Descending;
  }

  viewOrders(customerId: number, customerName: string): void {
    this.dialog.open(CustomerOrdersComponent, {
      minWidth: '1000px',
      panelClass: 'custom-modalbox',
      data: { customerId, customerName }
    });
  }

  createNewOrder(customerId: number, customerName: string): void {
    const dialogRef = this.dialog.open(NewOrderComponent, {
      minWidth: '700px',
      data: { customerId, customerName }

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result?.success) {
        console.log(result.message); // TODO: mejora implementar notificaciones para indicarle al usuario que se creo el registro
        this.loadPredictions();
      }
    });
  }
}