export class Message{
    text: string;
    sendDate: Date;
    sourceNickName: string;
    destNickName: string;
    constructor(item: any = null){
        this.text = item && item.text || null;
        this.sourceNickName = item && item.sourceNickName || null;
        this.destNickName = item && item.destNickName || null;
        this.sendDate = item && item.sendDate || Date.now;
    }
}