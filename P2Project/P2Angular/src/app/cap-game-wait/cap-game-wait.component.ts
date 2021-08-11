import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-cap-game-wait',
  animations: [
    trigger('rockingPokeball', [
      state('left', style({
        transform: 'rotate(20deg)'
      })),
      state('right', style({
        transform: 'rotate(-20deg)'
      })),
      transition('* <=> *', [
        animate('0.5s')
      ])
    ])
  ],
  templateUrl: './cap-game-wait.component.html',
  styleUrls: ['./cap-game-wait.component.css']
})
export class CapGameWaitComponent implements OnInit {

  state: string = 'left';
  shakeTimes: number = 0;
  @Output() showResults = new EventEmitter();

  constructor() { }
  
  onDone(){
    if(this.shakeTimes == 2) this.showResults.emit();
    if(this.state === 'right') this.shakeTimes++;
    this.state = this.state === 'left' ? 'right' : 'left';
  }

  ngOnInit(): void {
  }

}
