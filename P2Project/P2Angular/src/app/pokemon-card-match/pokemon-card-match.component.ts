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
    this.flippedc1  = false;
    this.flippedc2  = false;
    this.flippedc3  = false;
    this.flippedc4  = false;
  }

  updateCounter(value: string) : void {
    if(this.firstClick == ""){
      this.firstClick = value;
      this.counter++;
      // alert(this.firstClick);
    }
    else if(this.secondClick == "") {
      this.secondClick = value;
      this.counter++;
      // alert(this.secondClick);
      if(this.firstClick == this.secondClick) {
        alert("You Win");
        this.firstClick = "";
        this.secondClick = "";
        this.counter = 0 ;
        this.flippedc1  = false;
        this.flippedc2  = false;
        this.flippedc3  = false;
        this.flippedc4  = false;

      }
      else{
        alert("You Loose");
        this.firstClick = "";
        this.secondClick = "";
        this.counter = 0 ;
        this.flippedc1  = false;
        this.flippedc2  = false;
        this.flippedc3  = false;
        this.flippedc4  = false;
      }
    }

    // if(this.counter >= 2){
    //   this.firstClick = "";
    //   this.secondClick = "";
    //   this.counter = 0 ;
    // }



  }



}

