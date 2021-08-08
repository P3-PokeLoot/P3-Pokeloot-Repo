import { Component, OnInit } from '@angular/core';
import { GamesService } from '../games.service';

@Component({
  selector: 'app-game-page',
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.css']
})
export class GamePageComponent implements OnInit {

  private userId = localStorage.getItem('userId');
  public currentUserCoinBalance = {} as any;
  public gameList!: Array<any>;

  constructor(private _gameService: GamesService) {
    this.currentUserCoinBalance = {} as any;
  }

  ngOnInit(): void {
    if (this.userId != null) {
      this._gameService.GetBalance().subscribe(
        result => {
          let coinBalance = result;

          this.currentUserCoinBalance = coinBalance;
        });
    }

    this._gameService.GetList().subscribe(result => { this.gameList = result; })
    console.log("arrived at game");
  }

  playGame(): void {
    const numCoinsToAdd: number = 15;
    this._gameService.AddCoins(numCoinsToAdd).subscribe();

    this._gameService.GetBalance().subscribe(
      result => {
        let coinBalance = result;

        this.currentUserCoinBalance = coinBalance;
      });
  }
}
