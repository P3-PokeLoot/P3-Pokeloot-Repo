import { Component, OnInit } from '@angular/core';
import { CardServiceService } from '../card-service.service';
import { ICard } from '../cardcollect/ICard';

@Component({
  selector: 'app-pokemon-card-match',
  templateUrl: './pokemon-card-match.component.html',
  styleUrls: ['./pokemon-card-match.component.css']
})
export class PokemonCardMatchComponent implements OnInit {
  card1: string = "1";
  card2: string = "2";
  card3: string = "1";
  card4: string = "2";

  // toggle this on click
  flippedc1 : boolean = false;
  flippedc2 : boolean = false;
  flippedc3 : boolean = false;
  flippedc4 : boolean = false;

  // counter
  counter : number = 0;
  firstClick: string = "";
  secondClick: string = "";
  constructor() {}

  ngOnInit(): void {
    // this.flippedc1,  this.flippedc2, this.flippedc3 , this.flippedc4  = false;
    this.flippedc1  = false;
    this.flippedc2  = false;
    this.flippedc3  = false;
    this.flippedc4  = false;
  }

  updateCounter(value: string) : void { // alert(this.firstClick);
    if(this.firstClick == ""){
      this.firstClick = value;
      this.counter++;
    }
    else if(this.secondClick == "") {
      this.secondClick = value;
      this.counter++;
      // alert(this.secondClick);
      if(this.firstClick == this.secondClick) {
        this.firstClick = "";
        this.secondClick = "";
        this.counter = 0 ;
        // alert("You find Matched card");
        if(value =="1") {
          this.flippedc1=true;
          this.flippedc3=true;
        }
        else if(value =="2") {
          this.flippedc2=true;
          this.flippedc4=true;
        }
      }
      else{
        // alert("You Loose");
        this.firstClick ="";
        this.secondClick = "";
        this.counter = 0 ;
        this.flippedc1  = false;
        this.flippedc2  = false;
        this.flippedc3  = false;
        this.flippedc4  = false;
      }
      // check if plaer win the game
      if(this.flippedc1==true&&this.flippedc2==true&&this.flippedc3==true&&this.flippedc4==true){
          alert("YOU WIN");
          this.flippedc1  = false;
          this.flippedc2  = false;
          this.flippedc3  = false;
          this.flippedc4  = false;

      }


    }

  }
}

