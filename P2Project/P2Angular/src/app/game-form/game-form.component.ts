import { Component, OnInit } from '@angular/core';
import { FormGroup, NgForm, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GameService } from '../service/game/game-service';

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  styleUrls: ['./game-form.component.css']
})
export class GameFormComponent implements OnInit {

  gameForm: FormGroup = new FormGroup({
    imageFile: new FormControl('', [Validators.required]),
    imageName: new FormControl('', [Validators.required]),
    title: new FormControl('', [Validators.required, Validators.minLength(3)]),
    description: new FormControl('', [Validators.required, Validators.minLength(10)]),
    route: new FormControl('', [Validators.required, Validators.minLength(3)]),
  });

  isFormValid: boolean = false;
  isCredentialsValid: boolean = false;
  returnUrl!: string;
  msg: string = ''
  imageSelected!: File;
  imageUrl: any = '';
  isImage = false;
  showImage = false;

  constructor(private router: Router, private _gameService: GameService) { }

  ngOnInit(): void {
  }

  get f() {
    return this.gameForm.controls;
  }

  OnSubmit() {
    const fd = new FormData();
    fd.append('ImageFile', this.gameForm.value.imageFile);
    fd.append('ImageName', this.gameForm.value.imageName);
    fd.append('Title', this.gameForm.value.title);
    fd.append('Description', this.gameForm.value.description);
    fd.append('Route', this.gameForm.value.route);

    this._gameService.CreateGame(fd).subscribe(
      result => {
        if (result != null) {
          alert(`Successfuly Created ${result.title} Game`)
          this.router.navigate(["Game"])
        }
      },
      error => {
        alert("Error could not create game")
      }
    );
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
        this.msg = '';
        this.showImage = true;
      }

      this.gameForm.patchValue({
        imageFile: file,
        imageName: file.name
      })
    }
  }
}

