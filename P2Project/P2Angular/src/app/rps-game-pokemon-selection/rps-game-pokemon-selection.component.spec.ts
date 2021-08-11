import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RpsGamePokemonSelectionComponent } from './rps-game-pokemon-selection.component';

describe('RpsGamePokemonSelectionComponent', () => {
  let component: RpsGamePokemonSelectionComponent;
  let fixture: ComponentFixture<RpsGamePokemonSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      declarations: [ RpsGamePokemonSelectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RpsGamePokemonSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
