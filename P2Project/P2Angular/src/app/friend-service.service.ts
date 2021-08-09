import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FriendServiceService {

  private FriendUrlPath = `${environment.urlmain}Friends/`
  private FriendActionUrlPath = `${environment.urlmain}FriendAction/`

  constructor(private router: Router, private http: HttpClient) { }

  GetFriendsList(userId : string):Observable<any[]>{
    return this.http.get<any>(this.FriendUrlPath + userId)
  }

  FriendAction(userId : string, friendId : string):Observable<any[]>{
    return this.http.get<any>(this.FriendActionUrlPath + userId + '/' + friendId)
}
 FriendActionWithANumber(userId : string, friendId : number):Observable<any[]>{
    return this.http.get<any>(this.FriendActionUrlPath + userId + '/' + friendId)
}
}
