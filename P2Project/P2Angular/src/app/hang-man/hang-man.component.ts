import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-hang-man',
  templateUrl: './hang-man.component.html',
  styleUrls: ['./hang-man.component.css']
})
export class HangManComponent implements OnInit {
// note the switch toUpperCase(), we want to always work in upper case letters
  // to avoid confusing 'a' and 'A' as unequal.
  wordArray : string[] = ["Grookey", "Rallaboom","Scorbunny", "Raboot",
                          "Cinderace", "Sobble","Drizzile", "Inteleon",
                          "Greedent", "Rookidee","Dottler", "Nickit"
                        ];
  word : string = "";
  revealedLetters : string[] = [];
  revealWord : string = "";
  maxStrikes : number = 6;
  strikes: number = 0; // the number of incorrect guesses made so far
  userGuess : string = "";
  strikeLetters : string[] = []; //new Array(maxStrikes);
  strikeWord : string = "" ;

  constructor() { }

  ngOnInit(): void {
    // run this now, to draw the empty word at the start of the game.
    // this.revealedLetters.fill("_");
    this.word = this.wordArray[Math.floor(Math.random()*this.wordArray.length)].toUpperCase();
    // alert(this.word)
    for(let i=0; i<this.word.length; i++){
      this.revealedLetters.push("_");
    }
    this.revealWord = this.revealedLetters.join(" ");
    this.strikeWord = this.strikeLetters.join(" ");
  }

    drawWordProgress() : void {
      this.userGuess = this.userGuess.trim();
      this.userGuess = this.userGuess[0].toString();
      if (!this.word.includes(this.userGuess.toUpperCase())){
        this.strikeLetters.push(this.userGuess);
        this.strikeWord = this.strikeLetters.join(" ");
        this.strikes += 1;
      }
      else{
        for(let i=0; i<this.word.length; i++){
            if (this.word[i]==this.userGuess.toUpperCase()){
                this.revealedLetters[i] = this.userGuess;
                this.revealWord = this.revealedLetters.join(" ");
            }
        }
      }
      this.userGuess = ""; // clean up the form place holder
      this.checkGameWinner(); // check if there is a winer
    }

    checkGameWinner() : void {
            if(this.strikes >= this.maxStrikes) {
              alert("The game is over!");
              alert("YOU LOOSE.");
              this.resetFunction();
            }
            else if(!this.revealWord.includes("_"))
            {
              alert("YOU WIN.");
              this.resetFunction();
            }
    }

    resetFunction() : void {
      this.strikes = 0; // restart the counter
      this.strikeLetters = []; // restart with empty array
      this.revealedLetters=[];
      this.word = this.wordArray[Math.floor(Math.random()*this.wordArray.length)].toUpperCase();
      for(let i=0; i<this.word.length; i++){
          this.revealedLetters.push("_");
      }
      this.revealWord = ""; // reset to empty
      this.revealWord = this.revealedLetters.join(" ");
      this.strikeWord = ""; // reset to empty
    }

}
