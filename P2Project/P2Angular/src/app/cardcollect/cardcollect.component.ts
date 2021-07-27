import { IcuPlaceholder } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import { CardServiceService } from '../card-service.service';
import { ICard } from './ICard';
import { IRarities } from './IRarities';
import { Observable, of, BehaviorSubject } from 'rxjs';
//import { loadavg } from 'os';


@Component({
  selector: 'app-cardcollect',
  templateUrl: './cardcollect.component.html',
  styleUrls: ['./cardcollect.component.css']
})

export class CardCollectComponent implements OnInit {

  userCollection: ICard[];
  fullUserCollection: ICard[];
  raritiesList: IRarities[];
  //raritiesList: number[];
  filterValue: number;
  filterValueShiny: boolean;
  private userId = localStorage.getItem('userId');
  pageOfItems!: ICard[];
  //@Input() changePage = EventEmitter<any>(true);
  currentIndex : number = 0;
  currentPage: number = 1;
  lastpage!: number;
  bublapedia: string = 'https://bulbapedia.bulbagarden.net/wiki/';



  constructor(private _cardcollectionService: CardServiceService) {
    this.userCollection = [];
    this.fullUserCollection = [];
    this.filterValue = 0;
    this.filterValueShiny = false;
    this.raritiesList = [];
  }


  ngOnInit(): void {

    if (this.userId != null) {
      this._cardcollectionService.GetCardsList(this.userId).subscribe(
        result => {
          for (let i = 0; i < result.length; i++) {

            let PokemonId = result[i].Key.PokemonId;
            let Amount = result[i].Key.QuantityNormal;
            let AmountShiny = result[i].Key.QuantityShiny;
            let RarityId = result[i].Value.RarityId;
            let Link = result[i].Value.SpriteLink;
            let LinkShiny = result[i].Value.SpriteLinkShiny;
            let PokemonName = result[i].Value.PokemonName;

            if (Amount > 0) {
              let Quantity = Amount;
              let SpriteLink = Link;
              let IsShiny = false;
              let card: ICard = { PokemonId, Quantity, RarityId, SpriteLink, PokemonName, IsShiny };
              this.fullUserCollection.push(card);
            }
            if (AmountShiny > 0) {
              let Quantity = AmountShiny;
              let SpriteLink = LinkShiny;
              let IsShiny = true;
              let card: ICard = { PokemonId, Quantity, RarityId, SpriteLink, PokemonName, IsShiny };
              this.fullUserCollection.push(card);
            }
          }
          this.filterCollection();
          
        }
      );
      this._cardcollectionService.GetRarityList().subscribe(
        result => {

          result.forEach(element => {
            let RarityId = element.rarityId;
            let RarityName = element.rarityCategory;

            let newRarity: IRarities = { RarityId, RarityName };
            this.raritiesList.push(newRarity);
          });
        }
      );
      
      
    }
  }

  filterCollection(): void {
    this.userCollection = [];

    if (this.filterValue == 0) {
      if (this.filterValueShiny == false) {
        this.userCollection = this.fullUserCollection;
      }
      else {
        this.fullUserCollection.forEach(element => {
          if (element.IsShiny == this.filterValueShiny) {
            this.userCollection.push(element);
          }
        });
      }

    }
    else {
      this.fullUserCollection.forEach(element => {
        if (this.filterValueShiny == false) {
          if (element.RarityId == this.filterValue) {
            this.userCollection.push(element);
          }
        }
        else {
          if (element.RarityId == this.filterValue && element.IsShiny == this.filterValueShiny) {
            this.userCollection.push(element);
          }
        }
      });
    }
    if(this.userCollection != null){
      this.load();
    }
  
        
        



  }

  load(){
    console.log("collenction length = " + this.userCollection.length);
    this.currentIndex = 0;
    this.currentPage = 1;
    this.lastpage = 1 + Math.floor(this.userCollection.length / 25);
    this.pageOfItems = this.userCollection.slice(this.currentIndex, this.currentIndex + 25);
  }

  onChangePageNext() {
    // update current page of items
    this.currentIndex += 25;
    if(this.currentIndex >= this.userCollection.length - 25){
      this.currentIndex = this.userCollection.length - 25;
      this.currentPage = this.lastpage - 1;
    }
    this.currentPage++;
    console.log(this.currentIndex);
    console.log("collenction length = " + this.userCollection.length);
    this.pageOfItems = this.userCollection.slice(this.currentIndex, this.currentIndex + 25);
    //this.pageOfItems = pageOfItems;
}
  onChangePagePrev() {
    // update current page of items
    this.currentIndex -= 25;
    this.currentPage--;
    if(this.currentIndex <= 0){
      this.currentIndex = 0;
      this.currentPage = 1;
    }
    console.log(this.currentIndex);
    this.pageOfItems = this.userCollection.slice(this.currentIndex, this.currentIndex + 25);
    //this.pageOfItems = pageOfItems;
  }




}
