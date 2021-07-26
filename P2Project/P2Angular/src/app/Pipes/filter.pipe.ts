import { Pipe, PipeTransform } from '@angular/core';
import { IPost } from '../Models/IPost';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: Array<IPost>, filterString: string) {
    if (value.length === 0 || filterString === '' || filterString === undefined) {
      return value
    }

    const posts: Array<IPost> = [];

    for (const post of value) {
      if (post.PokemonName === filterString) {
        posts.push(post);
      }
    }

    return posts;

  }

}
