import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

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

  FullPost(postId : number):Observable<any>{
    console.log("PostId Path:" +  this.fullPosturl + postId);
    return this.http.get<any>(this.fullPosturl + postId);
  }

  PostComments(postId : number):Observable<any[]>{
    return this.http.get<any>(this.postCommentsurl + postId);
  }

  NewComment(userId : string, postId : number, content : string):void{
    console.log(this.newCommenturl + userId + '/' + postId + '/' + content);

    this.http.get<any>(this.newCommenturl + userId + '/' + postId + '/' + content);
  }



}
