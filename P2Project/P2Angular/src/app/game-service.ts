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


  private gameUrlPath = `${environment.urlmain}EarnCoins/`;
  private userBalanceUrlPath = `${environment.urlmain}Balance/`;
  private GamesUrl = `${environment.urlgame}`;


  constructor(private router: Router, private http: HttpClient) { }

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
    return this.http.post<any>(this.GamesUrl + "RpsWin?userId=" + localStorage.getItem('userId'), {});
  }

  RpsLose(): Observable<any>{
    console.log("RPS lose...");
    return this.http.post(this.GamesUrl + "RpsLose?userId=" + localStorage.getItem('userId'), {});
  }

  RpsRecord(): Observable<any> {
    console.log("RPS record...");
    return this.http.get(this.GamesUrl + "RpsRecord/" + localStorage.getItem('userId'), {responseType: 'text'});
  }

  WtpWin(){
    console.log("WTP win...");
    return this.http.post<any>(this.GamesUrl + "WtpWin?userId=" + localStorage.getItem('userId'), {});
  }

  WtpLose(): Observable<any> {
    console.log("WTP lose...");
    return this.http.post(this.GamesUrl + "WtpLose?userId=" + localStorage.getItem('userId'), {});
  }
  
  WtpRecord(): Observable<any> {
    console.log("WTP record...");
    return this.http.get(this.GamesUrl + "WtpRecord/" + localStorage.getItem('userId'), {responseType: 'text'});
  }

  CapWin(): Observable<any> {
    return this.http.post(this.GamesUrl + "CapWin?userId=" + localStorage.getItem('userId'), {});
  }

  CapLose(): Observable<any> {
    return this.http.post(this.GamesUrl + "CapLose?userId=" + localStorage.getItem('userId'), {});
  }
  
  CapRecord(): Observable<any> {
    return this.http.get(this.GamesUrl + "CapRecord/" + localStorage.getItem('userId'), {responseType: 'text'});
  }
}

export interface WtpGame
{
  pictureUrl: string,
  correctPokemon: string,
  options: string[],
}
