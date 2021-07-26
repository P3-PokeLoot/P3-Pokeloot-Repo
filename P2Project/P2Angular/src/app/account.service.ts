import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { User } from './profile-page/IUser';

@Injectable({
  providedIn: 'root'
})

export class AccountService {

  constructor(private http:HttpClient) { }

  GetUserProfile():Observable<User>{
    return this.http.get<User>('https://pokeloot.azurewebsites.net/api/P2/Profile/'+localStorage.getItem('userId'))
  }

}
