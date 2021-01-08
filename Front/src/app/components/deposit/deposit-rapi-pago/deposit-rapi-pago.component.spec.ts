import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepositRapiPagoComponent } from './deposit-rapi-pago.component';

describe('DepositRapiPagoComponent', () => {
  let component: DepositRapiPagoComponent;
  let fixture: ComponentFixture<DepositRapiPagoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DepositRapiPagoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepositRapiPagoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
