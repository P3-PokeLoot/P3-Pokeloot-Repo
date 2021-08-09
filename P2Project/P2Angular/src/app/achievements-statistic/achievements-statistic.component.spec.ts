import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AchievementsStatisticComponent } from './achievements-statistic.component';

describe('AchievementsStatisticComponent', () => {
  let component: AchievementsStatisticComponent;
  let fixture: ComponentFixture<AchievementsStatisticComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AchievementsStatisticComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AchievementsStatisticComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
