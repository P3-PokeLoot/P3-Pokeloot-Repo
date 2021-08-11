import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit, Input, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { DisplayServiceService } from '../display-service.service';
import { FriendServiceService } from '../friend-service.service';
import { IBuy } from '../Models/IBuy';
import { IPost } from '../Models/IPost';
import { IType } from './IType';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  @Input() search!: string;
  fullDisplayBoard!: IPost[];
  displayBoard!: IPost[];
  oldSearch: string = "null";
  attemptToBuy: boolean = false;
  broughtCard?: IBuy;
  userId: any = localStorage.getItem('userId');
  pageOfItems!: IPost[];
  //@Input() changePage = EventEmitter<any>(true);
  currentIndex : number = 0;
  currentPage: number = 1;
  lastpage!: number;
  filterValue: number = 0;
  currentPost: number = 0;
  typeList : IType[] = [{TypeId : 1, TypeName: "Discussion"}, {TypeId : 2, TypeName: "Display"}, {TypeId : 3, TypeName: "Sale"}];
  bublapedia: string = 'https://bulbapedia.bulbagarden.net/wiki/';
  edit:boolean = false;
  friendAction?: string;

  constructor(private route: Router, private _displayService: DisplayServiceService, private cdr: ChangeDetectorRef, private _friendService: FriendServiceService) {
    this.displayBoard = [];
    this.fullDisplayBoard = [];
  }

  //we should edit the api to also recieve the original username of poster
  ngOnInit(): void {
    this.attemptToBuy = false;
    //this.changePage.emit(this.pageOfItems);
    this._displayService.DisplayBoard().subscribe(
      result => {
        for (let i = 0; i < result.length; i++) {
          let PostId = result[i].postId;
          let PokemonId = result[i].pokemonId;
          let PostTime = result[i].postTime;
          let PostDescription = result[i].postDescription;
          let Price = result[i].price;
          let StillAvailable = result[i].stillAvailable;
          let IsShiny = result[i].isShiny;
          let UserId = result[i].userId;
          let type = result[i].postType;
          let UserName = result[i].userName;
          let SpriteLink = result[i].spriteLink;
          let PostType = '';
          if (type == 1) {
            PostType = 'Discussion';
          }
          else if (type == 2) {
            PostType = 'Sale';
          }
          else {
            PostType = 'Display';
          }
          let PokemonName = result[i].pokemonName;
          let RarityId = result[i].rarityId;

          let Post: IPost = { PostId, PokemonId, PostTime, PostDescription, Price, StillAvailable, IsShiny, UserId, UserName, SpriteLink, PostType, PokemonName, RarityId }
          this.fullDisplayBoard.push(Post);
        }
        //this.displayBoard = this.fullDisplayBoard.filter( x => x.PokemonName?.includes(this.search) || x.PokemonName == null);

        this.displayBoard = this.filterPost(this.fullDisplayBoard);
        
      }
    )
  }

  filterPost(board : IPost[]):IPost[]{ //applys filter to post
    let oldLength = this.lastpage;
    //if(this.oldSearch != this.search){
    if(this.filterValue == 0){  
    this.displayBoard = this.fullDisplayBoard.filter( x => x.PokemonName?.includes(this.search) || x.PokemonName == null);
    this.load();
    }else{
      if(this.filterValue == 1){
        this.displayBoard = this.fullDisplayBoard.filter(x => x.PostType == "Discussion");
        this.load();
      }else if(this.filterValue == 2){
        this.displayBoard = this.fullDisplayBoard.filter( x => x.PokemonName?.includes(this.search)).filter(x => x.PostType == "Display");
        this.load();
      }else{
        this.displayBoard = this.fullDisplayBoard.filter( x => x.PokemonName?.includes(this.search)).filter(x => x.PostType == "Sale");
        this.load();
      }
    }
    //this.onChangePagePrev();
    //}
      
 
    //this.oldSearch = this.search;
    return this.displayBoard;
  }

  load(){ //handles pagination
    console.log("collenction length = " + this.displayBoard.length);
    this.currentIndex = 0;
    this.currentPage = 1;
    this.lastpage = 1 + Math.floor(this.displayBoard.length / 5);
    this.pageOfItems = this.displayBoard.slice(this.currentIndex, this.currentIndex + 5);
  }

  buy(post: IPost): void { //buys a card from a post
    this.attemptToBuy = true; //used to display output from attempted purchase
    //Ouput: string,
    //Result: boolean,
    let Price = post.Price;
    let UserName = post.UserName;
    let SpriteLink = post.SpriteLink;
    let PokemonName = post.PokemonName;
    let RarityId = post.RarityId;
    let IsShiny = post.IsShiny;

    this._displayService.getBuyCard(post.PostId, this.userId).subscribe(
      result => {
        let Output = result[0].Key;
        let Result = result[0].Value;
        if (Result == false) {
          RarityId = 6;
        }

        this.broughtCard = { Output, Result, Price, UserName, SpriteLink, PokemonName, RarityId, IsShiny };
      }
    )
  }


  clickt(post: IPost){ //opens edit post options
    this.edit = !this.edit; //can close after opened
    this.currentPost = post.PostId; //only one post can be edited at a time

  }

  GetCardColor(rarityId: any): string {
    switch (rarityId) {
      case 1:
        return 'card1'
        break;
      case 2:
        return 'card2'
        break;
      case 3:
        return 'card3'
        break;
      case 4:
        return 'card4'
        break;
      case 5:
        return 'card5'
        break;
      case 6:
        return 'card6'
        break
      default:
        return ''
        break;
    }
  }

  OnSubmit(searchForm: NgForm) {
    this.search = searchForm.value.search;
    console.log(this.search);
    this.displayBoard = this.filterPost(this.fullDisplayBoard);
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
        return 'Mythic'
        break;
      case 5:
        return 'Legendary'
        break;
      default:
        return 'No Card Attached'
        break;
    }
  }

  ngAfterViewInit() {//detects changes to posts
    
    this.cdr.detectChanges();
  }

  onChangePageNext() {
    // update current page of items
    this.currentIndex += 5;
    if(this.currentIndex >= this.displayBoard.length - 5){
      this.currentIndex = this.displayBoard.length - 5;
      this.currentPage = this.lastpage - 1;
    }
    this.currentPage++;
    console.log(this.currentIndex);
    this.pageOfItems = this.displayBoard.slice(this.currentIndex, this.currentIndex + 5);
    //this.pageOfItems = pageOfItems;
}
  onChangePagePrev() {
    // update current page of items
    this.currentIndex -= 5;
    this.currentPage--;
    if(this.currentIndex <= 0){
      this.currentIndex = 0;
      this.currentPage = 1;
    }
    console.log(this.currentIndex);
    this.pageOfItems = this.displayBoard.slice(this.currentIndex, this.currentIndex + 5);
    //this.pageOfItems = pageOfItems;
  }

  addFriend(friend: IPost){ //performs a friend action on author on post
    if(this.userId){
    this._friendService.FriendActionWithANumber(this.userId, friend.UserId).subscribe(
      result => {
        this.friendAction = result[0];
        console.log(result);
        },
      error => {
        console.log(error.error);
        //From Alain: for some reason the call is returning an error due to parsing issues, the status code is 200 and the database is succefully updated on each call.
        //Since the issue is unresolved, i instead grabbed the error text which actually holds the output intended from the api
        this.friendAction = error.error.text;
        console.log(this.friendAction);
      }
    );
    }
   //this.refresh();
  }

  refresh(){ //reloads home page
    this.route.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.route.navigate(['/Home']);
    });
    this.route.navigate(['/Home']);
    this.route.navigateByUrl('/Home');
  }
}