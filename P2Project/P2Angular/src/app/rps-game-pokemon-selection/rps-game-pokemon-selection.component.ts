import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-rps-game-pokemon-selection',
  templateUrl: './rps-game-pokemon-selection.component.html',
  styleUrls: ['./rps-game-pokemon-selection.component.css']
})
export class RpsGamePokemonSelectionComponent implements OnInit {
  pokemon1?: number;
  pokemon2?: number;
  pokemon3?: number;
  constructor() { }

  ngOnInit(): void {
  }

  Pokemon1Selected(): void{
    // alert('Pekemon 1 is selected');
    this.pokemon1 = 1;
    alert(`Pekemon ${this.pokemon1} is selected`);
  }
  Pokemon2Selected(): void{
    // alert('Pekemon 2 is selected');
    this.pokemon2 = 2;
    alert(`Pekemon ${this.pokemon2} is selected`);
  }
  Pokemon3Selected(): void{
    // alert('Pekemon 3 is selected');
    this.pokemon3 = 3;
    alert(`Pekemon ${this.pokemon3} is selected`);
  }

}
