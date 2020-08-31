import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router'; // CLI imports router


import { UserAvatarComponent } from '../controls/user-avatar/user-avatar.component';
import { DialogModal } from '../controls/dialog/dialog-modal.component';

@NgModule({
    declarations:[
        UserAvatarComponent,
        DialogModal,
    ],
    imports:[
        CommonModule,
        //EmployeesRouting,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
    ],
    exports:[
        UserAvatarComponent,
        DialogModal,
    ]
})

export class ControlsModule {}