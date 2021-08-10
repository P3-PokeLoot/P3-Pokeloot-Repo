import { Component, OnInit } from '@angular/core';
import { LeaderboardStatsService } from '../service/leaderboards/leaderboard-stats.service';
import { PercentageOwnCardModel_Shiny } from '../Models/IPercentageOwnCard_ShinyLB';

@Component({
  selector: 'app-lb-child-who-has',
  templateUrl: './lb-child-who-has.component.html',
  styleUrls: ['./lb-child-who-has.component.css']
})
export class LbChildWhoHasComponent implements OnInit {

  constructor( private _leaderboardService:LeaderboardStatsService) {}

  chosenPokemon:PercentageOwnCardModel_Shiny[] =[];
  ngOnInit(): void {
  }

  //This method takes the observable data and puts it into a class property
  GetPercentOwnCard(pokemonName:string){
    this._leaderboardService.GetCardPercentage(pokemonName).subscribe(
      result => {
        this.chosenPokemon=result;
      }
    );
  }

}
