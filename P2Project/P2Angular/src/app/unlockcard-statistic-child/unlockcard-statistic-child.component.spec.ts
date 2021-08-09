import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnlockcardStatisticChildComponent } from './unlockcard-statistic-child.component';

describe('UnlockcardStatisticChildComponent', () => {
  let component: UnlockcardStatisticChildComponent;
  let fixture: ComponentFixture<UnlockcardStatisticChildComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnlockcardStatisticChildComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnlockcardStatisticChildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
