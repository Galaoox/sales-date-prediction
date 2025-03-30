import { Component } from '@angular/core';
import { CustomerOrderPredictionListComponent } from '../../components/customer-order-prediction-list/customer-order-prediction-list.component';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  standalone: true,
  selector: 'app-sales-prediction',
  imports: [CustomerOrderPredictionListComponent, MatToolbarModule],
  templateUrl: './sales-prediction.component.html',
  styleUrl: './sales-prediction.component.scss'
})
export class SalesPredictionComponent {

}
