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
