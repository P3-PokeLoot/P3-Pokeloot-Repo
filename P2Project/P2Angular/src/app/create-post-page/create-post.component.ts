import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CardServiceService } from '../card-service.service';
import { ICard } from '../cardcollect/ICard';
import { IPost } from '../Models/IPost';


@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {

  isPriceValid: boolean = false;
  isPostTypeSelect = false;
  submittedSuccesfully = false;
  private userId = localStorage.getItem('userId') as string;
  user = localStorage.getItem('user') as string
  userName!: string;
  bublapedia: string = 'https://bulbapedia.bulbagarden.net/wiki/';
  userCollection: ICard[];
  postType = [{ type: "Display" }, { type: "Sale" }, { type: "Discussion" }];


  constructor(private _cardcollectionService: CardServiceService) {
    this.userCollection = []
  }

  ngOnInit(): void {
    let parseObj = JSON.parse(this.user);
    this.userName = parseObj['userName'];
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
              this.userCollection.push(card);
            }
            if (AmountShiny > 0) {
              let Quantity = AmountShiny;
              let SpriteLink = LinkShiny;
              let IsShiny = true;
              let card: ICard = { PokemonId, Quantity, RarityId, SpriteLink, PokemonName, IsShiny };
              this.userCollection.push(card);
            }

          }
        }

      );
    }

  }


  OnSubmit(postForm: NgForm) {
    this.isPriceValid = false;
    this.isPostTypeSelect = false;

    if (postForm.value.postType === 'Sale') {
      this.isPriceValid
      if (!postForm.controls.Price.valid) {
        this.isPriceValid = true;
        return;
      }
    }

    if (postForm.value.postType === "") {
      this.isPostTypeSelect = true;
      return;
    }

    var card: any;

    for (var cards of this.userCollection) {
      // console.log(`${cards.PokemonId} === ${postForm.value.PokemonId}`)
      if (cards.PokemonId == postForm.value.PokemonId) {
        card = cards;
        break;
      } else {
        card = null
      }
    }

    let post = {
      PokemonId: card.PokemonId,
      PostDescription: postForm.value.textDescription,
      Price: postForm.value.postType === 'Sale' ? postForm.value.Price : 0,
      StillAvailable: postForm.value.postType === 'Sale' ? true : false,
      IsShiny: card.IsShiny,
      UserId: parseInt(this.userId),
      UserName: this.userName,
      SpriteLink: card.SpriteLink,
      PostType: postForm.value.postType,
      PokemonName: card.PokemonName,
      RarityId: card.RarityId,
    }
  }

  GetRarityDisplay(rarityId: any): string {
    switch (rarityId) {
      case 1:
        return 'Common'
        break;
      case 2:
        return 'Uncommon'
        break;
      case 3:
        return 'Rare'
        break;
      case 4:
        return 'Super Rare'
        break;
      case 5:
        return 'Specially Rare'
        break;
      default:
        return 'No Card Attached'
        break;
    }
  }
}
