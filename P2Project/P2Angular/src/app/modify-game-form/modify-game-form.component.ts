import { ThrowStmt } from '@angular/compiler';
import { Component, Input, OnChanges, OnInit, SimpleChange, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GameService } from '../service/game/game-service';
import { IGame } from '../Models/IGame';

@Component({
  selector: 'app-modify-game-form',
  templateUrl: './modify-game-form.component.html',
  styleUrls: ['./modify-game-form.component.css']
})
export class ModifyGameFormComponent implements OnInit, OnChanges {
  // Form fileds
  @Input() displayChild!: number;
  private gametemp!: IGame;
  public isFormValid: boolean = false;
  public isCredentialsValid: boolean = false;
  public returnUrl!: string;
  public imageSelected!: File;
  public imageUrl: any = '';
  public isImage = false;
  public showImage = false;

  gameForm!: FormGroup;
  constructor(private router: Router, private _gameService: GameService) { }

  ngOnInit(): void {
    this.gameForm = new FormGroup({
      imageFile: new FormControl(),
      imageName: new FormControl(),
      title: new FormControl("", [Validators.required, Validators.minLength(3)]),
      description: new FormControl("", [Validators.required, Validators.minLength(10)]),
      route: new FormControl("", [Validators.required, Validators.minLength(3)]),
    })
    this._gameService.SingleGame(this.displayChild).subscribe(
      result => {
        this.editGame(result)
      },
      error => { alert("An error occured trying to retrieve game") })

  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes)
    if (changes["displayChild"].previousValue) {
      this._gameService.SingleGame(changes["displayChild"].currentValue).subscribe(
        result => {
          this.editGame(result)
        },
        error => { alert("An error occured trying to retrieve game") })
    }
  }


  get f() {
    return this.gameForm.controls;
  }

  editGame(game: any) {
    this.gametemp = {
      id: game.id,
      title: game.title,
      description: game.description,
      imageName: game.imageName,
      route: game.route,
      imageFile: game.imageFile
    };

    if (game.imageSource !== "" || game.imageSource !== null) {
      this.imageUrl = game.imageSource
      this.showImage = true;
    }

    this.gameForm.patchValue({
      title: this.gametemp.title,
    })
    this.gameForm.patchValue({
      route: this.gametemp.route
    })
    this.gameForm.patchValue({
      description: this.gametemp.description,
    })
    this.gameForm.patchValue({
      imageName: this.gametemp.imageName
    })
    this.gameForm.patchValue({
      imageFile: this.gametemp.imageFile,
    })
  }

  onFileSelected(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      var imageType = event.target.files[0].type;

      //Check if its any image file
      if (imageType.match(/image\/*/) == null) {
        this.isImage = true;
        this.imageUrl = ''
        return;
      }

      const reader = new FileReader();
      reader.readAsDataURL(file)
      reader.onload = x => {
        this.imageUrl = x.target!.result;
        this.showImage = true;
      }

      console.log(`File: ${file}`)
      this.gameForm.patchValue({
        imageFile: file,
      })
    }
  }

  OnSubmit() {
    //Checks if new file image has beem submitted
    let imageName = this.gameForm.value.imageFile === null ? this.gametemp.imageName : this.gameForm.value.imageFile.name

    const fd = new FormData();

    let id = String(this.displayChild)
    fd.append('Id', id)
    fd.append('ImageFile', this.gameForm.value.imageFile);
    fd.append('ImageName', imageName);
    fd.append('Title', this.gameForm.value.title);
    fd.append('Description', this.gameForm.value.description);
    fd.append('Route', this.gameForm.value.route);
    fd.append('OldImageName', this.gametemp.imageName)

    this._gameService.ModifyGame(fd).subscribe(
      result => {
        if (result != null) {
          alert(`Successfuly Modified ${result.title} Game`)
          this.router.navigate(["Game"])
        }
      },
      error => {
        alert("Error - Could Not Modify Game")
      }
    );
  }

}
