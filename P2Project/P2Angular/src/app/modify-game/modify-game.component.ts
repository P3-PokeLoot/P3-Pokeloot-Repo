import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GameService } from '../service/game/game-service';

@Component({
  selector: 'app-modify-game',
  templateUrl: './modify-game.component.html',
  styleUrls: ['./modify-game.component.css']
})
export class ModifyGameComponent implements OnInit {

  // Select Fields
  public gameList!: Array<any>;
  public error = false;
  public isChoosen = false;
  public displayParent: any;
  public displayParentId!: number;



  constructor(private router: Router, private _gameService: GameService) { }

  ngOnInit(): void {
    this._gameService.GetList().subscribe(
      result => {
        this.gameList = result;
      },
      error => { console.log(error) }
    )
  }



  onOptionChange(gameChoosen: any) {
    if (gameChoosen.target.value != "") {
      this.displayParent = this.gameList.find(x => x.title === gameChoosen.target.value)
      this.displayParentId = this.displayParent.id
      this.isChoosen = true;

    } else {
      this.isChoosen = false;
    }
  }



}
