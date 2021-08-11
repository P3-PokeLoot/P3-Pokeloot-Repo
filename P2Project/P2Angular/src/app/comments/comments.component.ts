import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { DisplayServiceService } from '../display-service.service';
import { IPost } from '../Models/IPost';
import { IComment } from '../Models/IComment';


@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})

export class CommentsComponent implements OnInit {
  // @Input() postId?: number;
  private userId = localStorage.getItem('userId') as string;
  private postId : number;
  public fullPost : IPost;
  public commentInput: string;
  public commentList: IComment[];


  constructor(private _displayService: DisplayServiceService, private route: ActivatedRoute) { 
    this.postId = 0;
    this.route.params.subscribe( params => this.postId = params.postId );
    this.commentInput = "";
    this.fullPost = {} as IPost;
    this.commentList = [];

  }

  ngOnInit(): void {
    console.log("user id: " + this.userId);
    console.log("post id: " + this.postId);

    this._displayService.FullPost(this.postId).subscribe(
      result => {
        let PostId = result.postId;
        let PokemonId = result.pokemonId;
        let PostTime = result.postTime;
        let PostDescription = result.postDescription;
        let Price = result.price;
        let StillAvailable = result.stillAvailable;
        let IsShiny = result.isShiny;
        let UserId = result.userId;
        let type = result.postType;
        let UserName = result.userName;
        let SpriteLink = result.spriteLink;
        let PostType = '';
        if (type == 1) {
          PostType = 'Discussion';
        }
        else if (type == 2) {
          PostType = 'Sale';
        }
        else {
          PostType = 'Display';
        }
        let PokemonName = result.pokemonName;
        let RarityId = result.rarityId;

        this.fullPost = { PostId, PokemonId, PostTime, PostDescription, Price, StillAvailable, IsShiny, UserId, UserName, SpriteLink, PostType, PokemonName, RarityId }
        console.log(this.fullPost);
      }
    )
    this.BuildComments();
    
  }

  Submit() {
    this.commentList = [];
    if(this.commentInput != "")
    {
      this._displayService.NewComment(this.userId, this.postId, this.commentInput).subscribe()

      console.log(this.commentInput);
      this.commentInput = "";
    }
    let timer = setInterval(() => {
      this.BuildComments();
      clearInterval(timer);
    },500);
    
  }

  BuildComments() {
    this._displayService.PostComments(this.postId).subscribe(
      result => {
        result.forEach(element => {

          let CommentId = element.Key.CommentId
          let CommentPostId = element.Key.CommentPostId
          let CommentUserId = element.Key.CommentUserId
          
          let Timestamp = element.Key.CommentTimestamp
          let Content = element.Key.CommentContent

          let UserName = element.Value
          
          let comment = {CommentId, CommentPostId, CommentUserId, Timestamp, Content, UserName}
          this.commentList.push(comment);
        });
      }
    )
  }
  GetCardColor(rarityId: any): string {
    switch (rarityId) {
      case 1:
        return 'card1'
        break;
      case 2:
        return 'card2'
        break;
      case 3:
        return 'card3'
        break;
      case 4:
        return 'card4'
        break;
      case 5:
        return 'card5'
        break;
      case 6:
        return 'card6'
        break
      default:
        return ''
        break;
    }
  }

}
