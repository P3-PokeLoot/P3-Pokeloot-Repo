import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {

  private baseUrlLogin: string = "https://pokeloot.azurewebsites.net/api/P2/Login/"
  private loginStatus = new BehaviorSubject<boolean>(this.CheckLoginStatus());
  private userId = new BehaviorSubject<string>(localStorage.getItem('userId') as string);

  constructor(private router: Router, private http: HttpClient) { }



  AuthenticateWithApi(username: string, password: string): Observable<any> {

    return this.http.get<any>(this.baseUrlLogin + `${username}/${password}`).pipe(
      map(result => {

        if (result != null) {
          this.loginStatus.next(true);
          localStorage.setItem('loginStatus', "1");
          localStorage.setItem('userId', result.userId)
          localStorage.setItem('user', JSON.stringify(result))
        }

        return result
      }));
  }

  Logout() {
    localStorage.setItem('loginStatus', "0");
    // this.loginStatus.next(true);
    localStorage.removeItem('userId')
    this.router.navigate(['Login']);
  }

  CheckLoginStatus(): boolean {
    if (localStorage.getItem('loginStatus') === '1') {
      return true;
    }
    console.log('We return false')
    return false;
  }

  get CurrentUserId() {
    return this.userId.asObservable()
  }

  get IsLoggedIn() {
    return this.loginStatus.asObservable()
  }

}
