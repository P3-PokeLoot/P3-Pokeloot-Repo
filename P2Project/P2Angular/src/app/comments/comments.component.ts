import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {
  // @Input() postId?: number;
  private userId = localStorage.getItem('userId') as string;
  private postId? : number;
  filterString: string = '';



  constructor(private route: ActivatedRoute) { 
    this.route.params.subscribe( params => this.postId = params.postId );
  }

  ngOnInit(): void {
    console.log("well you made it");
    console.log("user id: " + this.userId);
    console.log("post id: " + this.postId);
    
  }

  Submit() {
    console.log("test")
  }

}
