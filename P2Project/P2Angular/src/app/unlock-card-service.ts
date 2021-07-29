import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError, retry } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})

export class UnlockCardService {
  


  constructor(private http: HttpClient) { }

  RollLootbox():Observable<any[]>{
    return this.http.get<any>('https://pokeloot.azurewebsites.net/api/P2/Lootbox/' + localStorage.getItem('userId'))
  }

  Balance():Observable<any[]>{
    return this.http.get<any>('https://pokeloot.azurewebsites.net/api/P2/Balance/' + localStorage.getItem('userId'))
  }

}
