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

  constructor(private route: Router, private _friendService : FriendServiceService) { }

  ngOnInit(): void {

    let friend1: IFriend = {UserName:"Alain", UserID:"1", UserLevel:50, FriendSince: new Date(), IsPending: false, TotalCards:100}
    let friend2: IFriend = {UserName:"Brian", UserID:"2", UserLevel:150, FriendSince: new Date(), IsPending: false, TotalCards:200}
    let friend3: IFriend = {UserName:"Mason", UserID:"3", UserLevel:250, FriendSince: new Date(), IsPending: true, TotalCards:300}
    let friend4: IFriend = {UserName:"John", UserID:"4", UserLevel:350, FriendSince: new Date(), IsPending: false, TotalCards:400}
    let friend5: IFriend = {UserName:"Mark", UserID:"5", UserLevel:450, FriendSince: new Date(), IsPending: false, TotalCards:500}
    let friend6: IFriend = {UserName:"Malia", UserID:"6", UserLevel:650, FriendSince: new Date(), IsPending: true, TotalCards:600}
    let friend7: IFriend = {UserName:"Greg", UserID:"7", UserLevel:750, FriendSince: new Date(), IsPending: false, TotalCards:800}
    let friend8: IFriend = {UserName:"Adrian", UserID:"8", UserLevel:850, FriendSince: new Date(), IsPending: false, TotalCards:800}
    let friend9: IFriend = {UserName:"Christian", UserID:"9", UserLevel:950, FriendSince: new Date(), IsPending: false, TotalCards:100}

    this.fullFriendsList.push(friend1);
    this.fullFriendsList.push(friend2);  
    this.fullFriendsList.push(friend3);  
    this.fullFriendsList.push(friend4);  
    this.fullFriendsList.push(friend5);  
    this.fullFriendsList.push(friend6);  
    this.fullFriendsList.push(friend7);  
    this.fullFriendsList.push(friend8);  
    this.fullFriendsList.push(friend9);
    
    this.filterForPending();
    
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
   this.refresh();
  }

  refresh(){
    this.route.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.route.navigate(['/Friends']);
    });
    this.route.navigate(['/Friends']);
    this.route.navigateByUrl('/Friends');
  }
}
