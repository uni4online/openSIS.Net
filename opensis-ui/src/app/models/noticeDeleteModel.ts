import { CommonField } from './commonField';

export class NoticeDeleteModel extends CommonField {
    public NoticeId: number
    constructor() {
        super();
        this.NoticeId = 0;
    }
}
