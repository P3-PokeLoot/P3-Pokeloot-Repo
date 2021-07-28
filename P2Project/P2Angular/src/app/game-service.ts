import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError, retry } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})

export class GameService {
  



  private gameUrlPath: string = 'https://pokeloot.azurewebsites.net/api/P2/EarnCoins/';
  private userBalanceUrlPath: string = 'https://pokeloot.azurewebsites.net/api/P2/Balance/';

  

  constructor(private router: Router, private http: HttpClient) { }

  AddCoins(amountCoins : number):Observable<any[]>{
    return this.http.get<any>(this.gameUrlPath + localStorage.getItem('userId') + '/' + amountCoins)
  }

  GetBalance():Observable<any[]>{
    return this.http.get<any>(this.userBalanceUrlPath + localStorage.getItem('userId'))
  }
}
