import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerOrderPredictionListComponent } from './customer-order-prediction-list.component';

describe('CustomerOrderPredictionListComponent', () => {
  let component: CustomerOrderPredictionListComponent;
  let fixture: ComponentFixture<CustomerOrderPredictionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerOrderPredictionListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomerOrderPredictionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
