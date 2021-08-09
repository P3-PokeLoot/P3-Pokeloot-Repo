import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CapGameCatchComponent } from './cap-game-catch.component';

describe('CapGameCatchComponent', () => {
  let component: CapGameCatchComponent;
  let fixture: ComponentFixture<CapGameCatchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CapGameCatchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CapGameCatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
