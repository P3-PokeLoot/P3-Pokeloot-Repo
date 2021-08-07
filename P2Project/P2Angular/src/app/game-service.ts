import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler } from "@angular/common/http";
import { Observable, of, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError, retry } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})

export class GameService {


  private gameUrlPath: string = 'https://pokeloot.azurewebsites.net/api/P2/EarnCoins/';
  private userBalanceUrlPath: string = 'https://pokeloot.azurewebsites.net/api/P2/Balance/';
  private gameListUrl: string = 'https://localhost:44301/api/Games/List';
  private createGameUrl: string = 'https://localhost:44301/api/Games/CreateGame'



  constructor(private router: Router, private http: HttpClient) { }

  AddCoins(amountCoins: number): Observable<any[]> {
    return this.http.get<any>(this.gameUrlPath + localStorage.getItem('userId') + '/' + amountCoins)
  }

  GetBalance(): Observable<any[]> {
    return this.http.get<any>(this.userBalanceUrlPath + localStorage.getItem('userId'))
  }

  // Grabs the list of games desciption that are avaiable to be displayed in the game page.
  GetList(): Observable<any[]> {
    return this.http.get<any>(this.gameListUrl);
  }

  CreateGame(gameForm: any) {
    return this.http.post<any>(this.createGameUrl, gameForm);
  }
}
