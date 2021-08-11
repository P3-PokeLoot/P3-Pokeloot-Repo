import { Component, OnInit } from '@angular/core';
import { CAPResultInfo } from '../cap-game-catch/cap-game-catch.component';

@Component({
  selector: 'app-cap-game',
  templateUrl: './cap-game.component.html',
  styleUrls: ['./cap-game.component.css']
})
export class CapGameComponent implements OnInit {

  constructor() { }

  GameState = GameState; //So enum can be used in template HTML
  currentState = this.GameState.Catch;
  catchChance?: number;
  pokemonUrl?: string;
  pokemonName?: string;

  ngOnInit(): void {
  }

  setCatchChance(incomingInfo: CAPResultInfo){
    this.pokemonName = incomingInfo.pokemonName;
    this.catchChance = incomingInfo.catchChance;
    this.pokemonUrl = incomingInfo.pokemonImgSrc;
    this.currentState = this.GameState.Wait;
  }

  showResults(){
    this.currentState = this.GameState.Result;
  }

  playAgain(){
    this.currentState = this.GameState.Catch;
  }
}

enum GameState{
  Catch,
  Result,
  Wait
}