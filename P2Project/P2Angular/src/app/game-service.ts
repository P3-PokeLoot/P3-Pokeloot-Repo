import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler } from "@angular/common/http";
import { Observable, of, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})

export class GameService {


  private gameListUrl: string = 'https://localhost:44301/api/Games/List';
  private createGameUrl: string = 'https://localhost:44301/api/Games/CreateGame'

  private gameUrlPath = `${environment.urlmain}EarnCoins/`;
  private userBalanceUrlPath = `${environment.urlmain}Balance/`;
  private GamesUrl = `${environment.urlgame}`;


  constructor(private router: Router, private http: HttpClient) { }

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
    console.log("RPS win...");
    return this.http.get<any>(this.GamesUrl + "RpsWin/" + localStorage.getItem('userId'));
  }

  RpsLose(): Observable<any> {
    console.log("RPS lose...");
    return this.http.get(this.GamesUrl + "RpsLose/" + localStorage.getItem('userId'));
  }

  RpsRecord(): Observable<any> {
    console.log("RPS record...");
    return this.http.get(this.GamesUrl + "RpsRecord/" + localStorage.getItem('userId'), { responseType: 'text' });
  }

  WtpWin(): Observable<any[]> {
    console.log("WTP win...");
    return this.http.get<any>(this.GamesUrl + "WtpWin/" + localStorage.getItem('userId'));
  }

  CreateGame(gameForm: any) {
    return this.http.post<any>(this.createGameUrl, gameForm);
  }
  WtpLose(): Observable<any[]> {
    console.log("WTP lose...");
    return this.http.get<any>(this.GamesUrl + "WtpLose/" + localStorage.getItem('userId'));
  }

  WtpRecord(): Observable<any> {
    console.log("WTP record...");
    return this.http.get(this.GamesUrl + "WtpRecord/" + localStorage.getItem('userId'), { responseType: 'text' });
  }

  CapWin(): Observable<any[]> {
    return this.http.get<any>(this.GamesUrl + "CapWin/" + localStorage.getItem('userId'));
  }

  CapLose(): Observable<any[]> {
    return this.http.get<any>(this.GamesUrl + "CapLose/" + localStorage.getItem('userId'));
  }

  CapRecord(): Observable<any> {
    return this.http.get(this.GamesUrl + "CapRecord/" + localStorage.getItem('userId'), { responseType: 'text' });
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
  pictureUrl: string,
  correctPokemon: string,
  options: string[],
}
