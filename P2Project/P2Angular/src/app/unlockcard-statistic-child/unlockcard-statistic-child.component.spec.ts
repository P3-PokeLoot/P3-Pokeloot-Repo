import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnlockCardStatisticChildComponent } from './unlockcard-statistic-child.component';

describe('ProfileStatisticChildComponent', () => {
  let component: UnlockCardStatisticChildComponent;
  let fixture: ComponentFixture<UnlockCardStatisticChildComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnlockCardStatisticChildComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnlockCardStatisticChildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
