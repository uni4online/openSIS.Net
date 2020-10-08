import { CommonField } from './commonField';
import { SchoolMasterModel } from './schoolMasterModel';
import { UserViewModel } from './userModel';

export class Membership {
    public tenantId: string;
    public schoolId: number;
    public MembershipId: number;
    public Profile: string;
    public Title: string;
    public Access: string;
    public WeeklyUpdate: boolean;
    public LastUpdated: string;
    public UpdatedBy: string;
    public schoolMaster:SchoolMasterModel;
    public UserMaster: UserViewModel;
}

export class GetAllMembersList extends CommonField {
    public getAllMemberList: Membership[];
    public tenantId: string;
    public schoolId: number;
    constructor() {
        super();
        this.getAllMemberList = [];
        this.schoolId = + sessionStorage.getItem('selectedSchoolId');
        this.tenantId = sessionStorage.getItem("tenantId");
        this._tenantName = sessionStorage.getItem('tenant');
        this._token= sessionStorage.getItem("token");
    }
}