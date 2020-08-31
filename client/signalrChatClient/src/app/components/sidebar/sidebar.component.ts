import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ChatService } from '../../core/services/api/chat.service';
import { Message } from '../../models/message';
import { faBell } from '@fortawesome/free-solid-svg-icons';
@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css']
})

export class SidebarComponent implements OnInit {

    @Input() nickName: string = "";
    @Input() history: Message[];

    faBell = faBell;

    isAuth: boolean;
    isAuthSubscription: Subscription;
    userRole: string;

    messages = new Array<Message>();  
    message = new Message();  

    txtMessage: string;

    newMessage: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private chatService: ChatService
    ) {}

    ngOnInit(){
        this.history.forEach(message => {
            if(message.destNickName == null || message.destNickName == ''){
                this.messages.unshift(message);
            }
        });
        this.subscribeToEvents();
    }

    sendMessage(): void {  
        if (this.txtMessage) {  
            this.message = new Message();  
            this.message.text = this.txtMessage;  
            this.message.sendDate = new Date();  
            this.message.sourceNickName = this.nickName;

            this.chatService.sendMessage(this.message);  
            this.txtMessage = '';  
        }  
    }  

    private subscribeToEvents(): void {  
        this.chatService.MessageReceivedAll.subscribe((message: Message) => { 
            this.messages.unshift(message);  
            if(message.sourceNickName !== this.nickName)
                this.newMessage = true;
        });  
    }

    over(){
        this.newMessage = false;
    }
}