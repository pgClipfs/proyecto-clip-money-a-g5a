import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepositocuentaComponent } from './depositocuenta.component';

describe('DepositocuentaComponent', () => {
  let component: DepositocuentaComponent;
  let fixture: ComponentFixture<DepositocuentaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DepositocuentaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepositocuentaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
