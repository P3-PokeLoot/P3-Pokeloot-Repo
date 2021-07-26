import { IcuPlaceholder } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import { UnlockCardService } from '../unlock-card-service';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { IUnlockCard } from './IUnlockCard';


@Component({
  selector: 'app-unlock-card-page',
  templateUrl: './unlock-card-page.component.html',
  styleUrls: ['./unlock-card-page.component.css']
})
export class UnlockCardPageComponent implements OnInit {

  private userId = localStorage.getItem('userId')
  newCard : IUnlockCard;
  currentUserCoinBalance : any;
  tooPoor: boolean = false;
  bublapedia: string = 'https://bulbapedia.bulbagarden.net/wiki/';

  constructor(private _cardcollectionService: UnlockCardService) { 
    this.newCard = {} as IUnlockCard;
    this.currentUserCoinBalance = {} as any;
  }

  ngOnInit(): void {
     if(this.userId != null)
     {
       this._cardcollectionService.Balance().subscribe(
         result => {
           let coinBalance   = result;

           this.currentUserCoinBalance = coinBalance;
         }
         
       );
    
     }
  }

  openLootbox(): void {
    if(this.userId != null)
    {
      if(this.currentUserCoinBalance >= 100)
      {
        this._cardcollectionService.RollLootbox().subscribe(
          result => {
            let PokemonId     = result[0].Key.PokemonId;
            let RarityId      = result[0].Key.RarityId;
            let SpriteLink    = result[0].Key.SpriteLink;
            let PokemonName   = result[0].Key.PokemonName;
            let SpriteLinkShiny = result[0].Key.SpriteLinkShiny;
            let IsShiny       = result[0].Value;
            let MainSprite = '';
            if (IsShiny == true) {
              MainSprite = SpriteLinkShiny;
            } else {
              MainSprite = SpriteLink;
            }

            this.newCard = {PokemonId, RarityId, SpriteLink, PokemonName, IsShiny, SpriteLinkShiny, MainSprite};

            console.log(this.newCard);
          }
        );
        this._cardcollectionService.Balance().subscribe(
          result => {
            let coinBalance   = result;
  
            this.currentUserCoinBalance = coinBalance;
          }
        );
      }
      else
      {
        this.tooPoor = true;
    }
  }

}

}
