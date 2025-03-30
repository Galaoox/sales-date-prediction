import { Routes } from '@angular/router';
import { SalesPredictionComponent } from './features/customer/pages/sales-prediction/sales-prediction.component';

export const routes: Routes = [
    {
        path: '',
        component: SalesPredictionComponent
    },
    {
        path: '**',
        redirectTo: ''
    }
];