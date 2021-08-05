import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-hang-man',
  templateUrl: './hang-man.component.html',
  styleUrls: ['./hang-man.component.css']
})
export class HangManComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    this.drawWordProgress(this.userGuess);
  }

// note the switch toUpperCase(), we want to always work in upper case word
// to avoid any confusion.
word : string = "revature".toUpperCase();
userGuess : string = "";
counter : number = 0;
@Output() playerGuess = new EventEmitter<string>();
maxStrikes : number = 6;// the number of incorrect guesses made so far

strikes : number = 0;

    drawWordProgress(playerGuess : string) {
      if(playerGuess == this.word){
      }
    }

    drawGallows() : void {
      this.counter++;
      if(this.userGuess.toUpperCase() == this.word){
        alert("you win");
        this.counter = 0;
      }
      else if(this.counter >= 6){
        alert("you loose")
        this.counter = 0;
      }

    }

}
