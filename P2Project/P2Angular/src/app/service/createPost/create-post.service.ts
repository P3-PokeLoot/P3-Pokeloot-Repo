import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { IPost } from 'src/app/Models/IPost';
import { FullPost } from 'src/app/Models/Post';

@Injectable({
  providedIn: 'root'
})
export class CreatePostService {


  private url: string = "https://pokeloot.azurewebsites.net/api/P2/Post"

  constructor(private router: Router, private http: HttpClient) { }

  CreatePost(newPost: FullPost) {
    var post = JSON.stringify(newPost);
    return this.http.get<any>(this.url + `/${newPost.pokemonId}` + `/${newPost.price}` + `/${newPost.isShiny}` + `/${newPost.userId}` + `/${newPost.postDescription}`).pipe(
      map(result => {
        if (result) {
          this.router.navigate(['Home'])
        } else {
          return result
        }
      })
    )

  }
}
