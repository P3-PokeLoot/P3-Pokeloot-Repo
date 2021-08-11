import { Component, OnInit, Input } from '@angular/core';
import { LeaderboardStatsService } from '../service/leaderboards/leaderboard-stats.service';
import { PercentageOwnCardModel_Shiny } from '../Models/IPercentageOwnCard_ShinyLB';
import { IUnlockCard } from '../unlock-card-page/IUnlockCard';

@Component({
  selector: 'app-lb-child-who-has',
  templateUrl: './lb-child-who-has.component.html',
  styleUrls: ['./lb-child-who-has.component.css']
})
export class LbChildWhoHasComponent implements OnInit {

  @Input() newCard={} as IUnlockCard;

  constructor(private _leaderboardService:LeaderboardStatsService) {
  }

  // card:IUnlockCard = this.newcard;
  chosenPokemon:PercentageOwnCardModel_Shiny ={
    pokemonId:0,
    rarityId:0,
    spriteLink:'',
    spriteLinkShiny:'',
    pokemonName:'',
    totalQy:0,
    total_Users:0,
    percentage_OwnCard:0
  };


  ngOnInit(): void {
  }

  ngOnChanges():void{
    this.GetPercentOwnCard();
  }

  //This method takes the observable data and puts it into a class property
  GetPercentOwnCard(){
    this._leaderboardService.GetCardPercentage(this.newCard.PokemonName).subscribe(
      result => {
        this.chosenPokemon=result;
        console.log(this.chosenPokemon);
      }
    );
  }

}
