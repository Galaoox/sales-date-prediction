import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSortModule, MatSort, Sort } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';

import { OrderService } from '@core/services/order.service';
import {
  ClientOrderDto,
  OrderParametersDto,
  PaginatedResult,
  SortColumnOptions,
  SortOrderOptions
} from '@core/models/order.models';

@Component({
  selector: 'app-customer-orders',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatTableModule,
    MatButtonModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatIconModule
  ],
  templateUrl: './customer-orders.component.html',
  styleUrls: ['./customer-orders.component.scss']
})
export class CustomerOrdersComponent implements OnInit {
  @ViewChild(MatSort, { static: false }) sort!: MatSort;

  displayedColumns: string[] = ['orderid', 'requireddate', 'shippeddate', 'shipname', 'shipaddress', 'shipcity'];
  dataSource: ClientOrderDto[] = [];
  totalItems = 0;
  pageSize = 5;
  pageIndex = 0;
  isLoading = true;
  sortColumn: SortColumnOptions = SortColumnOptions.OrderId;
  sortOrder: SortOrderOptions = SortOrderOptions.Ascending;

  constructor(
    public dialogRef: MatDialogRef<CustomerOrdersComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { customerId: number, customerName: string },
    private orderService: OrderService
  ) { }

  ngOnInit(): void {
    this.loadOrders();
  }



  loadOrders(): void {
    this.isLoading = true;

    const params: OrderParametersDto = {
      pageNumber: this.pageIndex + 1,
      pageSize: this.pageSize,
      sortColumn: this.sort?.active as SortColumnOptions,
      sortOrder: this.getSortOrder(this.sort?.direction)
    };

    this.orderService.getCustomerOrders(this.data.customerId, params).subscribe({
      next: (result: PaginatedResult<ClientOrderDto>) => {
        this.dataSource = result.items;
        this.totalItems = result.totalCount;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading orders:', error);
        this.isLoading = false;
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadOrders();
  }

  onSortChange(sortState: Sort): void {
    this.loadOrders();
  }

  private getSortColumn(column: string): SortColumnOptions {
    switch (column) {
      case 'orderid': return SortColumnOptions.OrderId;
      case 'requireddate': return SortColumnOptions.RequiredDate;
      case 'shippeddate': return SortColumnOptions.ShippedDate;
      case 'shipname': return SortColumnOptions.ShipName;
      case 'shipaddress': return SortColumnOptions.ShipAddress;
      case 'shipcity': return SortColumnOptions.ShipCity;
      default: return SortColumnOptions.OrderId;
    }
  }

  private getSortOrder(direction: string): SortOrderOptions | undefined {
    if (!direction) return undefined;
    return direction === 'asc' ? SortOrderOptions.Ascending : SortOrderOptions.Descending;
  }

  onClose(): void {
    this.dialogRef.close();
  }
}