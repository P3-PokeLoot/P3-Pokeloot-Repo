import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GameService } from '../service/game/game-service';

@Component({
  selector: 'app-cap-game-result',
  templateUrl: './cap-game-result.component.html',
  styleUrls: ['./cap-game-result.component.css']
})
export class CapGameResultComponent implements OnInit {

  @Input() catchChanceResult?: number;
  @Input() pokemonImgSrc?: string;
  @Input() pokemonName?: string;
  @Output() playAgainEmitter = new EventEmitter();
  winRecord?:string;
  result?: string;
  numCoinsToAdd: number = 100;
  waitTime: any = 1;
  @Input() pokeballImg = new Image();
  

  constructor(private _gameService: GameService) { }

  playAgain(){
    this.playAgainEmitter.emit();
  }

  

  ngOnInit(): void {
    if(this.catchChanceResult != undefined){
      let rand = Math.random();
      if((this.catchChanceResult === 0 && rand < 0.75) ||
         (this.catchChanceResult === 1 && rand < 0.50) ||
         (this.catchChanceResult === 2 && rand < 0.25)){
        this.result = "Success";
        this._gameService.CapWin().subscribe();
        this._gameService.AddCoins(this.numCoinsToAdd).subscribe();
      }
      else
      {
        this._gameService.CapLose().subscribe();
        this.result = "Failure";
      }
    }
    let timer = setInterval(() => {
      if(this.waitTime <= 1){
        this._gameService.CapRecord().subscribe(
          result =>{
            this.winRecord = result;
          });
        clearInterval(timer);
      } 
      else{
        this.waitTime -= 1;
      }
    },500);
  }
}
