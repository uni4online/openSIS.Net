import { CommonField } from './commonField';

export class NoticeDeleteModel extends CommonField {
    public NoticeId: number
    constructor() {
        super();
        this.NoticeId = 0;
        this._tenantName=sessionStorage.getItem('tenant')
        this._token=sessionStorage.getItem('token')
    }
}
