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

  //Coin Controller API service calls
  GetTopCurrentBalanceList():Observable<User[]>{//doesn't .Take() in the backend method work like TOP in SQL?
    return this.http.get<User[]>('https://localhost:44303/TopCurrentBalanceList?maxnumber=10');
  }
  GetTopEarnedCoinsList():Observable<User[]>{//doesn't .Take() work like TOP?
    return this.http.get<User[]>('https://localhost:44303/TopEarnedCoinsist?maxnumber=10');
  }
  GetTopSpentCoinsList():Observable<User[]>{//doesn't .Take() work like TOP?
    return this.http.get<User[]>('https://localhost:44303/TopSpentCoinsist?maxnumber=10');
  }

  //Shining Controller API service calls. Currently not in main, so commented out since unsure of observable structure
  GetMostShiningList():Observable<MVPShiny[]>{//doesn't .Take() work like TOP?
    return this.http.get<MVPShiny[]>('https://localhost:44303/"[action]/{userMost}"');
  }


}
