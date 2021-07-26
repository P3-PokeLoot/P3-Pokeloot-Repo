import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError, retry } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})

export class CardServiceService {
  


  // allen added

  BuyLootbox(UserId:number, price:number):Observable<boolean>{
    return this.http.get<boolean>('https://pokeloot.azurewebsites.net/api/P2/buy/' + UserId + '/' + price)
  }
  // end


  private baseUrlLogin: string = 'https://pokeloot.azurewebsites.net/api/P2/UserCollection/'

  private raritiesUrlPath: string = 'https://pokeloot.azurewebsites.net/api/P2/RarityTypes' 

  private rootUrl: string = 'https://pokeloot.azurewebsites.net'
  



  //constructor(private http: HttpClient) { }
  constructor(private router: Router, private http: HttpClient) { }

  GetCardsList(userId : string):Observable<any[]>{
    return this.http.get<any>(this.baseUrlLogin + userId)
  }

  GetRarityList():Observable<any[]>{
    return this.http.get<any>(this.raritiesUrlPath)
  }

}
