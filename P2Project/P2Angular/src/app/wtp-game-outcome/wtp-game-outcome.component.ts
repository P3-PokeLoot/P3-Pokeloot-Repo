import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { delay } from 'rxjs/operators';
import { GameService } from '../service/game/game-service';

@Component({
  selector: 'app-wtp-game-outcome',
  templateUrl: './wtp-game-outcome.component.html',
  styleUrls: ['./wtp-game-outcome.component.css']
})
export class WtpGameOutcomeComponent implements OnInit {
  // gets variables from selection component
  @Input() result?: { result: string, picture?: string, win?: boolean };
  // event emitter to play again
  @Output() playAgainEmitter = new EventEmitter();
  // string to display on page
  outcomeText?: string;
  // picture URL of pokemon sprite
  pictureUrl?: string;
  // boolean for user won
  win?: boolean;
  // string to hold win record from API
  winRecord?: string;
  // amount of coins to award for winning
  numCoinsToAdd: number = 100;
  // variable to hold user's coin balance
  currentUserCoinBalance?:any;

  constructor(private _gameService: GameService) { }

  ngOnInit(): void {
    // wait to determine result, give database chance to get data from selection component
    let timer = setInterval(() => {
      this.determineResult();
      clearInterval(timer);
    }, 500);
  }

  determineResult(): void {
    // set variables
    this.outcomeText = this.result?.result;
    this.pictureUrl = this.result?.picture;
    this.win = this.result?.win;
    // if user wins, give coins and get new balance
    if (this.win == true) {
      this._gameService.AddCoins(this.numCoinsToAdd).subscribe();
      // wait to get coin balance
      let timer = setInterval(() => {
        this._gameService.GetBalance().subscribe(
          result => {
            let coinBalance = result;
            this.currentUserCoinBalance = coinBalance;
          });
        clearInterval(timer);
      }, 300);
    }
    // get user win record
    this._gameService.WtpRecord().subscribe(
      result => {
        this.winRecord = result;
        console.log(this.winRecord);
      });
  }

  playAgain() {
    this.playAgainEmitter.emit();
  }
}