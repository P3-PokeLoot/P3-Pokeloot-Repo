import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserModel_Coins } from './Models/IUser_Coin';
import { MVPModel_Shiny } from './Models/IMVPShiny';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardStatsService {

  constructor(private http:HttpClient) { }
  //Every method takes in an input, which will be the requested number of objects requested from Db. 
  //This input will be a property on the leaderboards component called 'chosenNumber'. 
  baseURL:string='https://localhost:44303';

  //Coin Controller API service calls
  GetTopCurrentBalanceList(input:number):Observable<UserModel_Coins[]>{//outputs a list of User type
    return this.http.get<UserModel_Coins[]>(this.baseURL+'/TopCurrentBalanceList?maxnumber='+input);
  }
  GetTopEarnedCoinsList(input:number):Observable<UserModel_Coins[]>{//outputs a list of User type
    return this.http.get<UserModel_Coins[]>(this.baseURL+'/TopEarnedCoinsist?maxnumber='+input);
  }
  GetTopSpentCoinsList(input:number):Observable<UserModel_Coins[]>{//outputs a list of User type
    return this.http.get<UserModel_Coins[]>(this.baseURL+'/TopSpentCoinsist?maxnumber='+input);
  }

  //Shining Controller API service calls.
  //outputs a list of MVPShiny type. Api route may need adjusting, since this method currently not in main - only exists in Guillermo's branch
  GetMostShiningList():Observable<MVPModel_Shiny[]>{
    return this.http.get<MVPModel_Shiny[]>(this.baseURL+'/"[action]/{userMost}"');
  }


}
