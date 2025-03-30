import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpErrorInterceptor } from './core/interceptors/http-error.interceptor';

import { routes } from './app.routes';
import { provideNativeDateAdapter } from '@angular/material/core';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideNativeDateAdapter(),
    // provideAnimations(),
    provideHttpClient(withInterceptors([HttpErrorInterceptor]))
  ]
};