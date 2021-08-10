import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { User } from '../profile-page/IUser';
import { LeaderboardStatsService } from '../service/leaderboards/leaderboard-stats.service';

@Component({
  selector: 'app-achievements-statistic',
  templateUrl: './achievements-statistic.component.html',
  styleUrls: ['./achievements-statistic.component.css']
})
export class AchievementsStatisticComponent implements OnInit {

  constructor(private _leaderboardservice:LeaderboardStatsService, private _accservice:AccountService) {}
  //class properties
  currentProfile:User= {     UserId:0,
    FirstName:'',
    LastName:'',
    UserName:'',
    Password:'',
    Email:'',
    CoinBalance:0,
    AccountLevel:0,
    TotalCoinsEarned:0};

  //temporary array to hold aggregate achievement data. 
  tempArray:any[]=[];
  
  ngOnInit(): void {
    this._accservice.GetUserProfile().subscribe(
      x => {this.currentProfile = x; console.log('succesfully retrieved object for userId')},
      y => {console.log(`there was an error ${y}`)}
    );
  }

  GetAggregateAchievmentData(){
    this._leaderboardservice.GetListAllUserAchievements().subscribe(
      result => {
        this.tempArray = result;
      });
  //Method to display a histogram chart of different achievements
  
  }
}
