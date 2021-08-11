import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { GameService } from '../service/game/game-service';


const intervalTime = 2;

@Component({
  selector: 'app-cap-game-catch',
  animations: [
    trigger('catchChance', [
      state('low', style({
        height: '300px',
        width: '300px',
        borderRadius: '50%',
        border: '3px solid',
        borderColor: 'green'
      })),
      state('medium', style({
        height: '150px',
        width: '150px',
        borderRadius: '50%',
        border: '3px solid',
        borderColor: 'yellow'
      })),
      state('high', style({
        height: '1px',
        width: '1px',
        borderRadius: '50%',
        border: '3px solid',
        borderColor: 'red'
      })),
      transition('low => medium', [
        animate('1s')
      ]),
      transition('medium => high', [
        animate('1s')
      ])
    ])
  ],
  templateUrl: './cap-game-catch.component.html',
  styleUrls: ['./cap-game-catch.component.css']
})
export class CapGameCatchComponent implements OnInit {

  @Output() catchChanceEmitter = new EventEmitter<CAPResultInfo>();
  pokemonName?: string;
  isOpen = true;
  timeLeft: number = intervalTime;
  interval?: any;
  pictureUrl?: string;
  isImageLoaded: boolean = false;

  constructor(private _gamesService: GameService) { }

  toggle() {
    this.isOpen = !this.isOpen;
  }

  startTimer(){
    this.interval = setInterval(() => {
      if(this.timeLeft > 0){
        this.timeLeft--;
      }
      else{
        this.timeLeft = intervalTime;
      }
    }, 1000);
  }

  determineCatchChance(): string{
    if(this.timeLeft === intervalTime)
      return "low";
    else if(this.timeLeft > (intervalTime/3))
      return "medium";
    else
      return "high";
  }

  emitCatchChance(){
    this.catchChanceEmitter.emit({
      catchChance: this.timeLeft,
      pokemonImgSrc: this.pictureUrl,
      pokemonName: this.pokemonName
    });
  }

  ngOnInit(): void {
    this._gamesService.GetWtpGame().subscribe(
      result => {
        this.pictureUrl = result.PictureUrl;
        this.pokemonName = result.CorrectPokemon;
      });
    this.startTimer();
  }
}

export interface CAPResultInfo{
  pokemonName?: string,
  pokemonImgSrc?: string,
  catchChance?: number
};
