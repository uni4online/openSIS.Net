import { CommonField } from "../models/commonField";

export class StudentMasterModel {

    public tenantId: string;
    public schoolId: number;
    public studentId: number;
    public alternateId:string;
    public districtId: number;
    public stateId: number;
    public admissionNumber: string;
    public rollNumber:string;
    public salutation: string;
    public firstGivenName: string;
    public middleName: string;
    public lastFamilyName:string;
    public suffix: string;
    public preferredName: string;
    public previousName: string;
    public socialSecurityNumber:string;
    public otherGovtIssuedNumber: string;
    public studentPhoto:string;
    public dob: string;
    public displayAge: string;
    public gender: string;
    public race:string;
    public ethnicity: string;
    public maritalStatus: string;
    public countryOfBirth: number;
    public nationality:number;
    public firstLanguageId: number;
    public secondLanguageId: number;
    public thirdLanguageId: number;
    public homePhone:string;
    public mobilePhone: string;
    public personalEmail:string;
    public schoolEmail: string;
    public twitter: string;
    public facebook: string;
    public instagram:string;
    public youtube: string;
    public linkedin: string;
    public homeAddressLineOne: string;
    public homeAddressLineTwo:string;
    public homeAddressCity: string;
    public homeAddressState: string;
    public homeAddressZip: string;
    public busNo:string;
    public schoolBusPickUp: boolean;
    public schoolBusDropOff: boolean;
    public mailingAddressSameToHome: boolean;
    public mailingAddressLineOne: string;
    public mailingAddressLineTwo:  string;
    public mailingAddressCity:  string;
    public mailingAddressState:  string;
    public mailingAddressZip:  string;
    public primaryContactRelationship:  string;
    public primaryContactFirstname:  string;
    public primaryContactLastname:  string;
    public primaryContactHomePhone:  string;
    public primaryContactWorkPhone:  string;
    public primaryContactMobile:  string;
    public primaryContactEmail:  string;
    public isPrimaryCustodian: boolean;
    public isPrimaryPortalUser: boolean;
    public primaryPortalUserId:  string;
    public primaryContactStudentAddressSame: boolean;
    public primaryContactAddressLineOne: string;
    public primaryContactAddressLineTwo: string;
    public primaryContactCity: string;
    public primaryContactState: string;
    public primaryContactZip: string;
    public secondaryContactRelationship: string;
    public secondaryContactFirstname: string;
    public secondaryContactLastname: string;
    public secondaryContactHomePhone: string;
    public secondaryContactWorkPhone: string;
    public secondaryContactMobile: string;
    public secondaryContactEmail: string;
    public isSecondaryCustodian: boolean;
    public isSecondaryPortalUser: boolean;
    public secondaryPortalUserId: string;
    public secondaryContactStudentAddressSame: boolean;
    public secondaryContactAddressLineOne: string;
    public secondaryContactAddressLineTwo: string;
    public secondaryContactCity: string;
    public secondaryContactState: string;
    public secondaryContactZip: string;
    public homeAddressCountry: number;
    public mailingAddressCountry:number;
    constructor() {     
        this.tenantId = sessionStorage.getItem("tenantId");
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");   
    }
}

export class StudentAddModel  extends CommonField{
    public studentMaster:StudentMasterModel; 
    public schoolMaster:{};
    public studentEnrollment:{};
    constructor() {
        super();        
        this.studentMaster= new StudentMasterModel(); 
        this.schoolMaster=null;
        this.studentEnrollment=null;
        this._tenantName= sessionStorage.getItem("tenant");
        this._token=sessionStorage.getItem("token");
    }
}


export class StudentResponseListModel {
    public studentMaster:[]; 
    public tenantId: string;
    public schoolId: number;
    public totalCount:number;
    public pageNumber: number;
    public _pageSize: number;
    public _tenantName: string;
    public _token: string;
    public _failure: string;
    public _message: string;
}

export class StudentListModel extends CommonField{    
    public studentMaster:[]; 
    public totalCount:number;
    public tenantId: string;
    public schoolId: number;
    public pageNumber: number;
    public pageSize: number;
    public sortingModel: sorting;
    public filterParams: filterParams;

    constructor() {
        super();        
        this.tenantId= sessionStorage.getItem("tenantId");
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");
        this.pageNumber=1;
        this.pageSize=10;
        this.sortingModel=new sorting();
        this.filterParams=null;
        this._tenantName= sessionStorage.getItem("tenant");
        this._token=sessionStorage.getItem("token");
    }
        
      
}
class sorting{
    sortColumn:string;
    sortDirection:string;
    constructor(){
        this.sortColumn="";
        this.sortDirection="";
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
