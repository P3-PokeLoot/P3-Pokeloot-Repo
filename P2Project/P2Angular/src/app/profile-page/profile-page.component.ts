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
  currentProfile:User= {     UserId:0,
  FirstName:'',
  LastName:'',
  UserName:'',
  Password:'',
  Email:'',
  CoinBalance:0,
  AccountLevel:0,
  TotalCoinsEarned:0};

  JohtoBadges:Badge[] =[
    {Title:'Starter: Earn 100 coins',Completed:false},
    {Title:'Duke: Earn 1000 coins',Completed:false},
    {Title:'Baron: Earn over 10000 coins!',Completed:false},
    {Title:'Youngster Joey: Registered a new account!',Completed:false}, 
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
    this.currentProfile != null ? this.JohtoBadges[3].Completed = true: false;
    this.currentProfile != null ? this.JohtoBadges[4].Completed = false: true;
    this.currentProfile.AccountLevel > 10 ? this.JohtoBadges[5].Completed = true : false;
    this.currentProfile.AccountLevel > 50 ? this.JohtoBadges[6].Completed = true : false;
    this.currentProfile.AccountLevel > 100 ? this.JohtoBadges[7].Completed = true : false;
  } 

}
