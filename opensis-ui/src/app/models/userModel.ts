import { CommonField } from "../models/commonField";

export class UserModel {

    public tenant_id : string;
    public school_id: number
    public user_id: number;
    public name : string;  
    public emailAddress : string
    public passwordHash : string;
    constructor() {
        this.tenant_id ="1E93C7BF-0FAE-42BB-9E09-A1CEDC8C0355";
        this.school_id = 0;
        this.user_id = 0;
        this.name = "";  
        this.emailAddress = "test";
        this.passwordHash = null;
    }
}

export class UserViewModel extends CommonField {
    //public user: UserModel;
    public password : string;
    public email: string;
    public user_id?:  number;
    public tenant_id: string;
    constructor() {
        super();
        //this.user= new UserModel();
        this.tenant_id ="1E93C7BF-0FAE-42BB-9E09-A1CEDC8C0355";
        this.user_id = 0;
        this.email="";
        this.password = "";
    }
}
