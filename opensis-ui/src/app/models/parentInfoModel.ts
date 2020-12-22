import { CommonField } from "../models/commonField";
import { StudentMasterModel } from "../models/studentModel";
export class ParentInfoModel {
    public tenantId: string;
    public schoolId: number;
    public studentId: number;
    public parentId: number;
    public relationship: string;
    public salutation: string;
    public firstname: string;
    public middlename: string;
    public lastname: string;
    public homePhone: string;
    public workPhone: string;
    public mobile: string;
    public personalEmail: string;
    public workEmail: string;
    public userProfile: string;
    public studentAddressSame: boolean;
    public addressLineOne: string;
    public addressLineTwo: string;
    public country: number | string;
    public city: string;
    public state: string;
    public zip: string;
    public isCustodian: boolean;
    public isPortalUser: boolean;
    public loginEmail: string;
    public suffix: string;
    public busNo: string;
    public busPickup: boolean;
    public busDropoff: boolean;
    public contactType: string;
    public associationship: string;
    public lastUpdated: string;
    public updatedBy: string;
    public studentMaster: {};
    public getStudentForView:[StudentMasterModel];
    public students : [];
    constructor() {
        this.tenantId = sessionStorage.getItem("tenantId");
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");
    }
}

export class AddParentInfoModel extends CommonField {
    public parentInfo: ParentInfoModel;
    public passwordHash: number;
    public getStudentForView: [];

    constructor() {
        super();
        this.parentInfo = new ParentInfoModel();
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
        this.getStudentForView = null;

    }
}

export class ParentInfoList extends CommonField{ 
    public parentInfoForView:[];    
    public tenantId: string;
    public schoolId: number;
    public studentId: number;
    public firstname:number;
    public lastname:number;
    public mobile:number;  
    public email: string;
    public streetAddress: string;
    public city: string;
    public state: string;
    public zip: string;
    public totalCount: string;
    public pageNumber: string;
    public _pageSize: string;   
    constructor() {  
        super();
        this.tenantId = sessionStorage.getItem("tenantId");
        this.firstname = null;
        this.lastname=null;
        this.mobile=null;
        this.email=null;
        this.streetAddress=null;
        this.city=null;
        this.state = null;
        this.zip=null;   
        this.schoolId= +sessionStorage.getItem("selectedSchoolId");       
        this._tenantName= sessionStorage.getItem("tenant");
        this._token=sessionStorage.getItem("token");
    }
}  

export class ViewParentInfoModel extends CommonField {
    parentInfoList: [ParentInfoModel]
    schoolId: number;
    studentId: number;
    tenantId: string;
    constructor() {
        super()
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");
        this._tenantName = sessionStorage.getItem("tenant");
        this.tenantId = sessionStorage.getItem("tenantId");
        this._token = sessionStorage.getItem("token");
    }
}
export class GetAllParentModel extends CommonField {
    tenantId: string;
    schoolId: number;
    public parentInfoForView: [ParentInfoModel]
    constructor() {
        super();
        this.tenantId = sessionStorage.getItem("tenantId");
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
}


export class GetAllParentInfoModel extends CommonField {
    public parentInfoList: [];
    public tenantId: string;
    public schoolId: number;
    public studentId: number;
    constructor() {
        super();
        this.tenantId = sessionStorage.getItem("tenantId");
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");       
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
}

export class AssociateStudent{
    public contactRelationship: string;
    public isCustodian: boolean;
} 

export class RemoveAssociateParent extends CommonField{
    public parentInfo: ParentInfoModel;
    public studentId: number;
    constructor() {
        super();
        this.parentInfo = new ParentInfoModel();
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");      

    }
} 