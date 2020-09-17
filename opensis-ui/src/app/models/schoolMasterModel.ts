
export class SchoolMasterModel {  

    public tenant_Id:String;
    public school_Id:any;
    public school_Alt_Id:string;
    public school_State_Id:string
    public school_District_Id: string;
    public school_Level: string;
    public school_Classification: string;
    public school_Name: string;
    public alternate_Name: string;
    public street_Address_1: string;
    public street_Address_2: string;
    public city: string;
    public county: string;
    public division: string;
    public state: string;
    public district: string;
    public zip: string;
    public country: string;
    public geoPosition: string;
    public current_Period_ends: string;
    public max_api_checks: Number;
    public features:string;
    public created_By: any;
    public date_Created: string;
    public modified_By: any;
    public date_Modifed:string;

    constructor() {        
       
      this.tenant_Id= "1E93C7BF-0FAE-42BB-9E09-A1CEDC8C0355";
      this.school_Id=0;
      this.school_Alt_Id= "";
      this.school_State_Id= "";
      this.school_District_Id= "";
      this.school_Level= "";
      this.school_Classification= "";
      this.school_Name= "";
      this.alternate_Name= "";
      this.street_Address_1="";
      this.street_Address_2= "";
      this.city="";
      this.county= ' ';
      this.division= ' ';
      this.state= ' '; 
      this.district= ' ';
      this.zip= "";
      this.country= "";
      this.geoPosition= null;
      this.current_Period_ends= "2020-09-08T17:05:22.135Z";
      this.max_api_checks= 0;
      this.features= "";
      this.created_By= ' ',
      this.date_Created= "2020-09-08T17:05:22.135Z";
      this.modified_By= ' ';
      this.date_Modifed= "2020-09-08T17:05:22.135Z"
       
      }
}

