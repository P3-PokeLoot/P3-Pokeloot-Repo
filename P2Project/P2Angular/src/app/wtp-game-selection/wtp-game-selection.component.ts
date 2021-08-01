import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { GamesService, WtpGame } from '../games.service';

@Component({
  selector: 'app-wtp-game-selection',
  templateUrl: './wtp-game-selection.component.html',
  styleUrls: ['./wtp-game-selection.component.css']
})
export class WtpGameSelectionComponent implements OnInit {
  @Output() userChoiceEmitter = new EventEmitter<boolean>();
  pictureUrl?:string;
  correctPokemon?:string;
  options?:string[];

  constructor(private _gameService:GamesService) { }

  ngOnInit(): void {
    this._gameService.GetWtpGame().subscribe(
      result => {
        this.pictureUrl = result.pictureUrl;
        this.correctPokemon = result.correctPokemon;
        this.options = result.options;
        console.log(result);
      });
  }

  PokemonSelected(userSelection : string): void{
    console.log(userSelection);
    console.log(this.correctPokemon);
    if(userSelection.toString() === this.correctPokemon?.toString())
    {
      this.userChoiceEmitter.emit(true);
    }
    else
    {
      this.userChoiceEmitter.emit(false);
    }
  }
}
