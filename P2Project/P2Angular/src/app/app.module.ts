// 

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { SignupPageComponent } from './signup-page/signup-page.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { GamePageComponent } from './game-page/game-page.component';
import { CreatePostComponent } from './create-post-page/create-post.component';
import { TradeCardPageComponent } from './trade-card-page/trade-card-page.component';
import { UnlockCardPageComponent } from './unlock-card-page/unlock-card-page.component';
import { ViewInformationPageComponent } from './view-information-page/view-information-page.component';
import { ViewBalancePageComponent } from './view-balance-page/view-balance-page.component';
import { AppRoutingModule } from './app-routing.module';
import { TopNavBarComponent } from './top-nav-bar/top-nav-bar.component';
import { PostsComponent } from './posts/posts.component';
import { CardCollectComponent } from './cardcollect/cardcollect.component';
import { CardServiceService } from './card-service.service';
import { AccountService } from './account.service';
import { AddCardComponent } from './add-card/add-card.component';
import { CommonModule } from '@angular/common';
import { FilterPipe } from './Pipes/filter.pipe';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    LoginPageComponent,
    SignupPageComponent,
    ProfilePageComponent,
    GamePageComponent,
    CreatePostComponent,
    TradeCardPageComponent,
    UnlockCardPageComponent,
    ViewInformationPageComponent,
    ViewBalancePageComponent,
    TopNavBarComponent,
    PostsComponent,
    CardCollectComponent, 
    AddCardComponent, FilterPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule
  ],
  providers: [CardServiceService,AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }