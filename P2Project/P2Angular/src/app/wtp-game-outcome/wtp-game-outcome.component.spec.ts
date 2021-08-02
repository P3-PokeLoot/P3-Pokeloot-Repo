import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WtpGameOutcomeComponent } from './wtp-game-outcome.component';

describe('WtpGameOutcomeComponent', () => {
  let component: WtpGameOutcomeComponent;
  let fixture: ComponentFixture<WtpGameOutcomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WtpGameOutcomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WtpGameOutcomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
