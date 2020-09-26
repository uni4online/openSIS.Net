import { CommonField } from "../models/commonField";

export class TableStateModel {    

    public id: number;
    public name: string;
    public countryId: number;
   
    constructor(){
        this.id= null;
        this.name=null;
        this.countryId=null;
        
    }
    
}


export class StateModel extends CommonField {
    
    public tableState : [TableStateModel];
    public countryId:number;
    constructor() {
        super();
       
    }
}