import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { GameService } from '../service/game/game-service';

@Component({
  selector: 'app-wtp-game-selection',
  templateUrl: './wtp-game-selection.component.html',
  styleUrls: ['./wtp-game-selection.component.css']
})
export class WtpGameSelectionComponent implements OnInit {
  // output variable outputs result string, picture URL, and boolean for user won
  @Output() resultEmitter = new EventEmitter<{result:string, picture?:string, win?:boolean}>();
  // picture URL to silhouette
  pictureUrl?:string;
  // correct pokemon name
  correctPokemon?:string;
  // array containing names for options for game
  options?:string[];

  constructor(private _gameService: GameService) { }

  ngOnInit(): void {
    // get game object from game service
    this._gameService.GetWtpGame().subscribe(
      result => {
        // set variables from result
        this.pictureUrl = result.PictureUrl;
        this.correctPokemon = result.CorrectPokemon;
        this.options = result.Options;
      });
  }

  PokemonSelected(userSelection : string): void{
    if(userSelection == this.correctPokemon) // user guesses correct pokemon
    {
      // call game service to update play record
      this._gameService.WtpWin().subscribe();
      // pass variables to outcome component
      this.resultEmitter.emit({result:"Correct! It's "+ this.correctPokemon+"!", picture:this.pictureUrl, win:true});
    }
    else{ // user picks wrong pokemon
      // call game service to update play record
      this._gameService.WtpLose().subscribe();
      // pass variables to outcome component
      this.resultEmitter.emit({result:"Sorry, It's " + this.correctPokemon + "! You lose.", picture:this.pictureUrl, win:false});
    }
  }
}