import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PokemonCardMatchComponent } from './pokemon-card-match.component';

describe('PokemonCardMatchComponent', () => {
  let component: PokemonCardMatchComponent;
  let fixture: ComponentFixture<PokemonCardMatchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PokemonCardMatchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PokemonCardMatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
