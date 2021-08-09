import { Component, OnInit } from '@angular/core';
import { LeaderboardStatsService } from '../service/leaderboards/leaderboard-stats.service';

@Component({
  selector: 'app-achievements-statistic',
  templateUrl: './achievements-statistic.component.html',
  styleUrls: ['./achievements-statistic.component.css']
})
export class AchievementsStatisticComponent implements OnInit {

  constructor(private _leaderboardservice:LeaderboardStatsService) {}

  //temporary array to hold aggregate achievement data. 
  tempArray:any[]=[];
  
  ngOnInit(): void {

  }

  GetAggregateAchievmentData(){
    this._leaderboardservice.GetListAllUserAchievements().subscribe(
      result => {
        this.tempArray = result;
      });
  //Method to display a histogram chart of different achievements
  
  }
}
