import { PokemonCards } from "./pokemon-cards";
import { User } from "./User";

export interface CardCollectModel_Shiny{
    PokemonId:number,
    UserId:number,
    QuantityNormal:number,
    QuantityShiny:number,
    Pokemon:PokemonCards,
    User:User
}