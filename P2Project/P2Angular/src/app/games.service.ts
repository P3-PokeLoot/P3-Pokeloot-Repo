import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GamesService {

  constructor(private http: HttpClient) { }

  private BaseUrl = "localhost";
  private GamesUrl = "api/game/"; //Not sure how this is going to work once kubernetes-itized
  private WtpUrl = `${environment.urlgame}Wtp`;
  
  GetGamesList(): Observable<GameInfo[]>{
    return this.http.get<GameInfo[]>(this.BaseUrl + this.GamesUrl + "list");
  }

  

}

//Just an idea so far - Subject to change upon adding into the database
export interface GameInfo
{
  description: string,
  imgUrl: string,
  route: string
};

