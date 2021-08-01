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
  userInput?:string;
  State = WtpGameState;
  gameState: WtpGameState = WtpGameState.SelectPokemon;

  constructor() { }

  ngOnInit(): void {
  }

  setUserInput(userInput: string) {
    this.userInput = userInput;
    this.gameState = this.State.PresentOutcome;
  }

  restartGame() {
    this.gameState = this.State.SelectPokemon;
  }
}
