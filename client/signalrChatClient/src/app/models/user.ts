export class User{
    nickName: string;
    state: number;
    lastVizit: Date;
    avatarID: number;
    newMessage: boolean;
    constructor(item: any = null){
        this.nickName = item && item.nickName || null;
        this.newMessage = item && item.newMessage || null;
        this.state = item && item.state || 0;
        this.lastVizit = item && item.lastVizit || null;
        this.avatarID = item && item.avatarID || 0;
    }
}