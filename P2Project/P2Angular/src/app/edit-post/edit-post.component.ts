import { ActivatedRoute, Router } from '@angular/router';
import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { FullPost } from '../Models/Post';
import { IPost } from '../Models/IPost';
import { DisplayServiceService } from '../display-service.service';

@Component({
  selector: 'app-edit-post',
  templateUrl: 'edit-post.component.html',
  styleUrls: ['./edit-post.component.css']
})
export class EditPostComponent implements OnInit {

  
  @Input() currentPost!: IPost;
  submittedSuccesfully = false;
  isPriceValid?: boolean;

  constructor(private _displayService: DisplayServiceService ,private route:Router) { 

  }

  ngOnInit(): void {
    console.log("im at editing page");
    
  }

  OnSubmit(postForm: NgForm) {
    this.isPriceValid = false; 

    if (!postForm.controls.Price.valid) {//checks if price is valid
      this.isPriceValid = true;
      return;
    }

    this.route.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.route.navigate(['/Home']);
});

    this._displayService.editPost(this.currentPost.PostId, postForm.value.Price).subscribe(
      result => {
        console.log(result);
        //let currentUrl = this.route.url;
        this.refresh();
      },
      error => console.log(error)
    );
  }

  refresh(){//refreshes home page to reflect change
    this.route.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.route.navigate(['/Home']);
    });
    this.route.navigate(['/Home']);
    this.route.navigateByUrl('/Home');
  }

}
