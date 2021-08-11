import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Message } from 'src/app/Models/message';
import { MessageUser } from 'src/app/Models/MessageUser';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  private url = `${environment.urlmain}`

  constructor(private http: HttpClient) { }

  SearchMessageUserByUsername(username: string): Observable<MessageUser> {
    return this.http.get<MessageUser>(`${this.url}GetMessageUserByUsername/${username}`);
  }

  GetMessages(senderId: number, receiverId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.url}GetMessages/${senderId}/${receiverId}`);
  }

  DeleteMessages(user1Id: number, user2Id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.url}DeleteMessagesBetween/${user1Id}/${user2Id}`);
  }

  PostMessage(newMessage: Message): Observable<Message> {
    return this.http.post<Message>(`${this.url}PostMessage`, newMessage, this.httpOptions);
  }

  OngoingConversationUsers(userId: number): Observable<MessageUser[]> {
    return this.http.get<MessageUser[]>(`${this.url}GetOngoingMessages/${userId}`);
  }

  GetMessageUser(userId: number): Observable<MessageUser> {
    return this.http.get<MessageUser>(`${this.url}GetMessageUser/${userId}`);
  }
}
