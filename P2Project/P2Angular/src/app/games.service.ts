import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GamesService {

  constructor(private http: HttpClient) { }

  private BaseUrl = "localhost";
  private GamesAPIUrl = "api/game/"; //Not sure how this is going to work once kubernetes-itized
  private WtpUrl = `${environment.urlgame}Wtp`;
  private gameUrlPath = `${environment.urlmain}EarnCoins/`;
  private userBalanceUrlPath = `${environment.urlmain}Balance/`;
  private GamesUrl = `${environment.urlgame}`;
  
  GetGamesList(): Observable<GameInfo[]>{
    return this.http.get<GameInfo[]>(this.BaseUrl + this.GamesAPIUrl + "list");
  }

  GetWtpGame(): Observable<WtpGame>{
    return this.http.get<WtpGame>(this.GamesUrl + "Wtp")
  }

  AddCoins(amountCoins: number): Observable<any[]> {
    return this.http.get<any>(this.gameUrlPath + localStorage.getItem('userId') + '/' + amountCoins)
  }

  GetBalance(): Observable<any[]> {
    return this.http.get<any>(this.userBalanceUrlPath + localStorage.getItem('userId'))
  }

  // Grabs the list of games desciption that are avaiable to be displayed in the game page.
  GetList(): Observable<any[]> {
    return this.http.get<any>(this.GamesUrl + "List");
  }
    
  RpsWin(): Observable<any> {
    console.log("RPS win...");
    return this.http.get<any>(this.GamesUrl + "RpsWin/" + localStorage.getItem('userId'));
  }

  RpsLose(): Observable<any>{
    console.log("RPS lose...");
    return this.http.get(this.GamesUrl + "RpsLose/" + localStorage.getItem('userId'));
  }

  RpsRecord(): Observable<any> {
    console.log("RPS record...");
    return this.http.get(this.GamesUrl + "RpsRecord/" + localStorage.getItem('userId'), {responseType: 'text'});
  }

  WtpWin(): Observable<any[]> { 
    console.log("WTP win...");
    return this.http.get<any>(this.GamesUrl + "WtpWin/" + localStorage.getItem('userId'));
  }

  WtpLose(): Observable<any[]> {
    console.log("WTP lose...");
    return this.http.get<any>(this.GamesUrl + "WtpLose/" + localStorage.getItem('userId'));
  }
  
  WtpRecord(): Observable<any> {
    console.log("WTP record...");
    return this.http.get(this.GamesUrl + "WtpRecord/" + localStorage.getItem('userId'), {responseType: 'text'});
  }

  CapWin(): Observable<any[]> {
    return this.http.get<any>(this.GamesUrl + "CapWin/" + localStorage.getItem('userId'));
  }

  CapLose(): Observable<any[]> {
    return this.http.get<any>(this.GamesUrl + "CapLose/" + localStorage.getItem('userId'));
  }
  
  CapRecord(): Observable<any> {
    return this.http.get(this.GamesUrl + "CapRecord/" + localStorage.getItem('userId'), {responseType: 'text'});
  }
}

//Just an idea so far - Subject to change upon adding into the database
export interface GameInfo
{
  description: string,
  imgUrl: string,
  route: string
};

export interface WtpGame
{
  pictureUrl: string,
  correctPokemon: string,
  options: string[],
}

