import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { delay } from 'rxjs/operators';
import { GameService } from '../service/game/game-service';

@Component({
  selector: 'app-wtp-game-outcome',
  templateUrl: './wtp-game-outcome.component.html',
  styleUrls: ['./wtp-game-outcome.component.css']
})
export class WtpGameOutcomeComponent implements OnInit {
  @Input() result?: { result: string, picture?: string, win?: boolean };
  @Output() playAgainEmitter = new EventEmitter();
  outcomeText?: string;
  pictureUrl?: string;
  win?: boolean;
  winRecord?: string;
  numCoinsToAdd: number = 50;
  currentUserCoinBalance = {} as any;
  waitTime: any = 1;

  constructor(private _gameService: GameService) { }

  ngOnInit(): void {
    console.log("initialized");
    this.waitASec();
  }

  determineResult(): void {
    this.outcomeText = this.result?.result;
    this.pictureUrl = this.result?.picture;
    this.win = this.result?.win;
    if (this.win == true) {
      this._gameService.AddCoins(this.numCoinsToAdd).subscribe();
      this._gameService.GetBalance().subscribe(
        result => {
          let coinBalance = result;
          this.currentUserCoinBalance = coinBalance;
        });
    }
    this._gameService.WtpRecord().subscribe(
      result => {
        this.winRecord = result;
        console.log(this.winRecord);
      });
  }

  waitASec(){
    let timer = setInterval(() => {
      if(this.waitTime <= 1){
        this.determineResult();
        clearInterval(timer);
      } 
      else{
        this.waitTime -= 1;
      }
    },300);
  }

  playAgain() {
    this.playAgainEmitter.emit();
  }
}