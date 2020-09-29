import { schoolDetailsModel } from "../models/schoolDetailsModel";
import { CommonField } from "../models/commonField";
export class SchoolMasterModel {  

    public tenantId:string;
    public schoolId:number;
    public schoolInternalId:string;
    public schoolAltId:string;
    public schoolStateId:string
    public schoolDistrictId: string;
    public schoolLevel: string;
    public schoolClassification: string;
    public schoolName: string;
    public alternateName: string;
    public streetAddress1: string;
    public streetAddress2: string;
    public city: any;
    public county: string;
    public division: string;
    public state?: any;
    public district: string;
    public zip: string;
    public latitude?: number;
    public longitude?:number;
    public country?: any;
    public currentPeriodEnds?: number;
    public maxApiChecks?: number;
    public features:string;
    public planId?:number;
    public createdBy: string;
    public dateCreated:string;
    public modifiedBy: string;
    public dateModifed:string;
    public tableSchoolDetail:[schoolDetailsModel];
    constructor() {        
       
      this.tenantId= sessionStorage.getItem("tenantId");
      this.tableSchoolDetail= [new schoolDetailsModel];
     
      }
}

export class SchoolAddViewModel extends CommonField {
  public tblSchoolMaster: SchoolMasterModel;
  constructor() {
      super();
      this.tblSchoolMaster= new SchoolMasterModel();
      this.tblSchoolMaster.latitude=null;
      this.tblSchoolMaster.longitude=null;
  }
}

