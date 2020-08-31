import { Component, OnInit, OnChanges, Input, EventEmitter, Output, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import {Observable, from} from 'rxjs';
import { DialogComponent, DialogService } from "ngm-bootstrap-modal";
import {FormControl, FormGroup, Validators, FormBuilder, FormArray, ValidatorFn, ValidationErrors, AbstractControl} from '@angular/forms';
import { ChatService } from '../../../core/services/api/chat.service'
import { Message } from '../../../models/message'

export interface IDialogModal {
    sourceNickName:string;  
    destNickName:string;  
    footerText: string;
    dialogMessages: Message[];
}

@Component({
    selector: 'dialog-modal',
    templateUrl: './dialog-modal.component.html',
    styleUrls: ['./dialog-modal.component.css'],
    providers: []
})

export class DialogModal extends DialogComponent<IDialogModal, any> implements IDialogModal, OnInit {

    sourceNickName: string ;
    destNickName:string;  
    footerText: string;
    dialogMessages: Message[];

    txtMessage: string = "";

    message = new Message();
    messages = new Array<Message>();


    constructor(
        dialogService: DialogService,
        private chatService: ChatService) {
      super(dialogService);
    }

    ngOnInit(){
      this.dialogMessages.forEach(message => {
        this.messages.push(message);
    });
    this.subscribeToEvents();
    }

    sendMessage(){
      if (this.txtMessage) {  
        this.message = new Message();  
        this.message.text = this.txtMessage;  
        this.message.sendDate = new Date();  
        this.message.sourceNickName = this.sourceNickName;
        this.message.destNickName = this.destNickName;
        this.chatService.sendMessage(this.message);  
        this.txtMessage = '';  
      }  
    }

    private subscribeToEvents(): void {  
      this.chatService.MessageReceivedToClient.subscribe((message: Message) => { 
          this.messages.push(message);  
      });  
  }
}