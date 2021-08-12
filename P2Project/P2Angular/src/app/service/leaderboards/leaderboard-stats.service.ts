import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserModel_Coin } from 'src/app/Models/IUser_CoinLB'; 
import { TopPercentModel_Coin } from 'src/app/Models/ITopPercentModel_CoinLB';
import { UserModel_Rarity } from 'src/app/Models/IUser_RarityLB';
import { CardCollectModel_Shiny } from 'src/app/Models/ICardCollect_ShinyLB';
import { MVPShinyModel_Shiny } from 'src/app/Models/IMVPShiny_ShinyLB';
import { UserCollectionModel_Shiny } from 'src/app/Models/IUserCollection_ShinyLB';
import { PercentageOwnCardModel_Shiny } from 'src/app/Models/IPercentageOwnCard_ShinyLB';
import { UserCollectionModel_Shiny2 } from 'src/app/Models/IUserCollection_ShinyLB2';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardStatsService {

  constructor(private http:HttpClient) { }
  //Every method takes in an input, which will be the requested number of objects requested from Db. 
  //This input will be a property on the leaderboards component called 'chosenNumber'. 
  baseURL = `${environment.urlstat}`;

  //Coin Controller API service calls
  GetTopCurrentBalanceList(input:number):Observable<UserModel_Coin[]>{//outputs a list of User type
    return this.http.get<UserModel_Coin[]>(this.baseURL+'TopCurrentBalanceList?maxnumber='+input);
  }
  GetTopEarnedCoinsList(input:number):Observable<UserModel_Coin[]>{//outputs a list of User type
    return this.http.get<UserModel_Coin[]>(this.baseURL+'TopEarnedCoinsist?maxnumber='+input);
  }
  GetTopSpentCoinsList(input:number):Observable<UserModel_Coin[]>{//outputs a list of User type
    return this.http.get<UserModel_Coin[]>(this.baseURL+'TopSpentCoinsist?maxnumber='+input);
  }

  GetTopCompletedCollection(input:number):Observable<TopPercentModel_Coin[]>{
    return this.http.get<TopPercentModel_Coin[]>(this.baseURL+'TopCompletedCollection?maxnumber='+input);
  }

  //Rarity Controller API service calls - change the model
  GetUserListbyRarity(rarity:string,input:number):Observable<UserModel_Rarity[]>{
    return this.http.get<UserModel_Rarity[]>(this.baseURL+'UserListByMostRarityCategory?rarityCategory='+rarity+'+&maxnumber='+input);
  }
  // These are likely going to need their own request - w/ diff constructor
  //GetPercentOfRarityCategory(user:number,input:number):number{
  //   return this.http.get()
  // }
  // GetTotalRarityCategoryForUser(){}

  
  //ShiningPokemon Controller API service calls.
  // GetMostShinyList(userMost:number):Observable<CardCollectModel_Shiny[]>{//May need to be refactored based on the data retruned
  //   return this.http.get<CardCollectModel_Shiny[]>(this.baseURL+'/ShiningPokemon/DisplayUserMostShinyPokemon/'+userMost);
  // }

  GetMVPShinyUsersList(topUsers:number):Observable<MVPShinyModel_Shiny[]>{
    return this.http.get<MVPShinyModel_Shiny[]>(this.baseURL+'ShiningPokemon/MVPShinyUsers/'+topUsers);
  }
  
  GetUsersTotalCollectionList(input:number):Observable<UserCollectionModel_Shiny[]>{
    return this.http.get<UserCollectionModel_Shiny[]>(this.baseURL+'ShiningPokemon/UsersTotalCollection/'+input);
  }
  GetUserTotalAmountList(topUser:number):Observable<UserCollectionModel_Shiny2[]>{
    return this.http.get<UserCollectionModel_Shiny2[]>(this.baseURL+'ShiningPokemon/GetTotalCardUserHave/'+topUser);
  }
  GetCardPercentage(pokename:string):Observable<PercentageOwnCardModel_Shiny>{
    return this.http.get<PercentageOwnCardModel_Shiny>(this.baseURL+'ShiningPokemon/GetCardPorcentage/'+pokename);
  }


  //achievement services are here, just in case they can also be used in the leaderboards component. 
  GetListSingleUserAchievements(userId:number):Observable<any>{
    return this.http.get<any>(this.baseURL+'UserArchievementsList?userId='+userId);
  }
  GetListAllUserAchievements():Observable<any>{
    return this.http.get<any>(this.baseURL+'UserAchievements');
  }
}
