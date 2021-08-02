import { ActivatedRoute, Router } from '@angular/router';
import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { FullPost } from '../Models/Post';
import { IPost } from '../Models/IPost';

@Component({
  selector: 'app-edit-post',
  templateUrl: 'edit-post.component.html',
  styleUrls: ['./edit-post.component.css']
})
export class EditPostComponent implements OnInit {

  
  @Input() currentPost!: IPost;
  submittedSuccesfully = false;
  isPriceValid?: boolean;
  constructor() { }

  ngOnInit(): void {
    console.log("im at editing page");
    
  }

  OnSubmit(postForm: NgForm) {
    this.isPriceValid = false;

    if (!postForm.controls.Price.valid) {
      this.isPriceValid = true;
      return;
    }
  }

}
