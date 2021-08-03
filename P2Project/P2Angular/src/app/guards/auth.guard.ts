import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { take, map } from 'rxjs/operators';
import { AuthenticationService } from '../service/authentication/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authentication: AuthenticationService, private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return this.authentication.IsLoggedIn.pipe(take(1), map((loginStatus: boolean) => {
      const destination: string = state.url;

      // Check if user is not logged In
      if (!loginStatus) {
        this.router.navigate(['Login'], { queryParams: { returnUrl: state.url } })
        return false;
      }


      return loginStatus;
    }))
  }

}
