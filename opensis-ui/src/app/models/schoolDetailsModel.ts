
import { SchoolMasterModel } from "../models/schoolMasterModel";
import { CommonField } from "../models/commonField";
 
export class schoolDetailsModel {    

    public  id:Number;
    public  tenant_Id:string;
    public  school_Id:Number;

    public schoolMaster: SchoolMasterModel;
    
    public affiliation: string;
    public associations: string;
    public locale: string;
    public lowest_Grade_Level: string;
    public highest_Grade_Level: string;
    public date_School_Opened: string;
    public date_School_Closed: string;
    public status: Boolean;
    public gender: string;
    public internet: Boolean;
    public electricity: Boolean;
    public telephone: string;
    public fax: string;
    public website: string;
    public email: string;
    public facebook: string;
    public twitter: string;
    public instagram: string;
    public youtube: string;
    public linkedIn: string;
    public name_of_Principal: string;
    public name_of_Assistant_Principal: string;
    public school_Logo: string;
    public running_Water: Boolean;
    public main_Source_of_Drinking_Water: string;
    public currently_Available: Boolean;
    public female_Toilet_Type: string;
    public total_Female_Toilets: Number;
    public total_Female_Toilets_Usable: Number;
    public female_Toilet_Accessibility: string;
    public male_Toilet_Type: string;
    public total_Male_Toilets: Number;
    public total_Male_Toilets_Usable: Number;
    public male_Toilet_Accessibility: string;
    public  comon_Toilet_Type: string;
    public total_Common_Toilets: Number;
    public total_Common_Toilets_Usable: Number;
    public common_Toilet_Accessibility: string;
    public  handwashing_Available: Boolean;
    public soap_and_Water_Available: Boolean;
    public hygene_Education: string;
   
    constructor(){
        this.id= 0;
        this.tenant_Id=  "1E93C7BF-0FAE-42BB-9E09-A1CEDC8C0355";
        this.school_Id=  0;

        this.schoolMaster= new SchoolMasterModel();

		this.affiliation="";
		this.associations="";
		this.locale="";
		this.lowest_Grade_Level="";
		this.highest_Grade_Level="";
		this.date_School_Opened=null;
		this.date_School_Closed=null;
		this.status=false;
		this.gender="";
		this.internet=false;
		this.electricity=false;
		this.telephone="";
		this.fax="";
		this.website="";
		this.email="";
		this.facebook="";
		this.twitter="";
		this.instagram="";
		this.youtube="";
		this.linkedIn="";
		this.name_of_Principal=""; 
		this.name_of_Assistant_Principal="";
		this.school_Logo=null;
        this.running_Water=false ;
        this.main_Source_of_Drinking_Water="";
        this.currently_Available=false ;
        this.female_Toilet_Type="";
        this.total_Female_Toilets=0;
        this.total_Female_Toilets_Usable=0;
        this.female_Toilet_Accessibility="";
        this.male_Toilet_Type="";
        this.total_Male_Toilets=0;
        this.total_Male_Toilets_Usable=0;
        this.male_Toilet_Accessibility="";
        this.comon_Toilet_Type="";
        this.total_Common_Toilets=0;
        this.total_Common_Toilets_Usable=0;
        this.common_Toilet_Accessibility="";
        this.handwashing_Available=false ; 
        this.soap_and_Water_Available=false ;
        this.hygene_Education="";      
        
        
    }
    
}


export class SchoolAddViewModel extends CommonField {
    public tblSchoolDetail: schoolDetailsModel;
    public latitude: Number;
    public longitude:Number;
    constructor() {
        super();
        this.tblSchoolDetail= new schoolDetailsModel();
        this.latitude=0;
        this.longitude=0;
    }
}