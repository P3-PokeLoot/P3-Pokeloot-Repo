import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { GameService } from '../service/game/game-service';

@Component({
  selector: 'app-delete-game',
  templateUrl: './delete-game.component.html',
  styleUrls: ['./delete-game.component.css']
})
export class DeleteGameComponent implements OnInit {
  public gameList!: Array<any>;
  public display: any;
  public isChoosen = false;
  public title = ""
  public description = ""
  public imageSrc = ""
  public error = false;

  constructor(private router: Router, private _gameService: GameService) { }

  ngOnInit(): void {
    this._gameService.GetList().subscribe(
      result => { this.gameList = result; },
      error => { console.log(error) }
    )
  }

  OnSubmit() {
    this._gameService.DeleteGame(this.display.id).subscribe(
      result => {
        alert(`Game ${result.title} was deleted successfully`)
        this.router.navigate(["/Game"])
      },
      error => { console.log(`Error ${error}`) }
    );

  }

  onOptionChange(gameChoosen: any) {
    if (gameChoosen.target.value != "") {
      this.isChoosen = true;
      this.display = this.gameList.find(x => x.title === gameChoosen.target.value)
    } else {
      this.isChoosen = false;
    }
  }

}
