import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MockShinyStatisticData } from './leaderboards/MockShinyData';
import { MockCoinStatisticData } from './leaderboards/MockCoinData';
import { MockPostStatisticData } from './leaderboards/MockPostData';
import { MockCoinData } from './leaderboards/IMockCoinData';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardStatsService {

  constructor(private http:HttpClient) { }
  //insert methods to route to StatsAPI. for now just use mockdata
  // GetStat():Observable<StatType>{
  //   return this.http.get<StatType>('StatAPIurl');
  // }
  GetMockShinyData(){
    return MockShinyStatisticData
  }
  // GetStat():Observable<StatType>{
  //   return this.http.get<StatType>('StatAPIurl');
  // }
  GetMockCoinData(){
    return MockCoinStatisticData
  }
  GetMockPostData(){
    return MockPostStatisticData
  }
}
