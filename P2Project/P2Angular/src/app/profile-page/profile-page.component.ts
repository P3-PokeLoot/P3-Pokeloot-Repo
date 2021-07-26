import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { CardServiceService } from '../card-service.service';
import { ICard } from '../cardcollect/ICard';
import { Badge } from './IBadge';
import { User } from './IUser';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {
  currentProfile!:User;
  KantoBadges:Badge[] =[
    {Title:'Youngster: Have a pokemon starter by unlocking your first pokemon',Completed:false},
    {Title:'TBD',Completed:false},
    {Title:'badge3',Completed:false},
    {Title:'Pokemon Ranger: Have over 100+ pokemon in your collection',Completed:false},
    {Title:'Chosen: Unlock a shiny pokemon',Completed:false},
    {Title:'TBD',Completed:false},
    {Title:'TBD',Completed:false},
    {Title:'Pokemon Master: Complete the Pokedex by collecting one of each pokemon',Completed:false},
    ];
  //Stretch goal - display achievements according to gender selection: F/M/NB?
  JohtoBadges:Badge[] =[
    {Title:'Starter: Earn 100 coins',Completed:false},
    {Title:'Duke: Earn 1000 coins',Completed:false},
    {Title:'Baron: Earn over 10000 coins!',Completed:false},
    {Title:'Youngster Joey: Registered a new account!',Completed:false}, //maybe including email verification for this?
    {Title:'PokeKid: Have an account for one year',Completed:false},
    {Title:'Grunt: Reach account level 10',Completed:false},
    {Title:'Leader: Reach account level 50',Completed:false},
    {Title:'Ace Trainer: Reach account level 100!',Completed:false},
    ];
  HoennBadges:Badge[] =[
      {Title:'TBD',Completed:false},
      {Title:'TBD',Completed:false},
      {Title:'TBD',Completed:false},
      {Title:'TBD',Completed:false},
      {Title:'TBD',Completed:false},
      {Title:'TBD',Completed:false},
      {Title:'TBD',Completed:false},
      {Title:'TBD',Completed:false},
      ];


  constructor(private _accservice:AccountService) { }

  ngOnInit(): void {
    this._accservice.GetUserProfile().subscribe(
      x => {this.currentProfile = x; console.log('succesfully retrieved object for userId')},
      y => {console.log(`there was an error ${y}`)}
    );

    
    this.ViewJohto(this.currentProfile,this.JohtoBadges);
  }

  //method to check 'Collection' achievements - needs front end method in cardcollect/card service to store a collection into
  //localstore
  /*ViewKanto(userCOLLECTION, _kantoBadges:Badge[]){
  }*/
  


  ViewJohto(currentprof:User, johtobadges:Badge[]){
    this.currentProfile = currentprof;
    this.JohtoBadges=johtobadges;
    if(this.currentProfile.TotalCoinsEarned>=10000){
      this.JohtoBadges[0].Completed = true;
      this.JohtoBadges[1].Completed = true;
      this.JohtoBadges[2].Completed = true;
    }
    if(this.currentProfile.TotalCoinsEarned>=1000){
      this.JohtoBadges[0].Completed = true;
      this.JohtoBadges[1].Completed = true;
    }
    if(this.currentProfile.TotalCoinsEarned>=100){
      this.JohtoBadges[0].Completed = true;
    }
    //this.currentProfile.TotalCoinsEarned > 100 ? this.JohtoBadges[0].Completed = true : false;
    //this.currentProfile.TotalCoinsEarned > 1000 ? this.JohtoBadges[1].Completed = true : false;
    //this.currentProfile.TotalCoinsEarned > 10000 ? this.JohtoBadges[2].Completed = true : false;
    //Register account
    this.currentProfile != null ? this.JohtoBadges[3].Completed = true: false;
    //Account age > 1
    this.currentProfile != null ? this.JohtoBadges[4].Completed = true: false;
    this.currentProfile.AccountLevel > 10 ? this.JohtoBadges[5].Completed = true : false;
    this.currentProfile.AccountLevel > 50 ? this.JohtoBadges[6].Completed = true : false;
    this.currentProfile.AccountLevel > 100 ? this.JohtoBadges[7].Completed = true : false;
  } 

  //Method to check 'Posts' achievements - needs backend method to send all of a User's posts
  /*ViewHoenn(userPOSTS, hoennbadges:Badge[]){


  }*/

}
