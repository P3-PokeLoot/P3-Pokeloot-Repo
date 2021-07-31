import { Component, OnInit } from '@angular/core';

enum WtpGameState{
  SelectPokemon,
  PresentOutcome
}

@Component({
  selector: 'app-wtp-game',
  templateUrl: './wtp-game.component.html',
  styleUrls: ['./wtp-game.component.css']
})

export class WtpGameComponent implements OnInit {
  State = WtpGameState;
  gameState: WtpGameState = WtpGameState.SelectPokemon;
  userSelection?:string;

  constructor() { }

  ngOnInit(): void {
  }

  setUserInput(userSelection: string)
  {
    this.userSelection = userSelection;
    this.gameState = this.State.PresentOutcome;
  }

  restartGame(){
    this.gameState = this.State.SelectPokemon;
  }
}
