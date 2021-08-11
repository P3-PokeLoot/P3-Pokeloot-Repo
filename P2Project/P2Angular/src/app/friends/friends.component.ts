import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FriendServiceService } from '../friend-service.service';
import { IFriend } from './ifriend';


@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {

  private userId = localStorage.getItem('userId');
  pageOfItems!: IFriend[];
  //@Input() changePage = EventEmitter<any>(true);
  currentIndex : number = 0;
  currentPage: number = 1;
  lastpage!: number;
  fullFriendsList: IFriend[] = [];
  friendList: IFriend[] = [];
  isPending: boolean = false;
  selectFriend?: string;
  friendAction?: string;

  constructor(private route: Router, private _friendService : FriendServiceService) { }

  ngOnInit(): void {

    if(this.userId){
    this._friendService.GetFriendsList(this.userId).subscribe(
      result => {
        for (let i = 0; i < result.length; i++) {
          let UserName = result[i].friendName;
          let UserID = result[i].friendId;
          let UserLevel = result[i].friendLevel;
          let FriendSince = result[i].dateAdded;
          let IsPending= result[i].isPending;
          let TotalCards= result[i].totalCards;

          let friend:IFriend = {UserName, UserID, UserLevel, FriendSince, IsPending, TotalCards};
          //console.log(result[i]);
          this.fullFriendsList.push(friend);
          }
          this.friendList = this.fullFriendsList.filter(x => x.IsPending == this.isPending);
          this.isPending = !this.isPending; 
      },
      error => console.log(error),
      () => this.load() //only loads pagination after filtering and populating is done
    );
    }
    
    
    
  }


  filterForPending(){ //filters for pending request
   
    
    this.friendList = this.fullFriendsList.filter(x => x.IsPending == this.isPending);
    this.isPending = !this.isPending; //switches case after each click
    this.load();
    
    
    
  }

  load(){  //handles pagination
    console.log("collenction length = " + this.friendList.length);
    this.currentIndex = 0;
    this.currentPage = 1;
    this.lastpage = 1 + Math.floor(this.friendList.length / 5);
    this.pageOfItems = this.friendList.slice(this.currentIndex, this.currentIndex + 5);
  }

  onChangePageNext() {
    // update current page of items
    this.currentIndex += 5;
    if(this.currentIndex >= this.friendList.length - 5){
      this.currentIndex = this.friendList.length - 5;
      this.currentPage = this.lastpage - 1;
    }
    this.currentPage++;
    console.log(this.currentIndex);
    console.log("collenction length = " + this.friendList.length);
    this.pageOfItems = this.friendList.slice(this.currentIndex, this.currentIndex + 5);
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
    this.pageOfItems = this.friendList.slice(this.currentIndex, this.currentIndex + 5);
    //this.pageOfItems = pageOfItems;
  }

  clickt(friend: IFriend){ //accepts friend request
   
    if(this.userId){
      
    this._friendService.FriendAction(this.userId, friend.UserID).subscribe(
      result => {
        console.log(result);
        this.friendAction = result[0];},
      error => {
        console.log(error);
         //From Alain: for some reason the call is returning an error due to parsing issues, the status code is 200 and the database is succefully updated on each call.
        //Since the issue is unresolved, i instead grabbed the error text which actually holds the output intended from the api
        this.friendAction = error.error.text;
      }
    );
    }
    
  }

  refresh() { //refreshes friends page
    this.route.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.route.navigate(['/Friends']);
    });
    this.route.navigate(['/Friends']);
    this.route.navigateByUrl('/Friends');
  }
}
