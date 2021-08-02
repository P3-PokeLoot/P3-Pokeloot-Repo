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
  outcome?:{result:string, picture?:string, win?:boolean};
  State = WtpGameState;
  gameState: WtpGameState = WtpGameState.SelectPokemon;

  constructor() { }

  ngOnInit(): void {
  }

  setUserInput(outcome: {result:string, picture?:string, win?:boolean}) {
    this.outcome = outcome;
    this.gameState = this.State.PresentOutcome;
  }

  restartGame() {
    this.gameState = this.State.SelectPokemon;
  }
}
