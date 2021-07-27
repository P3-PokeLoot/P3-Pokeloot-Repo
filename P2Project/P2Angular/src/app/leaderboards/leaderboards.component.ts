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
    mockShinyDataArray:MockShinyData[] = MockShinyStatisticData;
    statisticslist:string[]=[
      "shinystats",
      "coinstats",
      "poststats"
    ];
    //adding DI now, since likely will use this service to communicate w/ the stats API
  constructor( private _leaderboardservice:LeaderboardStatsService) { }

  ngOnInit(): void {
  }

  QueryFunction(){

  }
}
