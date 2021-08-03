import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewBalancePageComponent } from './view-balance-page.component';

describe('ViewBalancePageComponent', () => {
  let component: ViewBalancePageComponent;
  let fixture: ComponentFixture<ViewBalancePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewBalancePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewBalancePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
