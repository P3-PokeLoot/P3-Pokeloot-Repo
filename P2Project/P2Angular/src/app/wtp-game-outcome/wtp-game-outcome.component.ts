import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-wtp-game-outcome',
  templateUrl: './wtp-game-outcome.component.html',
  styleUrls: ['./wtp-game-outcome.component.css']
})
export class WtpGameOutcomeComponent implements OnInit {
  @Input() result?: string;
  @Output() playAgainEmitter = new EventEmitter();
  outcomeText?:string;

  constructor() {}

  ngOnInit(): void {
    console.log("Initialized");
    this.determineResult();
  }

  determineResult():void{
    console.log('result ' + this.result);
    this.outcomeText = this.result;
  }

  playAgain(){
    this.playAgainEmitter.emit();
  }
}