import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import {
  OrderParametersDto,
  CreateOrderParametersDto,
  OrderPredictionParametersDto,
  PaginatedResult,
  ClientOrderDto,
  CustomerOrderPredictionDto
} from '@core/models/order.models';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = `${environment.apiUrl}/api/Orders`;

  constructor(private http: HttpClient) { }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'An unknown error occurred!';

    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }

  getCustomerOrders(customerId: number, params: OrderParametersDto): Observable<PaginatedResult<ClientOrderDto>> {
    return this.http.post<PaginatedResult<ClientOrderDto>>(`${this.apiUrl}/${customerId}`, params).pipe(
      catchError(this.handleError)
    );
  }

  getOrderPredictions(params: OrderPredictionParametersDto): Observable<PaginatedResult<CustomerOrderPredictionDto>> {
    return this.http.post<PaginatedResult<CustomerOrderPredictionDto>>(`${this.apiUrl}/predictions`, params).pipe(
      catchError(this.handleError)
    );
  }

  createOrder(orderParams: CreateOrderParametersDto): Observable<any> {
    return this.http.post<any>(this.apiUrl, orderParams).pipe(
      catchError(this.handleError)
    );
  }
}