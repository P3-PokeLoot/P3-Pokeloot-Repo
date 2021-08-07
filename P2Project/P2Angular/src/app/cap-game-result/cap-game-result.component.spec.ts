import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CapGameResultComponent } from './cap-game-result.component';

describe('CapGameResultComponent', () => {
  let component: CapGameResultComponent;
  let fixture: ComponentFixture<CapGameResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CapGameResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CapGameResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
