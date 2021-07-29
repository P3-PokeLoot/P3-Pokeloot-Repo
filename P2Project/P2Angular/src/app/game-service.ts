import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, of, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError, retry } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})

export class GameService {
  private GameList = [
    { image: "assets/img/Poke_Ball.png", title: "Rock Paper Scissors", description: "Some quick example text to build on the card title and make up the bulk of the card\'s content." },
    { image: "assets/img/Poke_Ball.png", title: "Clicker Game", description: "Some quick example text to build on the card title and make up the bulk of the card\'s content." },
    { image: "assets/img/Poke_Ball.png", title: "Coming soon Game", description: "Some quick example text to build on the card title and make up the bulk of the card\'s content." },
    { image: "assets/img/Poke_Ball.png", title: "Coming soon Game", description: "Some quick example text to build on the card title and make up the bulk of the card\'s content." },
    { image: "assets/img/Poke_Ball.png", title: "Coming soon Game", description: "Some quick example text to build on the card title and make up the bulk of the card\'s content." },
    { image: "assets/img/Poke_Ball.png", title: "Coming soon Game", description: "Some quick example text to build on the card title and make up the bulk of the card\'s content." },
    { image: "assets/img/Poke_Ball.png", title: "Coming soon Game", description: "Some quick example text to build on the card title and make up the bulk of the card\'s content." },
    { image: "assets/img/Poke_Ball.png", title: "Coming soon Game", description: "Some quick example text to build on the card title and make up the bulk of the card\'s content." },
    { image: "assets/img/Poke_Ball.png", title: "Coming soon Game", description: "Some quick example text to build on the card title and make up the bulk of the card\'s content." },

  ];



  private gameUrlPath: string = 'https://pokeloot.azurewebsites.net/api/P2/EarnCoins/';
  private userBalanceUrlPath: string = 'https://pokeloot.azurewebsites.net/api/P2/Balance/';



  constructor(private router: Router, private http: HttpClient) { }

  AddCoins(amountCoins: number): Observable<any[]> {
    return this.http.get<any>(this.gameUrlPath + localStorage.getItem('userId') + '/' + amountCoins)
  }

  GetBalance(): Observable<any[]> {
    return this.http.get<any>(this.userBalanceUrlPath + localStorage.getItem('userId'))
  }

  GetList(): Observable<any[]> {
    return of(this.GameList);
  }
}
