import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../service/authentication/authentication.service';

@Component({
  selector: 'app-top-nav-bar',
  templateUrl: './top-nav-bar.component.html',
  styleUrls: ['./top-nav-bar.component.css']
})
export class TopNavBarComponent implements OnInit {

  LoginStatus$!: Observable<boolean>;
  isLogin!: boolean;

  constructor(public authenticatationService: AuthenticationService) { }

  ngOnInit(): void {

    this.LoginStatus$ = this.authenticatationService.IsLoggedIn;
    this.LoginStatus$.subscribe(x => { this.isLogin = x })

  }

  Logout() {
    this.isLogin = false
    this.authenticatationService.Logout();
  }

}
