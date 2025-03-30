import {
    HttpRequest,
    HttpHandlerFn,
    HttpEvent,
    HttpErrorResponse,
    HttpInterceptorFn
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export const HttpErrorInterceptor: HttpInterceptorFn = (
    request: HttpRequest<unknown>,
    next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
    return next(request).pipe(
        catchError((error: HttpErrorResponse) => {
            let errorMsg = '';

            if (error.error instanceof ErrorEvent) {
                // Client-side error
                errorMsg = `Error: ${error.error.message}`;
            } else {
                // Server-side error
                errorMsg = `Error Code: ${error.status}, Message: ${error.message}`;
            }

            console.error(errorMsg);
            return throwError(() => new Error(errorMsg));
        })
    );
};