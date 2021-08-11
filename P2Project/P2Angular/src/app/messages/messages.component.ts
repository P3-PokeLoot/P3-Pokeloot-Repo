import { Component, OnInit, Input } from '@angular/core';
import { Message } from 'src/app/Models/message';
import { MessageUser } from 'src/app/Models/MessageUser';
import { AuthenticationService } from '../service/authentication/authentication.service';
import { MessageService } from '../service/message/message.service';
import { ActivatedRoute, Route } from '@angular/router';
import { interval } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  senderId!: number;
  sender!: MessageUser;
  receiverId: number = -1;
  receiver?: MessageUser;
  messages?: Message[];
  content: string = '';
  ongoing_users?: MessageUser[];

  constructor(private messageService: MessageService, private authService: AuthenticationService,
    private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.senderId = parseInt(localStorage.getItem('userId')!);
    this.CheckUrlForReceiver();
    this.GetMessages();
    this.GetUsers();
    this.GetOngoingConversationUsers();
    setInterval(() => { this.GetMessages(); this.GetOngoingConversationUsers(); }, 10000);
  }

  CheckUrlForReceiver(): void {
    var recIdParam = this.route.snapshot.paramMap.get('receiverId');
    if (recIdParam) this.receiverId = parseInt(recIdParam);
  }

  GetOngoingConversationUsers(): void {
    this.messageService.OngoingConversationUsers(this.senderId).subscribe(
      users => this.ongoing_users = users
    );
  }

  GetMessages(): void {
    if (this.receiverId != -1) {
      this.messageService.GetMessages(this.senderId, this.receiverId).subscribe(
        messages => this.messages = messages
      );
    }
  }

  DeleteMessages(awayId: number): void {
    this.messageService.DeleteMessages(this.senderId, awayId).subscribe(
      x => { }, e => { }, () => {
        this.receiverId = -1;
        this.receiver = undefined;
        this.messages = [];
        this.GetOngoingConversationUsers();
        this.GetMessages();
        this.GetUsers();
      }
    );
  }

  GetUsers(): void {
    this.messageService.GetMessageUser(this.senderId).subscribe(
      user => this.sender = user
    );
    if (this.receiverId != -1) {
      this.messageService.GetMessageUser(this.receiverId).subscribe(
        user => this.receiver = user
      );
    }
  }

  SearchMessageUserByUsername(username: string): void {
    this.messageService.SearchMessageUserByUsername(username).subscribe(
      user => this.receiverId = user == null ? this.receiverId : user.userId,
      error => { }, () => this.ngOnInit()
    );
  }

  onSelect(userId: number): void {
    this.receiverId = userId;
    this.GetMessages();
    this.GetUsers();
  }

  onSubmit(): void {
    if (this.receiverId != -1) {
      var dateNow: Date = new Date();
      dateNow.setTime(dateNow.getTime() - dateNow.getTimezoneOffset() * 60 * 1000);
      let newMessage: Message = {
        messageId: 0,
        senderId: this.senderId,
        receiverId: this.receiverId,
        content: this.content,
        timestamp: dateNow
      }
      this.messageService.PostMessage(newMessage).subscribe(x => { }, error => { }, () => this.ngOnInit());
    }
  }

}
