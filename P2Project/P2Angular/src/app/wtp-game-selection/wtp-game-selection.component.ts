import { Component, OnInit, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-wtp-game-selection',
  templateUrl: './wtp-game-selection.component.html',
  styleUrls: ['./wtp-game-selection.component.css']
})
export class WtpGameSelectionComponent implements OnInit {
  @Output() userChoiceEmitter = new EventEmitter<string>();
  constructor() { }

  ngOnInit(): void {
  }

  PokemonSelected(i : string): void{
    this.userChoiceEmitter.emit(i);
  }
}
