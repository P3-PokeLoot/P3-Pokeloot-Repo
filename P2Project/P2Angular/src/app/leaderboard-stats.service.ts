import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MockShinyStatisticData } from './leaderboards/MockShinyData';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardStatsService {

  constructor(private http:HttpClient) { }
  //insert methods to route to StatsAPI. for now just use mockdata
  // GetStat():Observable<StatType>{
  //   return this.http.get<StatType>('StatAPIurl');
  // }
  GetData(){
    return MockShinyStatisticData
  }
}
