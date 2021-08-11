import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CardServiceService } from '../card-service.service';
import { ICard } from '../cardcollect/ICard';
import { IPost } from '../Models/IPost';
import { FullPost } from '../Models/Post';
import { CreatePostService } from '../service/createPost/create-post.service';
import { IRarities } from '../cardcollect/IRarities';
import { IGen } from '../cardcollect/IGen';


@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {

  isPriceValid: boolean = false;
  isPostTypeSelect = false;
  isCardChosen = false;
  isDescription = false;
  isPokemonId = false;
  submittedSuccesfully = false;
  private userId = localStorage.getItem('userId') as string;
  user = localStorage.getItem('user') as string
  userName!: string;
  bublapedia: string = 'https://bulbapedia.bulbagarden.net/wiki/';
  userCollection: ICard[];
  postType: string[] = ["Display", "Sale", "Discussion"];
  raritiesList: IRarities[] = [];
  filterValue: number = 0;
  filterValueShiny: boolean = false;
  checkFavorites: boolean = false;
  genList: IGen[];
  genValue: string = "Any";
  genOptions: string[] = ["Any", "Kanto", "Johto", "Hoen", "Sinnoh", "Unova", "Kalos", "Alola", "Galar"];
  fullUserCollection: ICard[] = [];



  constructor(private _cardcollectionService: CardServiceService, private _createPostService: CreatePostService, private route: Router) {
    this.userCollection = [];
    this.genList = [];
  }

  ngOnInit(): void {

    for (let i: number = 0; i <= 809; i++) {//create list if the generation
      if (i <= 151) {
        this.genList.push({ PokemonId: i, GenName: "Kanto" });
      }
      else if (i > 151 && i <= 251) {
        this.genList.push({ PokemonId: i, GenName: "Johto" });
      }
      else if (i > 251 && i <= 386) {
        this.genList.push({ PokemonId: i, GenName: "Hoen" });
      }
      else if (i > 386 && i <= 493) {
        this.genList.push({ PokemonId: i, GenName: "Sinnoh" });
      }
      else if (i > 493 && i <= 649) {
        this.genList.push({ PokemonId: i, GenName: "Unova" });
      }
      else if (i > 649 && i <= 721) {
        this.genList.push({ PokemonId: i, GenName: "Kalos" });
      }
      else if (i > 721 && i <= 809) {
        this.genList.push({ PokemonId: i, GenName: "Alola" });
      }
      else {
        this.genList.push({ PokemonId: i, GenName: "Galar" });
      }
    }

    console.log(this.postType);
    let parseObj = JSON.parse(this.user);
    if (parseObj != null)
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
            let Favorite = result[i].Key.IsFavorite;


            //creates seperate cards shiny variants
            //only makes pushes to full user collection
            if (Amount > 0) {
              let Quantity = Amount;
              let SpriteLink = Link;
              let IsShiny = false;
              let IsFavorite = Favorite;
              let card: ICard = { PokemonId, Quantity, RarityId, SpriteLink, PokemonName, IsShiny, IsFavorite };
              this.fullUserCollection.push(card);
            }
            if (AmountShiny > 0) {
              let Quantity = AmountShiny;
              let SpriteLink = LinkShiny;
              let IsShiny = true;
              let IsFavorite = Favorite;
              let card: ICard = { PokemonId, Quantity, RarityId, SpriteLink, PokemonName, IsShiny, IsFavorite };
              this.fullUserCollection.push(card);
            }

          }
          this.filterCollection();//filter collection after intializing
        }

      );
      this._cardcollectionService.GetRarityList().subscribe(//grabs rarity list for filtering
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
    this.userCollection = [];//reset user collection 

    //checks edge cases for different filter combination
    if (this.filterValue == 0) {
      if (this.filterValueShiny == false) {
        if (this.genValue == "Any") {
          this.userCollection = this.fullUserCollection;
        } else {
          this.fullUserCollection.forEach(element => {
            if (element.IsShiny == this.filterValueShiny) {
              let generation = this.genList.filter(x => x.PokemonId == element.PokemonId)[0];
              //console.log(generation);
              if (generation.GenName == this.genValue) {
                this.userCollection.push(element);
              }
            }
          });
        }
      }
      else {
        this.fullUserCollection.forEach(element => {
          if (element.IsShiny == this.filterValueShiny) {
            if (this.genValue == "Any") {
              this.userCollection.push(element);
            } else {
              let generation = this.genList.filter(x => x.PokemonId == element.PokemonId)[0];
              //console.log(generation);
              if (generation.GenName == this.genValue) {
                this.userCollection.push(element);
              }
            }
          }
        });
      }

    }
    else {
      this.fullUserCollection.forEach(element => {
        if (this.filterValueShiny == false) {
          if (element.RarityId == this.filterValue) {
            if (this.genValue == "Any") {
              this.userCollection.push(element);
            } else {
              let generation = this.genList.filter(x => x.PokemonId == element.PokemonId)[0];
              if (generation.GenName == this.genValue) {
                this.userCollection.push(element);
              }
            }
          }
        }
        else {
          if (element.RarityId == this.filterValue && element.IsShiny == this.filterValueShiny) {
            if (this.genValue == "Any") {
              this.userCollection.push(element);
            } else {
              let generation = this.genList.filter(x => x.PokemonId == element.PokemonId)[0];
              if (generation.GenName == this.genValue) {
                this.userCollection.push(element);
              }
            }
          }
        }
      });
    }

    if (this.checkFavorites) {//check for favorites
      this.userCollection = this.userCollection.filter(x => x.IsFavorite == true);
    }

  }




  OnSubmit(postForm: NgForm) {
    this.isPriceValid = false;
    this.isPostTypeSelect = false;
    this.isPokemonId = false;
    this.isDescription = false;
    if (postForm.value.postType === 'Sale') { //price only appears in sale posts
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

    if (postForm.value.textDescription === "") {
      this.isDescription = true;
      return;
    }

    if (postForm.value.postType != 'Discussion') { //pokemon cards dont appear in discussion posts
      if (postForm.value.PokemonId == null || postForm.value.PokemonId.length == 0) {
        this.isPokemonId = true;
        return;
      }


      var card: any;

      for (var cards of this.userCollection) {
        // console.log(`${cards.PokemonId} === ${postForm.value.PokemonId}`)
        if (cards.PokemonId == postForm.value.PokemonId) {
          card = cards;
          break;
        } else {
          card = null;
        }
      }
    }

    console.log("a post was created");
    let post: FullPost = {

      pokemonId: postForm.value.postType !== 'Discussion' ? card.PokemonId : 0,
      postTime: new Date(),
      postDescription: postForm.value.textDescription,
      price: postForm.value.postType === 'Sale' ? postForm.value.Price : 0,
      stillAvailable: postForm.value.postType === 'Sale' ? true : false,
      isShiny: postForm.value.postType !== 'Discussion' ? card.IsShiny : false,
      userId: parseInt(this.userId),
      userName: this.userName,
      spriteLink: postForm.value.postType !== 'Discussion' ? card.SpriteLink : "",
      postType: postForm.value.postType,
      pokemonName: postForm.value.postType !== 'Discussion' ? card.PokemonName ? card.pokemonName : '' : '',
      rarityId: postForm.value.postType !== 'Discussion' ? card.RarityId : 0,
    };

    console.log(post);

    this._createPostService.CreatePost(post).subscribe(
      result => {
        console.log("we added an post" + result);
      },
      error => {
        console.log(error)
      }
    );

  }

  GetRarityDisplay(rarityId: any): string {
    switch (rarityId) {
      case 1:
        return 'Common'
      case 2:
        return 'Uncommon'
      case 3:
        return 'Rare'
      case 4:
        return 'Mythic'
      case 5:
        return 'Lengendary'
      default:
        return 'No Card Attached'
    }
  }
}
