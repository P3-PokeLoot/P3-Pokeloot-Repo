import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CapGameWaitComponent } from './cap-game-wait.component';

describe('CapGameWaitComponent', () => {
  let component: CapGameWaitComponent;
  let fixture: ComponentFixture<CapGameWaitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CapGameWaitComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CapGameWaitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
