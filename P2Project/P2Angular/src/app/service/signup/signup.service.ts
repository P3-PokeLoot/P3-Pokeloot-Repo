import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/User';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SignupService {

  private baseUrlSignup = `${environment.urlmain}Signup`


  constructor(private route: Router, private http: HttpClient) { }

  CreateUser(newUser: User) {
    return this.http.post<any>(this.baseUrlSignup, newUser);
  }
}
