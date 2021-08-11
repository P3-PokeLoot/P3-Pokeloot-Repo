import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { GameService } from '../service/game/game-service';

@Component({
  selector: 'app-wtp-game-selection',
  templateUrl: './wtp-game-selection.component.html',
  styleUrls: ['./wtp-game-selection.component.css']
})
export class WtpGameSelectionComponent implements OnInit {
  @Output() resultEmitter = new EventEmitter<{result:string, picture?:string, win?:boolean}>();
  pictureUrl?:string;
  correctPokemon?:string;
  options?:string[];

  constructor(private _gameService: GameService) { }

  ngOnInit(): void {
    this._gameService.GetWtpGame().subscribe(
      result => {
        console.log(result.PictureUrl)
        this.pictureUrl = result.PictureUrl;
        console.log(this.pictureUrl);
        this.correctPokemon = result.CorrectPokemon;
        console.log(this.correctPokemon);
        this.options = result.Options;
      });
  }

  PokemonSelected(userSelection : string): void{
    if(userSelection == this.correctPokemon)
    {
      console.log('win');
      this._gameService.WtpWin().subscribe();
      this.resultEmitter.emit({result:"Correct! It's "+ this.correctPokemon+"!", picture:this.pictureUrl, win:true});
    }
    else{
      console.log('lose');
      this._gameService.WtpLose().subscribe();
      this.resultEmitter.emit({result:"Sorry, It's " + this.correctPokemon + "! You lose.", picture:this.pictureUrl, win:false});
    }
  }
}