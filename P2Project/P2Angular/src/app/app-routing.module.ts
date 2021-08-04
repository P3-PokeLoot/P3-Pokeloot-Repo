import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
import { CardCollectComponent } from './cardcollect/cardcollect.component';
import { AuthGuard } from './guards/auth.guard';
import { RpsGameComponent } from './rps-game/rps-game.component';
import { RpsGamePokemonSelectionComponent } from './rps-game-pokemon-selection/rps-game-pokemon-selection.component';
import { WtpGameComponent } from './wtp-game/wtp-game.component';
import { PokemonCardMatchComponent } from './pokemon-card-match/pokemon-card-match.component';
import { EditPostComponent } from './edit-post/edit-post.component';
import { FriendsComponent } from './friends/friends.component';
import { WamGameComponent } from './wam-game/wam-game.component';



const routes: Routes = [
  { path: '', redirectTo: 'Home', pathMatch: 'full' },
  { path: 'Login', component: LoginPageComponent },
  { path: 'Signup', component: SignupPageComponent },
  { path: 'Home', component: HomePageComponent, canActivate: [AuthGuard] },
  { path: 'Profile', component: ProfilePageComponent, canActivate: [AuthGuard] },
  { path: 'Game', component: GamePageComponent, canActivate: [AuthGuard] },
  { path: 'PokeRPS', component: RpsGamePokemonSelectionComponent, canActivate: [AuthGuard] },
  { path: 'Collection', component: CardCollectComponent, canActivate: [AuthGuard] },
  { path: 'TradeCard', component: TradeCardPageComponent, canActivate: [AuthGuard] },
  { path: 'UnlockCard', component: UnlockCardPageComponent, canActivate: [AuthGuard] },
  { path: 'EditPost', component: EditPostComponent, canActivate: [AuthGuard] },
  { path: 'ViewInformation', component: ViewInformationPageComponent, canActivate: [AuthGuard] },
  { path: 'ViewBalance', component: ViewBalancePageComponent, canActivate: [AuthGuard] },
  { path: 'PostForm', component: CreatePostComponent, canActivate: [AuthGuard] },
  { path: 'Game/RPS', component: RpsGameComponent, canActivate: [AuthGuard] },
  { path: 'Friends', component: FriendsComponent, canActivate: [AuthGuard] },
  { path: 'Game/WhosThatPokemon', component: WtpGameComponent, canActivate: [AuthGuard] },
  { path: 'Game/WhackADiglett', component: WamGameComponent, canActivate: [AuthGuard] },
  { path: 'Game/PokemonCardMatch', component: PokemonCardMatchComponent, canActivate: [AuthGuard] },
  { path: '**', component: HomePageComponent },
];


@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation : 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
