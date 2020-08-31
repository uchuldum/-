import { Component, NgZone } from '@angular/core';  
import { Message } from './models/message';  
import { User } from './models/user';  
import { ChatService } from './core/services/api/chat.service';  
import { faEnvelope, faBell } from '@fortawesome/free-solid-svg-icons';
import { DialogService } from "ngm-bootstrap-modal";
import { DialogModal } from './components/controls/dialog/dialog-modal.component';
import { userInfo } from 'os';

@Component({  
  selector: 'app-root',  
  templateUrl: './app.component.html',  
  styleUrls: ['./app.component.css']  
})  
export class AppComponent {  
  faEnvelope = faEnvelope;
  faBell = faBell;

  title = 'Чат на SignalR';  

  txtMessage: string = '';  

  nickName: string = '';

  messages = new Array<Message>();  
  message = new Message();  

  messagesHistory = new Array<Message>();  
  isHistory: boolean = false;

  messagesDialog = new Array<Message>();  
  isDialog: boolean = false;

  connectedUsers = new Array<User>();

  isConnected: boolean = false;

  destNickName: string = "";

  newMessage: boolean = true;

  constructor(  
    private chatService: ChatService,  
    private _ngZone: NgZone ,
    private dialogService: DialogService,
  ) {}  

  login(){
    this.chatService.connect(this.nickName);

    this.isConnected = true;

    this.title += `. Здравствуйте, ${this.nickName}.`;

    this.subscribeToEvents();
  }

  sendMessage(): void {  
    if (this.txtMessage) {  
      this.message = new Message();  
      this.message.text = this.txtMessage;  
      this.message.sendDate = new Date();  
      this.messages.push(this.message);  
      this.chatService.sendMessage(this.message);  
      this.txtMessage = '';  
    }  
  } 

  isNewMessage(nickName){

  }

  getDialog(user: User){
    if(user.nickName != this.nickName){
      user.newMessage = false;
      this.chatService.getDialogFunc(user.nickName);  
      this.destNickName = user.nickName;
    }
  }

  private subscribeToEvents(): void {  

    this.chatService.MessageReceivedToClient.subscribe((message: Message) => { 
      let nickName = message.sourceNickName;

      let updateItem = this.connectedUsers.find(item => item.nickName === nickName);

      let index = this.connectedUsers.indexOf(updateItem);

      if(index > -1)
        this.connectedUsers[index].newMessage = true;
    }); 

    this.chatService.clients.subscribe((clients: User[]) => { 
      this.connectedUsers = clients;
    });  

    this.chatService.connected.subscribe((client: User) => { 

      let updateItem = this.connectedUsers.find(item => item.nickName === client.nickName);

      let index = this.connectedUsers.indexOf(updateItem);

      if(index > -1)
        this.connectedUsers[index] = client;
      else
        this.connectedUsers.push(client);
    }); 

    this.chatService.disconnected.subscribe((client: User) => { 
      let updateItem = this.connectedUsers.find(item => item.nickName === client.nickName);

      let index = this.connectedUsers.indexOf(updateItem);

      if(index > -1)
        this.connectedUsers[index] = client;
    }); 

    this.chatService.urMessages.subscribe((messages: Message[]) => { 
      this.messagesHistory = messages;
      this.isHistory = true;
    });


    this.chatService.getDialog.subscribe((messages: Message[]) => { 
      this.messagesDialog = messages;
      this.dialogService.addDialog(DialogModal, {
        sourceNickName:  `${this.nickName}`,
        destNickName: `${this.destNickName}`,
        footerText: '',
        dialogMessages: this.messagesDialog,
        }).subscribe((result: any)=>{
          this.isDialog = false;
        });
    });
  }  
} 