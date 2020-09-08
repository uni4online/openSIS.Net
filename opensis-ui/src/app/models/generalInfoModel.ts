import { CommonField } from "../models/commonField";

export class GeneralInfoModel extends CommonField {  

    public school_name:string;
    public alternate_name:string;
    public school_id:string
    public alternate_id: string;
    public school_level: string;
    public school_classification: string;
    public affiliation: string;
    public associations: string;
    public lowest_grade_level: string;
    public highest_grade_level: string;
    public date_school_opened: string;
    public date_school_closed: string;
    public code: string;
    public gender: string;
    public internet: string;
    public electricity: string;
    public status: string;
    public street_address_1: string;
    public street_address_2: string;
    public city: string;
    public country: string;
    public division: string;
    public district: string;
    public zip: string;    
    public latitude: string;
    public longitude: string;
    public principal: string;
    public ass_principal: string;
    public telephone: string;
    public fax: string;
    public website: string;
    public email: string;
    public twitter: string;
    public facebook: string;
    public instagram: string;
    public youtube: string;
    public linkedin: string;
    public state: string;
    public county:string;
    constructor() {
        super();
       
        this.school_name = "";
        this.alternate_name = "";
        this.school_id = "";
        this.alternate_id = "";
        this.school_level = "";
        this.school_classification = "";
        this.affiliation = "";
        this.associations = "";
        this.lowest_grade_level = "";
        this.highest_grade_level = "";
        this.date_school_opened = "";
        this.date_school_closed = "";
        this.code = "";
        this.gender = "";
        this.internet = "";
        this.electricity = "";
        this.status = "";
        this.street_address_1 = "";
        this.street_address_2 = "";
        this.city = "";
        this.country = "";
        this.division = "";
        this.district = "";
        this.zip = "";
        this.latitude = "";
        this.longitude = "";
        this.principal = "";
        this.ass_principal = "";
        this.telephone = "";
        this.fax = "";
        this.email = "";
        this.twitter = "";
        this.facebook = "";
        this.instagram = "";
        this.youtube = "";
        this.linkedin = "";
        this.state="";
        this.county="";

        


        



    }
}