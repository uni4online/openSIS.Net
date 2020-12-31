import { CommonField } from "../models/commonField";

export class TableCountryModel {
    public id: number;
    public name: string;
    public countryCode: string;
    public createdBy: string;
    public createdOn: string;
    public updatedBy: string;
    public updatedOn: string;
    public state: []
    constructor(){
        this.state = null;
        this.id= 0;
        this.name=null;
        this.countryCode=null;    
        this.createdBy = sessionStorage.getItem("email"); 
        this.updatedBy = sessionStorage.getItem("email");    
    }   
}


export class CountryModel extends CommonField {    
    public tableCountry : [];
    public stateCount: number;   
    constructor() {
        super();
        this.tableCountry= null;
        this._tenantName=sessionStorage.getItem('tenant');
        this._token=sessionStorage.getItem('token');
    }
}

export class CountryAddModel extends CommonField {
    
    public country : TableCountryModel   
    constructor() {
        super();
        this.country= new TableCountryModel();
        this._tenantName=sessionStorage.getItem('tenant');
        this._token=sessionStorage.getItem('token');
    }
}