import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-cap-game-result',
  templateUrl: './cap-game-result.component.html',
  styleUrls: ['./cap-game-result.component.css']
})
export class CapGameResultComponent implements OnInit {

  @Input() catchChanceResult?: number;
  @Input() pokemonImgSrc?: string;
  @Input() pokemonName?: string;
  @Output() playAgainEmitter = new EventEmitter();
  result?: string;

  constructor() { }

  playAgain(){
    this.playAgainEmitter.emit();
  }

  ngOnInit(): void {
    if(this.catchChanceResult != undefined){
      let rand = Math.random();
      if(this.catchChanceResult === 0 && rand < 0.75)
        this.result = "Success";
      else if(this.catchChanceResult === 1 && rand < 0.5)
        this.result = "Success";
      else if(this.catchChanceResult === 2 && rand < 0.25)
        this.result = "Success";
      else
        this.result = "Failure";
    }
  }
}
