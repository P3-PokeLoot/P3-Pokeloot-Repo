import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyGameFormComponent } from './modify-game-form.component';

describe('ModifyGameFormComponent', () => {
  let component: ModifyGameFormComponent;
  let fixture: ComponentFixture<ModifyGameFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModifyGameFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModifyGameFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
