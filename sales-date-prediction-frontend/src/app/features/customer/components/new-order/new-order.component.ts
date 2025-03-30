import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { OrderService } from '@core/services/order.service';
import { ProductService } from '@core/services/product.service';
import { EmployeeService } from '@core/services/employee.service';
import { ShipperService } from '@core/services/shipper.service';
import { CreateOrderParametersDto, OrderDetailDto } from '@core/models/order.models';
import { Employee } from '@app/core/models/employee.models';
import { Shipper } from '@app/core/models/shipper.models';
import { Product } from '@app/core/models/product.models';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-new-order',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatDividerModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatIconModule
  ],
  templateUrl: './new-order.component.html',
  styleUrls: ['./new-order.component.scss']
})
export class NewOrderComponent implements OnInit {
  orderForm: FormGroup;
  employees: Employee[] = [];
  shippers: Shipper[] = [];
  products: Product[] = [];

  constructor(
    public dialogRef: MatDialogRef<NewOrderComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { customerId: number },
    private fb: FormBuilder,
    private orderService: OrderService,
    private productService: ProductService,
    private employeeService: EmployeeService,
    private shipperService: ShipperService
  ) {
    this.orderForm = this.fb.group({
      // Sección Order
      employeeId: ['', Validators.required],
      shipperId: ['', Validators.required],
      shipName: ['', Validators.required],
      shipAddress: ['', Validators.required],
      shipCity: ['', Validators.required],
      shipCountry: ['', Validators.required],
      orderDate: ['', Validators.required],
      requiredDate: ['', Validators.required],
      shippedDate: [''],
      freight: ['', [Validators.required, Validators.min(0)]],

      // Sección Order Details
      productId: ['', Validators.required],
      unitPrice: ['', [Validators.required, Validators.min(0)]],
      quantity: ['', [Validators.required, Validators.min(1)]],
      discount: ['', [Validators.min(0), Validators.max(1), Validators.pattern(/^\d+(\.\d{1,3})?$/)]]
    });
  }

  ngOnInit(): void {
    this.loadDropdownData();
  }

  private loadDropdownData(): void {
    this.employeeService.getEmployees().subscribe(emps => this.employees = emps);
    this.shipperService.getShippers().subscribe(shps => this.shippers = shps);
    this.productService.getProducts().subscribe(prods => this.products = prods);
  }

  onSubmit(): void {
    if (this.orderForm.valid) {
      const formValue = this.orderForm.value;

      const orderDto: CreateOrderParametersDto = {
        order: {
          custid: this.data.customerId,
          empid: formValue.employeeId,
          shipperid: formValue.shipperId,
          shipname: formValue.shipName,
          shipaddress: formValue.shipAddress,
          shipcity: formValue.shipCity,
          shipcountry: formValue.shipCountry,
          orderdate: this.formatDateTime(formValue.orderDate),
          requireddate: this.formatDateTime(formValue.requiredDate),
          shippeddate: formValue.shippedDate ? this.formatDateTime(formValue.shippedDate) : '',
          freight: formValue.freight
        },
        orderDetailDtos: [{
          product_id: formValue.productId,
          unit_price: formValue.unitPrice,
          quantity: formValue.quantity,
          discount: formValue.discount || 0
        }]
      };

      this.orderService.createOrder(orderDto).subscribe({
        next: () => this.dialogRef.close({ success: true, message: 'Orden creada exitosamente' }),
        error: (err) => console.error('Error creating order:', err)
      });
    }
  }

  private formatDateTime(date: Date): string {
    return date.toISOString().slice(0, 10) + 'T00:00:00';
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}