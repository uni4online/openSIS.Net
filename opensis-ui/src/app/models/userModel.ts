import { CommonField } from "../models/commonField";



export class UserViewModel extends CommonField {
    
    public password : string;
    public email: string;
    public userId?:  number;
    public tenantId: string;
    constructor() {
        super();
        
        this.tenantId ="1E93C7BF-0FAE-42BB-9E09-A1CEDC8C0355";
        this.userId = 0;
        this.email="";
        this.password = "";
    }
}

export class CheckUserEmailAddressViewModel extends CommonField {
    public tenantId: string;
    public emailAddress: string;
    public isValidEmailAddress: boolean;
    constructor() {
        super();
        this.tenantId = sessionStorage.getItem("tenantId");
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
}
