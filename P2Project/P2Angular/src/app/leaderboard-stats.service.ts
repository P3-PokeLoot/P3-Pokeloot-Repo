import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from './Models/User';
import { MVPShiny } from './leaderboards/IMVPShiny';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardStatsService {

  constructor(private http:HttpClient) { }
  //Every method takes in an input, which will be the requested number of objects requested from Db. 
  //This input will be a property on the leaderboards component called 'chosenNumber'. 


  //Coin Controller API service calls
  GetTopCurrentBalanceList(input:number):Observable<User[]>{//outputs a list of User type
    return this.http.get<User[]>('https://localhost:44303/TopCurrentBalanceList?maxnumber='+input);
  }
  GetTopEarnedCoinsList(input:number):Observable<User[]>{//outputs a list of User type
    return this.http.get<User[]>('https://localhost:44303/TopEarnedCoinsist?maxnumber='+input);
  }
  GetTopSpentCoinsList(input:number):Observable<User[]>{//outputs a list of User type
    return this.http.get<User[]>('https://localhost:44303/TopSpentCoinsist?maxnumber='+input);
  }

  //Shining Controller API service calls.
  //outputs a list of MVPShiny type. Api route may need adjusting, since this method currently not in main - only exists in Guillermo's branch
  GetMostShiningList():Observable<MVPShiny[]>{
    return this.http.get<MVPShiny[]>('https://localhost:44303/"[action]/{userMost}"');
  }


}
