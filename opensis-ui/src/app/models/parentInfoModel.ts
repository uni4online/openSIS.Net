import { CommonField } from "../models/commonField";
import { StudentMasterModel } from "./studentModel";

export class ParentInfoModel{
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
    getStudentForView:[StudentMasterModel];
    constructor(){
        this.updatedBy=sessionStorage.getItem("email");
        this.getStudentForView=null;
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
export class GetAllParentModel extends CommonField{
    tenantId:string;
    schoolId:number;
    pageNumber: number;
    _pageSize: number;
    totalCount:number;
    filterParams:filterParams;
    public parentInfoForView:[ParentInfoModel]
    constructor() {
        super();
        this.tenantId= sessionStorage.getItem("tenantId");
        this.schoolId= +sessionStorage.getItem("selectedSchoolId");
        this.pageNumber=1;
        this._pageSize=10;
        this.totalCount=0;
        this.filterParams=null;
        this._tenantName=sessionStorage.getItem("tenant");
        this._token=sessionStorage.getItem("token");
    }
}
export class filterParams{
    columnName: string;
    filterValue: string;
    filterOption: number;
 constructor(){
     this.columnName=null;
     this.filterOption=3;
     this.filterValue=null;
 }
}
