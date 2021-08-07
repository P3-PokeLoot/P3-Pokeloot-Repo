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
  isPending: boolean = true;
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
          let FriendSince = result[i].fateAdded;
          let IsPending= result[i].isPending;
          let TotalCards= result[i].totalCards;

          let friend:IFriend = {UserName, UserID, UserLevel, FriendSince, IsPending, TotalCards};
          //console.log(result[i]);
          this.fullFriendsList.push(friend);
          }
      },
      error => console.log(error)
    );
    }
    
    this.friendList = this.fullFriendsList.filter(x => x.IsPending == this.isPending);
    this.load();
    
  }


  filterForPending(){
    this.friendList = [];
    this.fullFriendsList.forEach( element =>{
      if(element.IsPending == this.isPending){
        this.friendList.push(element);
      }
      
    });
    this.isPending = !this.isPending;
    this.load();
  }

  load(){
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

  clickt(friend: IFriend){
    if(this.userId){
    this._friendService.FriendAction(this.userId, friend.UserID).subscribe(
      result => {this.friendAction = result[0];},
      error => {
        this.friendAction = error.text;
      }
    );
    }
   //this.refresh();
  }

  refresh(){
    this.route.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.route.navigate(['/Friends']);
    });
    this.route.navigate(['/Friends']);
    this.route.navigateByUrl('/Friends');
  }
}
