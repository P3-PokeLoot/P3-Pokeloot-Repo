import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/User';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class SignupService {

  private baseUrlSignup: string = "https://pokeloot.azurewebsites.net/api/P2/Signup"


  constructor(private route: Router, private http: HttpClient) { }

  CreateUser(newUser: User) {
    return this.http.post<any>(this.baseUrlSignup, newUser);
  }
}
