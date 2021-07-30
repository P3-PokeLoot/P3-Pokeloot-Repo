import { Component, OnInit } from '@angular/core';

enum RpsGameState{
  SelectPokemon,
  PresentOutcome
}

@Component({
  selector: 'app-rps-game',
  templateUrl: './rps-game.component.html',
  styleUrls: ['./rps-game.component.css']
})
export class RpsGameComponent implements OnInit {
  State = RpsGameState;
  gameState: RpsGameState = RpsGameState.SelectPokemon;
  userSelection?: number;

  constructor() { }

  ngOnInit(): void {}

  setUserInput(userSelection: number)
  {
    this.userSelection = userSelection;
    this.gameState = this.State.PresentOutcome;
  }

  restartGame(){
    this.gameState = this.State.SelectPokemon;
  }
}

