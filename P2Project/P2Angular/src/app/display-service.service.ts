import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class DisplayServiceService {

  
  

  private url: string = 'https://pokeloot.azurewebsites.net/api/P2/DisplayBoard';
  //private url: string = '';

  //private url: string = 'https://localhost:44307/api/P2/DisplayBoard';
  private urltobuy: string = 'https://pokeloot.azurewebsites.net/api/P2/buyCard/';


  constructor( private router:Router, private http:HttpClient) { }

  DisplayBoard():Observable<any[]>{
    return this.http.get<any>(this.url);
  }

  getBuyCard(postId: number, userId: string):Observable<any>{
      let newUrl = this.urltobuy + postId + '/' + userId + '/';
      return this.http.get<any>(newUrl);
  }
}
