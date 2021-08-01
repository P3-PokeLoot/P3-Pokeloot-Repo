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
  userWin?: boolean;
  State = WtpGameState;
  gameState: WtpGameState = WtpGameState.SelectPokemon;

  constructor() { }

  ngOnInit(): void {
  }

  setUserInput(userWin: boolean) {
    this.userWin = userWin;
    this.gameState = this.State.PresentOutcome;
  }

  restartGame() {
    this.gameState = this.State.SelectPokemon;
  }
}
