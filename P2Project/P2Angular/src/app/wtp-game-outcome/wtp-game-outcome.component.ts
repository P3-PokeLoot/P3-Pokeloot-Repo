import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-wtp-game-outcome',
  templateUrl: './wtp-game-outcome.component.html',
  styleUrls: ['./wtp-game-outcome.component.css']
})
export class WtpGameOutcomeComponent implements OnInit {
  @Input() userWin?: boolean;
  @Output() playAgainEmitter = new EventEmitter();
  outcomeText?:string;

  constructor() {}

  ngOnInit(): void {
    console.log(this.userWin);
    if(this.userWin == true)
    {
      this.outcomeText = "You win!";
    }
    else
    {
      this.outcomeText = "You lose!"
    }
  }

}
