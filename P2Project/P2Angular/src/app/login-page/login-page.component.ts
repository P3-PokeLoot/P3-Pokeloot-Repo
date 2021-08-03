import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../service/authentication/authentication.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})


export class LoginPageComponent implements OnInit {

  isFormValid: boolean = false;
  isCredentialsValid: boolean = false;
  returnUrl!: string;

  constructor(
    private authenticateService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || ''
  }

  OnSubmit(loginInForm: NgForm) {

    if (!loginInForm.valid) {
      this.isFormValid = true;
      this.isCredentialsValid = false;
      return;
    }


    this.authenticateService.AuthenticateWithApi(loginInForm.value.username, loginInForm.value.password).subscribe(
      result => {

        if (result == null) {
          this.isFormValid = false;
          this.isCredentialsValid = true;
        } else {
          this.isFormValid = false;
          this.isCredentialsValid = false;
          this.router.navigateByUrl(this.returnUrl)

        }

      },
      error => { `Error Occured ${console.log(error)}` }
    )
  }

}
