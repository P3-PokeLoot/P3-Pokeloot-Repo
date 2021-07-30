import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-rps-game-pokemon-selection',
  templateUrl: './rps-game-pokemon-selection.component.html',
  styleUrls: ['./rps-game-pokemon-selection.component.css']
})
export class RpsGamePokemonSelectionComponent implements OnInit {

  // this refers to userChoice within rps-game-outcome' userChoice
  @Output() userChoiceEmitter = new EventEmitter<number>();


  constructor() { }

  ngOnInit(): void {
  }

  PokemonSelected(i : number): void{
    this.userChoiceEmitter.emit(i);
  }
}
