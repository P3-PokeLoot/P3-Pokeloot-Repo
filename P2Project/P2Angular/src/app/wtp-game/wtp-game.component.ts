import { Component, OnInit } from '@angular/core';

enum WtpGameState {
  SelectPokemon,
  PresentOutcome
}

@Component({
  selector: 'app-wtp-game',
  templateUrl: './wtp-game.component.html',
  styleUrls: ['./wtp-game.component.css']
})

export class WtpGameComponent implements OnInit {
  // outcome variable passes result string, picture URL string, and boolean for if user won
  outcome?:{result:string, picture?:string, win?:boolean};
  // State enum
  State = WtpGameState;
  // gameState determines which component gets rendered
  gameState: WtpGameState = WtpGameState.SelectPokemon;

  constructor() { }

  ngOnInit(): void {
  }

  setUserInput(outcome: {result:string, picture?:string, win?:boolean}) {
    // set outcome
    this.outcome = outcome;
    // change state to render outcome component
    this.gameState = this.State.PresentOutcome;
  }

  restartGame() {
    // reset game state to selection component
    this.gameState = this.State.SelectPokemon;
  }
}
