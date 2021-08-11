import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})

export class GameService {

  private gameUrlPath = `${environment.urlmain}EarnCoins/`;
  private userBalanceUrlPath = `${environment.urlmain}Balance/`;
  private GamesUrl = `${environment.urlgame}`;
  private createGameUrl: string = `${environment.urlgame}CreateGame`;


  constructor(private http: HttpClient) { }

  GetGamesList(): Observable<GameInfo[]> {
    return this.http.get<GameInfo[]>(this.GamesUrl + "List");
  }

  GetWtpGame(): Observable<WtpGame> {
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
    return this.http.post<any>(this.GamesUrl + "RpsWin?userId=" + localStorage.getItem('userId'), {});
  }

  RpsLose(): Observable<any> {
    return this.http.post(this.GamesUrl + "RpsLose?userId=" + localStorage.getItem('userId'), {});
  }

  RpsRecord(): Observable<any> {
    return this.http.get(this.GamesUrl + "RpsRecord/" + localStorage.getItem('userId'), { responseType: 'text' });
  }

  WtpWin() {
    return this.http.post<any>(this.GamesUrl + "WtpWin?userId=" + localStorage.getItem('userId'), {});
  }

  CreateGame(gameForm: any) {
    return this.http.post<any>(this.createGameUrl, gameForm);
  }

  WtpLose(): Observable<any> {
    return this.http.post(this.GamesUrl + "WtpLose?userId=" + localStorage.getItem('userId'), {});
  }

  WtpRecord(): Observable<any> {
    return this.http.get(this.GamesUrl + "WtpRecord/" + localStorage.getItem('userId'), { responseType: 'text' });
  }

  CapWin(): Observable<any> {
    return this.http.post(this.GamesUrl + "CapWin?userId=" + localStorage.getItem('userId'), {});
  }

  CapLose(): Observable<any> {
    return this.http.post(this.GamesUrl + "CapLose?userId=" + localStorage.getItem('userId'), {});
  }

  CapRecord(): Observable<any> {
    return this.http.get(this.GamesUrl + "CapRecord/" + localStorage.getItem('userId'), { responseType: 'text' });
  }

  WamPlayed(score:number): Observable<any> {
    return this.http.put(this.GamesUrl + "WamPlayed/" + localStorage.getItem('userId')+"?highScore="+score, {});
  }

  WamRecord(): Observable<any> {
    return this.http.get(this.GamesUrl + "WamRecord/" + localStorage.getItem('userId'), { responseType: 'text' });
  }

  DeleteGame(id: number): Observable<any> {
    return this.http.delete<any>(this.GamesUrl + `Delete/${id}`)
  }

  ModifyGame(gameForm: any): Observable<any> {
    return this.http.patch<any>(this.GamesUrl + `ModifyGame`, gameForm)
  }

  SingleGame(id: number): Observable<any> {
    return this.http.get(this.GamesUrl + `SingleGame/${id}`);
  }
}

export interface WtpGame {
  PictureUrl: string,
  CorrectPokemon: string,
  Options: string[],
};

export interface GameInfo {
  description: string,
  imgUrl: string,
  route: string
};