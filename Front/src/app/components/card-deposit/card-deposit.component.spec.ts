import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardDepositComponent } from './card-deposit.component';

describe('CardDepositComponent', () => {
  let component: CardDepositComponent;
  let fixture: ComponentFixture<CardDepositComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardDepositComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CardDepositComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
