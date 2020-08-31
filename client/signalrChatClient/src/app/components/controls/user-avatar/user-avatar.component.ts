import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpErrorResponse, HttpEventType} from '@angular/common/http';

@Component({
    selector: 'user-avatar',
    templateUrl: './user-avatar.component.html',
    styleUrls: ['./user-avatar.component.css'],
    providers: []
})

export class UserAvatarComponent implements OnInit{
    @Input() avatarID: string = "";
    src : string = "";


    constructor(){}

    
    ngOnInit(){
        if(this.avatarID)
            this.src = `../../../../assets/images/${this.avatarID}.jpg`;
        else{
            this.src = '../../../../assets/images/profile.jpg';
        }
    }
}