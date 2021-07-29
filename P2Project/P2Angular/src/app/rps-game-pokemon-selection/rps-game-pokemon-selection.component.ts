import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-rps-game-pokemon-selection',
  templateUrl: './rps-game-pokemon-selection.component.html',
  styleUrls: ['./rps-game-pokemon-selection.component.css']
})
export class RpsGamePokemonSelectionComponent implements OnInit {

  // this refers to userChoice within rps-game-outcome' userChoice
  @Input() userChoice : number = 0;

  constructor() { }

  ngOnInit(): void {
  }

  PokemonSelected(i : number): void{

    // alert('Pekemon 1 is selected');
    this.userChoice = i;
    alert(`Pekemon ${this.userChoice} is selected`);
  }


}
