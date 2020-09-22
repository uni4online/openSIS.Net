
import { SchoolMasterModel } from "../models/schoolMasterModel";
import { CommonField } from "../models/commonField";
 
export class schoolDetailsModel {    

    public  id:number;
    public  tenantId:string;
    public  schoolId?:number;

    public tableSchoolMaster: SchoolMasterModel;
    
    public affiliation: string;
    public associations: string;
    public locale: string;
    public lowestGradeLevel: string;
    public highestGradeLevel: string;
    public dateSchoolOpened?: number;
    public dateSchoolClosed?: number;
    public status?: boolean;
    public gender: string;
    public internet?: boolean;
    public electricity?: boolean;
    public telephone: string;
    public fax: string;
    public website: string;
    public email: string;
    public facebook: string;
    public twitter: string;
    public instagram: string;
    public youtube: string;
    public linkedIn: string;
    public nameOfPrincipal: string;
    public nameOfAssistantPrincipal: string;
    public schoolLogo: string;
    public runningWater: boolean;
    public mainSourceOfDrinkingWater: string;
    public currentlyAvailable?: boolean;
    public femaleToiletType: string;
    public totalFemaleToilets?: number;
    public totalFemaleToiletsUsable?: number;
    public femaleToiletAccessibility: string;
    public maleToiletType: string;
    public totalMaleToilets?: number;
    public totalMaleToiletsUsable?: number;
    public maleToiletAccessibility: string;
    public comonToiletType: string;
    public totalCommonToilets?: number;
    public totalCommonToiletsUsable?: number;
    public commonToiletAccessibility: string;
    public handwashingAvailable?: boolean;
    public soapAndWaterAvailable?: boolean;
    public hygeneEducation: string;
   
    constructor(){
        this.id= 0;
        this.tenantId=  sessionStorage.getItem("tenantId");
        this.schoolId=  0;
        this.tableSchoolMaster= new SchoolMasterModel();
		this.affiliation=null;
		this.associations=null;
		this.locale=null;
		this.lowestGradeLevel=null;
		this.highestGradeLevel=null;
		this.dateSchoolOpened=null;
		this.dateSchoolClosed=null;
		this.status=null;
		this.gender=null;
		this.internet=null;
		this.electricity=null;
		this.telephone=null;
		this.fax=null;
		this.website=null;
		this.email=null;
		this.facebook=null;
		this.twitter=null;
		this.instagram=null;
		this.youtube=null;
		this.linkedIn=null;
		this.nameOfPrincipal=null; 
		this.nameOfAssistantPrincipal=null;
		this.schoolLogo=null;
        this.runningWater=null;
        this.mainSourceOfDrinkingWater=null;
        this.currentlyAvailable=null;
        this.femaleToiletType=null;
        this.totalFemaleToilets=null;
        this.totalFemaleToiletsUsable=null;
        this.femaleToiletAccessibility=null;
        this.maleToiletType=null;
        this.totalMaleToilets=null;
        this.totalMaleToiletsUsable=null;
        this.maleToiletAccessibility=null;
        this.comonToiletType=null;
        this.totalCommonToilets=null;
        this.totalCommonToiletsUsable=null;
        this.commonToiletAccessibility=null;
        this.handwashingAvailable=null; 
        this.soapAndWaterAvailable=null;
        this.hygeneEducation=null;      
        
        
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