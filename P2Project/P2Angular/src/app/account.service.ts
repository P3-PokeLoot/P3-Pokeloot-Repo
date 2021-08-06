import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { User } from './profile-page/IUser';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class AccountService {

  constructor(private http:HttpClient) { }

  GetUserProfile():Observable<User>{
    return this.http.get<User>(`${environment.urlmain}Profile/${localStorage.getItem('userId')}`)
  }

}
