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
  chosenNumber:number=0;

  //Coin Controller API service calls
  GetTopCurrentBalanceList():Observable<User[]>{//outputs a list of User type
    return this.http.get<User[]>('https://localhost:44303/TopCurrentBalanceList?maxnumber='+this.chosenNumber);
  }
  GetTopEarnedCoinsList():Observable<User[]>{//outputs a list of User type
    return this.http.get<User[]>('https://localhost:44303/TopEarnedCoinsist?maxnumber='+this.chosenNumber);
  }
  GetTopSpentCoinsList():Observable<User[]>{//outputs a list of User type
    return this.http.get<User[]>('https://localhost:44303/TopSpentCoinsist?maxnumber='+this.chosenNumber);
  }

  //Shining Controller API service calls.
  //outputs a list of MVPShiny type. Api route may need adjusting, since this method currently not in main - only exists in Guillermo's branch
  GetMostShiningList():Observable<MVPShiny[]>{
    return this.http.get<MVPShiny[]>('https://localhost:44303/"[action]/{userMost}"');
  }


}
