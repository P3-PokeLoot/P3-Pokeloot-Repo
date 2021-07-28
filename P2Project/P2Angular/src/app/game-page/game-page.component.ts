import { Component, OnInit } from '@angular/core';
import { GameService } from '../game-service';

@Component({
  selector: 'app-game-page',
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.css']
})
export class GamePageComponent implements OnInit {

  private userId = localStorage.getItem('userId');
  public currentUserCoinBalance = {} as any;

  constructor(private _gameService: GameService) { 
    this.currentUserCoinBalance = {} as any;
  }

  ngOnInit(): void {
    if(this.userId != null)
    {
      this._gameService.GetBalance().subscribe(
        result => {
          let coinBalance   = result;

          this.currentUserCoinBalance = coinBalance;
          console.log(this.currentUserCoinBalance);
        });
    }
  }

  playGame(): void {

    const numCoinsToAdd : number = 15;
    this._gameService.AddCoins(numCoinsToAdd).subscribe();


    this._gameService.GetBalance().subscribe(
      result => {
        let coinBalance   = result;

        this.currentUserCoinBalance = coinBalance;
        console.log(this.currentUserCoinBalance);
      });


  }


}
