
import { Component, Input, OnInit } from '@angular/core';
import { PokemonCards } from '../Models/pokemon-cards';
import { IUnlockCard } from '../unlock-card-page/IUnlockCard';


@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent implements OnInit {

  @Input() newCard?: IUnlockCard;

  
  constructor() { }
  
  //method to update array in usercollection
  
  ngOnInit(): void {
    
    
    
  }

  
}
