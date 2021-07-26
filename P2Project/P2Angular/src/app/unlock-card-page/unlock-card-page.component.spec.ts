import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnlockCardPageComponent } from './unlock-card-page.component';

describe('UnlockCardPageComponent', () => {
  let component: UnlockCardPageComponent;
  let fixture: ComponentFixture<UnlockCardPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnlockCardPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnlockCardPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
