import { Component, OnInit } from '@angular/core';
import { GameService } from '../game-service';

@Component({
  selector: 'app-wam-game',
  templateUrl: './wam-game.component.html',
  styleUrls: ['./wam-game.component.css']
})

export class WamGameComponent implements OnInit {

  //user score
  score = 0;
  //time remaining in game
  time: any = 30;
  //location of diglett
  random?:number;
  //is the game over true/false
  gameOver: boolean = false;
  //has the game started true/false
  gameStart: boolean = false;
  //modifies difficulty (how fast digletts appear/disappear)
  difficulty:number = 1;

  numCoinsToAdd?: number;
  currentUserCoinBalance = {} as any;

  constructor(private _gameService: GameService) { }

  ngOnInit(): void {
  }

  //intializes game conditions and starts new game
  startGame(){
    this.score = 0;
    this.time = 30;
    this.gameOver = false;
    this.gameStart = true;
    this.countdown();
    this.moveDigglet();
  }

  //countdown timer till game ends
  countdown(){
    let timer = setInterval(() => {
      if(this.time <= 1){
        this.time = "Game Over!";
        this.gameOver = true;
        this.gameStart = false;
        this.determineResult();
        clearInterval(timer);
      } 
      else{
        this.time -= 1;
      }
    },1000);
  }

  //removes diglett if user click hits
  remove_img(x:string){
    document.getElementById(x)!.remove();
  }
 
  //tests if user click loaction is the same as a diglett
  testhit(x:number){
    if(x == this.random){
      this.score++;
    }
  }

  //generate a random number to assign the location where a diglett will appear for a time 
  moveDigglet(){
    let peek = setInterval(() =>{ 
      if(this.gameOver == true)
      {
        this.random = 0;
        clearInterval(peek);
      }
      else{
        var x = Math.floor(Math.random() * 10) + 1;
        //makes sure diglett does not appear in same square
        if(x == this.random)
        {
          if(x == 10)
          {
            this.random = Math.floor(Math.random() * 9) + 1;
          }
          else
          {
            this.random = x + 1;
          }
        }
        else
        {
          this.random = x;
        }
      }
    }, 1500/this.difficulty)
  }

  // gives coins to user based on score and difficulty
  determineResult(){
    if (this.gameOver == true)
    {
      this.numCoinsToAdd = this.difficulty * this.score;
      this._gameService.AddCoins(this.numCoinsToAdd).subscribe();
      this._gameService.GetBalance().subscribe(
        result => {
          let coinBalance   = result;
          this.currentUserCoinBalance = coinBalance;
        });
    }
  }
}
