import { Component, OnInit } from '@angular/core';
import { GameService } from '../service/game/game-service';

@Component({
  selector: 'app-game-page',
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.css']
})
export class GamePageComponent implements OnInit {

  private userId?: any = localStorage.getItem('userId');
  public admin!: boolean;
  public currentUserCoinBalance = {} as any;
  public gameList!: Array<any>;

  constructor(private _gameService: GameService) {
    this.currentUserCoinBalance = {} as any;
  }

  ngOnInit(): void {
    // Check is user is admin
    console.log(localStorage.getItem('userId'))
    this.admin = (parseInt(this.userId) == 12)
    console.log(this.admin)

    if (this.userId != null) {
      this._gameService.GetBalance().subscribe(
        result => {
          let coinBalance = result;

          this.currentUserCoinBalance = coinBalance;
        });
    }

    this._gameService.GetList().subscribe(
      result => {
        this.gameList = result;
        console.log(result)
      },
      error => { console.log(error) }
    )
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
