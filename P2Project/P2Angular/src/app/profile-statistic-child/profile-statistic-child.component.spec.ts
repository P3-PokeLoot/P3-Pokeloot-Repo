import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileStatisticChildComponent } from './profile-statistic-child.component';

describe('ProfileStatisticChildComponent', () => {
  let component: ProfileStatisticChildComponent;
  let fixture: ComponentFixture<ProfileStatisticChildComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileStatisticChildComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileStatisticChildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
