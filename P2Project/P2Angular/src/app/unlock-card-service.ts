import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})

export class UnlockCardService {
  


  constructor(private http: HttpClient) { }

  RollLootbox(boxType: number):Observable<any[]>{
    return this.http.get<any>(`${environment.urlmainlocalonly}Lootbox/` + localStorage.getItem('userId') + '/' + boxType)
  }

  Balance():Observable<any[]>{
    return this.http.get<any>(`${environment.urlmain}Balance/${localStorage.getItem('userId')}`)
  }

}
