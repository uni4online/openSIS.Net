import { CommonField } from './commonField';

export class GetAllMembers {
    public Membership_id : number;
    public Profile: string;
}

export class GetAllMembersList extends CommonField {
    public getAllMemberList: GetAllMembers[]  ;
    constructor() {
        super();
        this.getAllMemberList= [];
    }
}