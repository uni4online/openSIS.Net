import { CommonField } from './commonField';

export class NoticeModel {
    public tenantId: string;
    public schoolId: number;
    public noticeId: number;
    public targetMembershipIds: string;
    public title: string;
    public body: string;
    public validFrom: string;
    public validTo: string;
    public createdBy: string;
    constructor() {
        this.schoolId = 1;
        this.tenantId = sessionStorage.getItem("tenantId");
        this.createdBy = "Souvik";
    }
}

export class NoticeAddViewModel extends CommonField {
    public notice: NoticeModel;
    constructor() {
        super();
        this.notice = new NoticeModel();
    }
}


export class NoticeListViewModel extends CommonField {
    public noticeList: NoticeModel[];
    constructor() {
        super();
        this.noticeList = [];
    }
}

