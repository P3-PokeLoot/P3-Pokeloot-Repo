import { Component, Input, OnInit } from '@angular/core';
import { CardServiceService } from '../card-service.service';
import { ICard } from '../cardcollect/ICard';

@Component({
  selector: 'app-pokemon-card-match',
  templateUrl: './pokemon-card-match.component.html',
  styleUrls: ['./pokemon-card-match.component.css']
})
export class PokemonCardMatchComponent implements OnInit {

  // user1Id
  @Input() user1Id: string = '';
  // user1 cards collection
  user1Collection: ICard[] = [];
  // user2Id
  @Input() user2Id: string = '';
  // user2 cards collection
  user2Collection: ICard[] = [];

  // pokemon-card-match collection
  sameCollection: ICard[] = [];

  constructor(private _cardcollectionService: CardServiceService) {
    this.sameCollection = [];

  }


  ngOnInit(): void {

  }

  // get user1 card colection
  getUser1CardCollection(user1Id : string) : void {
    if (this.user1Id != null) {
      this._cardcollectionService.GetCardsList(this.user1Id).subscribe(
        x => this.user1Collection = x,
        y => "there is no pokemon matched card"
      )
    }
  }

  // get user1 card colection
  getUser2CardCollection(user2Id : string) : void {
    if (this.user2Id != null) {
      this._cardcollectionService.GetCardsList(this.user2Id).subscribe(
        x => this.user2Collection = x,
        y => "there is no pokemon matched card"
      )
    }
  }
  // render pokemon card that user1 and user2 have in common.
  sameCardCollection() : void {
    if (this.user1Collection.length > 0 && this.user2Collection.length > 0 ) {
      // filter pokemon card that exist in user1Collection and user2Collection
        this.sameCollection = this.user1Collection.filter(x => this.user2Collection.includes(x));
    }
  }

}
