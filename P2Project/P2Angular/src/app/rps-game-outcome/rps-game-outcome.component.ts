import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { GameService } from '../service/game/game-service';


@Component({
  selector: 'app-rps-game-outcome',
  templateUrl: './rps-game-outcome.component.html',
  styleUrls: ['./rps-game-outcome.component.css']
})
export class RpsGameOutcomeComponent implements OnInit {

  // number of coins that user wins
  public numCoinsToAdd: number = 20;
  //take user choice input from rps-game-pokemone-selection
  // @Input() userChoice: number = 0;
  @Input() userChoice?: number;
  //Computer rps choice value
  public computerChoice: number = 0;
  //game outcome string
  public outcome!: string;
  //specific message string
  public message?: string;
  //Display how much coins user won!
  public totalEarned: string = `You won ${this.numCoinsToAdd} Congratulations!`
  //Boolean if user won
  public userWon: boolean = false;
  @Output() playAgainEmitter = new EventEmitter();

  constructor(private _gameService: GameService) { }

  ngOnInit(): void {
    this.determineComputerChoice();
    if (this.userChoice)
      this.evaluateGameOutcome(this.computerChoice, this.userChoice);
  }

  //Generates a random number for computer selection
  determineComputerChoice(): void {
    this.computerChoice = Math.floor(Math.random() * 3) + 1;
  }

  //Compares the user's selection with computer's selection and determines the proper result
  // 1 = grass
  // 2 = water
  // 3 = fire
  evaluateGameOutcome(computerChoice: number, userChoice: number): void {
    if (userChoice == 1 && computerChoice == 1) {
      this.outcome = "Looks like a tie! "
      this.message = "Tie! \nGrass is not very effective against grass!"
      this._gameService.RpsLose().subscribe();
    }
    else if (userChoice == 2 && computerChoice == 2) {
      this.outcome = "Looks like a tie! "
      this.message = "Tie! \nWater is not very effective against water!"
      this._gameService.RpsLose().subscribe();
    }
    else if (userChoice == 3 && computerChoice == 3) {
      this.outcome = "Looks like a tie! "
      this.message = "Tie! \nFire is not very effective against fire!"
      this._gameService.RpsLose().subscribe();
    }
    else if (userChoice == 1 && computerChoice == 2) {
      this.outcome = "Yay you won! "
      this.message = "You win! \nGrass is super effective against water!"
      this.userWon = true;
      this._gameService.AddCoins(this.numCoinsToAdd).subscribe();
      this._gameService.RpsWin().subscribe();
    }
    else if (userChoice == 2 && computerChoice == 3) {
      this.outcome = "Yay you won! "
      this.message = "You win! \nWater is super effective against fire!"
      this.userWon = true;
      this._gameService.AddCoins(this.numCoinsToAdd).subscribe();
      this._gameService.RpsWin().subscribe();
    }
    else if (userChoice == 3 && computerChoice == 1) {
      this.outcome = "Yay you won! "
      this.message = "You win! \nFire is super effective against grass!"
      this.userWon = true;
      this._gameService.AddCoins(this.numCoinsToAdd).subscribe();
      this._gameService.RpsWin().subscribe();
    }
    else if (userChoice == 1 && computerChoice == 3) {
      this.outcome = "Aww you lost! "
      this.message = "Computer wins! \nFire is super effective against grass!"
      this._gameService.RpsLose().subscribe();
    }
    else if (userChoice == 2 && computerChoice == 1) {
      this.outcome = "Aww you lost! "
      this.message = "Computer wins! \nGrass is super effective against water!"
      this._gameService.RpsLose().subscribe();
    }
    else if (userChoice == 3 && computerChoice == 2) {
      this.outcome = "Aww you lost! "
      this.message = "Computer wins! \nWater is super effective against fire!"
      this._gameService.RpsLose().subscribe();
    }
    let timer = setInterval(() => {
      this._gameService.RpsRecord().subscribe(
        result => {
          this.outcome += "Win record: " + result;
        });
      clearInterval(timer);
    }, 500);
  }

  playAgain() {
    this.playAgainEmitter.emit();
  }
}
