import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { GameService } from '../game-service';

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  styleUrls: ['./game-form.component.css']
})
export class GameFormComponent implements OnInit {

  // const[values: any, setValues: any] = useState()

  isFormValid: boolean = false;
  isCredentialsValid: boolean = false;
  returnUrl!: string;
  msg: string = ''
  imageSelected!: File;
  imageUrl: any = '';
  isIamge = false;

  constructor(private _gameService: GameService) { }

  ngOnInit(): void {
  }

  OnSubmit(newGame: NgForm) {

    const fd = new FormData();
    fd.append('ImageFile', this.imageSelected);
    fd.append('ImageName', this.imageSelected.name);
    fd.append('Title', newGame.value.title);
    fd.append('Description', newGame.value.textDescription);
    fd.append('Route', newGame.value.route);

    console.log(newGame.value)
    this._gameService.CreateGame(fd).subscribe(
      result => { console.log(result) },
      error => { console.log(error) }
    );

  }

  onFileSelected(event: any) {
    if (event.target.files && event.target.files[0]) {
      this.imageSelected = event.target.files[0];

      var imageType = event.target.files[0].type;

      //Check if its any image file
      if (imageType.match(/image\/*/) == null) {
        this.msg = 'Only images are supported'
        this.isIamge = true;
        this.imageUrl = ''
        return;
      }

      const reader = new FileReader();
      reader.readAsDataURL(this.imageSelected)
      reader.onload = x => {
        this.imageUrl = x.target!.result;
        this.msg = '';
        this.isIamge = false;
      }
    }
  }

}

