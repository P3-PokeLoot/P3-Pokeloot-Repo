import { Component, Input, OnInit } from '@angular/core';


@Component({
  selector: 'app-rps-game-outcome',
  templateUrl: './rps-game-outcome.component.html',
  styleUrls: ['./rps-game-outcome.component.css']
})
export class RpsGameOutcomeComponent implements OnInit {

  //take user choice input from rps-game-pokemone-selection
  // @Input() userChoice: number = 0;
  @Input() userChoice: number = 0;
  //Computer rps choice value
  public computerChoice: number = 0;
  //game outcome string
  public outcome?: string;
  //specific message string
  public message?: string;

  constructor() { }

  ngOnInit(): void {
    this.determineComputerChoice();
    this.evaluateGameOutcome(this.computerChoice, this.userChoice);
  }

  //Generates a random number for computer selection
  determineComputerChoice(): void{
    this.computerChoice = Math.floor(Math.random()*3) + 1;
  }

  //Compares the user's selection with computer's selection and determines the proper result
  // 1 = grass
  // 2 = water
  // 3 = fire
  evaluateGameOutcome(computerChoice: number, userChoice: number): void{
    if(userChoice == 1 && computerChoice == 1){
      this.outcome = "Looks like a tie"
      this.message = "Tie! Grass is not very effective against grass!"
    }
    else if(userChoice == 2 && computerChoice == 2){
      this.outcome = "Looks like a tie"
      this.message = "Tie! Water is not very effective against water!"
    }
    else if (userChoice == 3 && computerChoice == 3){
      this.outcome = "Looks like a tie"
      this.message = "Tie! Fire is not very effective against fire!"
    }
    else if(userChoice == 1 && computerChoice == 2){
      this.outcome = "Yay you won"
      this.message = "You win! Grass is super effective against water!"
    }
    else if(userChoice == 2 && computerChoice == 3){
      this.outcome = "Yay you won"
      this.message = "You win! Water is super effective against fire!"
    }
    else if (userChoice == 3 && computerChoice == 1){
      this.outcome = "Yay you won"
      this.message = "You win! Fire is super effective against grass!"
    }
    else if(userChoice == 1 && computerChoice == 3){
      this.outcome = "Aww you lost"
      this.message = "Computer wins! Fire is super effective against grass!"
    }
    else if(userChoice == 2 && computerChoice == 1){
      this.outcome = "Aww you lost"
      this.message = "Computer wins! Grass is super effective against water!"
    }
    else if (userChoice == 3 && computerChoice == 2){
      this.outcome = "Aww you lost"
      this.message = "Computer wins! Water is super effective against fire!"
    }
  }
}
