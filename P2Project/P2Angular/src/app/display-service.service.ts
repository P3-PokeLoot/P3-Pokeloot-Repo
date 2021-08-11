import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { IComment } from './Models/IComment';

@Injectable({
  providedIn: 'root'
})
export class DisplayServiceService {

  
  

  private url = `${environment.urlmain}DisplayBoard`;
  //private url: string = '';

  private localurl = `${environment.urlmainlocalonly}EditPrice/`;
  private urltobuy = `${environment.urlmain}buyCard/`;
  private fullPosturl = `${environment.urlmain}FullPostById/`;
  private postCommentsurl = `${environment.urlmain}Comments/`;
  private newCommenturl = `${environment.urlmain}PostComment/`;




  constructor( private router:Router, private http:HttpClient) { }

  DisplayBoard():Observable<any[]>{
    return this.http.get<any>(this.url);
  }

  getBuyCard(postId: number, userId: string):Observable<any>{
      let newUrl = this.urltobuy + postId + '/' + userId + '/';
      return this.http.get<any>(newUrl);
  }

  editPost(postId: number, newPrice: string):Observable<boolean>{
    let newUrl = this.localurl + postId + '/' + newPrice + '/';
    return this.http.get<any>(newUrl);
  }

  // Gets all the info needed to build a full post for the Post Comments component
  FullPost(postId : number):Observable<any>{
    console.log("PostId Path:" +  this.fullPosturl + postId);
    return this.http.get<any>(this.fullPosturl + postId);
  }

  // Gets all the comments for a Post by Id for the Post Comments component
  PostComments(postId : number):Observable<any[]>{
    return this.http.get<any>(this.postCommentsurl + postId);
  }

  // Generates a new comment object for the post and user being userd for the Post Comments component
  NewComment(userId : string, postId : number, content : string):Observable<any>{
    let status = this.http.get<any>(this.newCommenturl + userId + '/' + postId + '/' + encodeURI(content));
    return status;
  }



}
