import { Component, OnInit } from '@angular/core';
import { FormControlName, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SignupService } from '../service/signup/signup.service';
import { User } from '../Models/User';
import { FormBuilder, FormGroup, FormControl, Validator } from '@angular/forms'

@Component({
  selector: 'app-signup-page',
  templateUrl: './signup-page.component.html',
  styleUrls: ['./signup-page.component.css']
})
export class SignupPageComponent implements OnInit {

  returnUrl!: string;
  isFormValid: boolean = false;
  isCreatedAccount: boolean = false;

  newUser: User = {
    UserName: "",
    Password: "",
    FirstName: "",
    LastName: "",
    Email: "",
    CoinBalance: 0,
    AccountLevel: 0,
    TotalCoinsEarned: 0,
    CardCollections: [],
    DisplayBoards: [],
  }


  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private signup: SignupService,) { }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || ''
  }

  OnSubmit(singupForm: NgForm) {

    if (!singupForm.valid) {
      this.isFormValid = true;
      this.isCreatedAccount = false;
      return;
    }


    this.newUser.UserName = singupForm.value.username
    this.newUser.Password = singupForm.value.password
    this.newUser.FirstName = singupForm.value.Fname
    this.newUser.LastName = singupForm.value.Lname
    this.newUser.Email = singupForm.value.Email

    // Check to see if user was created
    this.signup.CreateUser(this.newUser).subscribe(
      result => {

        this.isCreatedAccount = false;
        this.isFormValid = false;
        this.router.navigate(['Login'])

      },
      error => {
        this.isCreatedAccount = true;
        this.isFormValid = false;
        return;
      }
    );
  }

}
