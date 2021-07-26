import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TradeCardPageComponent } from './trade-card-page.component';

describe('TradeCardPageComponent', () => {
  let component: TradeCardPageComponent;
  let fixture: ComponentFixture<TradeCardPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TradeCardPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TradeCardPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
