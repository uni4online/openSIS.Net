import { CommonField } from "../models/commonField";

class ParentInfoModel{
    public tenantId:string;
    public schoolId: number;
    public studentId: number;
    public parentId: number;
    public relationship: string;
    public firstname: string;
    public lastname: string;
    public homePhone: string;
    public workPhone: string;
    public mobile: string;
    public email: string;
    public studentAddressSame: true;
    public addressLineOne: string;
    public addressLineTwo: string;
    public country: string;
    public city: string;
    public state: string;
    public zip: string;
    public isCustodian: true;
    public isPortalUser: true;
    public portalUserId: string;
    public busNo: string;
    public busPickup: true;
    public busDropoff: true;
    public contactType: string;
    public associationship: string;
    public lastUpdated: string;
    public updatedBy: string;
    constructor(){
        this.updatedBy=sessionStorage.getItem("email");
    }
}

export class ViewParentInfoModel extends CommonField{
    parentInfoList:[ParentInfoModel]
    schoolId: number;
    studentId: number;
    tenantId:string;
    constructor(){
        super()
        this.schoolId=+sessionStorage.getItem("selectedSchoolId");
        this._tenantName=sessionStorage.getItem("tenant");
        this.tenantId=sessionStorage.getItem("tenantId");
        this._token=sessionStorage.getItem("token");
    }
}