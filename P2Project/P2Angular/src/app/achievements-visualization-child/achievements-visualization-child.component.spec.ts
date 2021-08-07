import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AchievementsVisualizationChildComponent } from './achievements-visualization-child.component';

describe('AchievementsVisualizationChildComponent', () => {
  let component: AchievementsVisualizationChildComponent;
  let fixture: ComponentFixture<AchievementsVisualizationChildComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AchievementsVisualizationChildComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AchievementsVisualizationChildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
