
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
    public city: string;
    public county: string;
    public division: string;
    public state: string;
    public district: string;
    public zip: string;
    public country: string;
    public geoPosition: string;
    public currentPeriodEnds?: number;
    public maxApiChecks?: number;
    public features:string;
    public planId?:number;
    public createdBy: string;
    public dateCreated?: number;
    public modifiedBy: string;
    public dateModifed?:number;

    constructor() {        
       
      this.tenantId= sessionStorage.getItem("tenantId");;
      this.schoolId=0;
      this.schoolInternalId=null;
      this.schoolAltId= null;
      this.schoolStateId= null;
      this.schoolDistrictId= null;
      this.schoolLevel= null;
      this.schoolClassification= null;
      this.schoolName= null;
      this.alternateName= null;
      this.streetAddress1=null;
      this.streetAddress2= null;
      this.city=null;
      this.county= null;
      this.division= null;
      this.state= null; 
      this.district= null;
      this.zip= null;
      this.country= null;
      this.geoPosition= null;
      this.currentPeriodEnds= null;
      this.maxApiChecks= null;
      this.features= null;
      this.planId=null;
      this.createdBy= null;
      this.dateCreated= null;
      this.modifiedBy= null;
      this.dateModifed= null;
       
      }
}

