import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplaystatisticsComponent } from './displaystatistics.component';

describe('DisplaystatisticsComponent', () => {
  let component: DisplaystatisticsComponent;
  let fixture: ComponentFixture<DisplaystatisticsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisplaystatisticsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplaystatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
