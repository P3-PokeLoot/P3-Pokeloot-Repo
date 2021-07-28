import { Component, OnInit } from '@angular/core';
import { LeaderboardStatsService } from '../leaderboard-stats.service';
import { MockShinyData } from './IMockShinyData';
import { MockShinyStatisticData } from './MockShinyData';

@Component({
  selector: 'app-leaderboards',
  templateUrl: './leaderboards.component.html',
  styleUrls: ['./leaderboards.component.css']
})
export class LeaderboardsComponent implements OnInit {

    //for now we will simply assign an observable into a property of the Display class - will refactor after service creation
    //mockShinyDataArray:MockShinyData[] = MockShinyStatisticData;
    observableDataFromMethods:[]=[];
    allStatsList:string[]=[
      'shinystats',
      'coinstats',
      'poststats'
    ];
    currentChosenStat:string='';
    s1:string='shinystats';
    s2:string='coinstats';
    s3:string='poststats';
    //adding DI now, since likely will use this service to communicate w/ the stats API
  constructor(private _leaderboardservice:LeaderboardStatsService) { }

  ngOnInit(): void {
    
  }

  OnClickQueryFunction(chosenDropdownList:string){
     let counter:string = chosenDropdownList;
    switch(counter){
      case 'shinystats': 
      if (counter = 'shinystats') {
        this.observableDataFromMethods != this._leaderboardservice.GetMockShinyData()}//when we get actual observable streams, then add "subscribe"
      break; 
      case 'coinstats': 
      if (counter = 'coinstats') {
        this.observableDataFromMethods != this._leaderboardservice.GetMockCoinData()}
      break; 
      case 'poststats': 
      if (counter = 'poststats') {
        this.observableDataFromMethods !=this._leaderboardservice.GetMockPostData()}
      break; 
    }
  }
}
