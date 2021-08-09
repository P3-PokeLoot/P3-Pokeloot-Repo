import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})

export class CardServiceService {
  


  // allen added

  BuyLootbox(UserId:number, price:number):Observable<boolean>{
    return this.http.get<boolean>(`${environment.urlmain}buy/${UserId}/${price}`)
  }
  // end


  private baseUrlLogin = `${environment.urlmain}UserCollection/`

  private raritiesUrlPath = `${environment.urlmain}RarityTypes`

  private favoriteUrlPath = `${environment.urlmain}Favorite/`

  // Commented out by Greg because it looked like it wasn't being used and it will cause problems if it suddenly starts getting used.
  // Can be deleted if you know for sure what its purpose was supposed to be and that it isn't being used.
  //private rootUrl: string = 'https://pokeloot.azurewebsites.net'
  



  //constructor(private http: HttpClient) { }
  constructor(private router: Router, private http: HttpClient) { }

  GetCardsList(userId : string):Observable<any[]>{
    return this.http.get<any>(this.baseUrlLogin + userId)
  }

  GetRarityList():Observable<any[]>{
    return this.http.get<any>(this.raritiesUrlPath)
  }
 
  Favorite(userId: string, pokemonid: number):Observable<any>{
    return this.http.get<any>(this.favoriteUrlPath + userId + '/' + pokemonid)
  }

}
