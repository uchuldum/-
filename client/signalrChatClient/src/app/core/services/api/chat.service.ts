import { EventEmitter, Injectable } from '@angular/core';  
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';  
import { Message } from '../../../models/message';  
import { ConfigService } from '../config.service';  
@Injectable({
    providedIn: 'root'
})


export class ChatService {  
  MessageReceivedToClient = new EventEmitter<Message>();
  MessageReceivedAll = new EventEmitter<Message>();
  clients = new EventEmitter<any>();
  connected = new EventEmitter<any>();
  disconnected = new EventEmitter<any>();
  urMessages = new EventEmitter<any>();
  getDialog = new EventEmitter<any>();

  connectionEstablished = new EventEmitter<Boolean>();  
  


  private readonly prefix: string = '/ChatHub';
  private get apiPrefix(): string {
      return `${this.configService.resourceApiURI}${this.prefix}`;
  }

  private connectionIsEstablished = false;  
  private _hubConnection: HubConnection;  
  
  constructor(
    private configService: ConfigService,
  ) { }  
  
  connect(nickName: string){
    this.createConnection(nickName);  
    this.registerOnServerEvents();  
    this.startConnection();  
  }

  sendMessage(message: Message) {  
    this._hubConnection.invoke('NewMessage', message);  
  }  

  getDialogFunc(nickName: string) {  
    this._hubConnection.invoke('GetDialog', nickName);  
  } 
  
  private createConnection(nickName: string) {  
    this._hubConnection = new HubConnectionBuilder()  
      .withUrl(this.apiPrefix+`?nickName=${nickName}`)  
      .build();  
  }  
  
  private startConnection(): void {  
    this._hubConnection  
      .start()  
      .then(() => {  
        this.connectionIsEstablished = true;  
        console.log('Hub connection started');  
        this.connectionEstablished.emit(true);  
      })  
      .catch(err => {  
        console.log('Error while establishing connection, retrying...');  
        setTimeout(function () { this.startConnection(); }, 5000);  
      });  
  }  
  
  private registerOnServerEvents(): void {  
    this._hubConnection.on('MessageReceivedToClient', (data: any) => {  
      this.MessageReceivedToClient.emit(data);  
    });

    this._hubConnection.on('MessageReceivedAll', (data: any) => {  
      this.MessageReceivedAll.emit(data);  
    });

    this._hubConnection.on('Clients', (data: any) => {  
      this.clients.emit(data);  
    });

    this._hubConnection.on('Connected', (data: any) => {  
      this.connected.emit(data);  
    });

    this._hubConnection.on('Disconnected', (data: any) => {  
      this.disconnected.emit(data);  
    });

    this._hubConnection.on('GetDialog', (data: any) => {  
      this.getDialog.emit(data);  
    });

    this._hubConnection.on('UrMessages', (data: any) => {  
      this.urMessages.emit(data);  
    });
  }  
}    