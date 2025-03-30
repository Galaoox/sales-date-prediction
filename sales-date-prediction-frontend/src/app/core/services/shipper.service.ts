import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Shipper } from '../models/shipper.models';

@Injectable({
  providedIn: 'root'
})
export class ShipperService {
  private apiUrl = `${environment.apiUrl}/api/Shippers`;

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

  getShippers(): Observable<any[]> {
    return this.http.get<Shipper[]>(this.apiUrl).pipe(
      catchError(this.handleError)
    );
  }
}