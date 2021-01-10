import { CommonField } from "../models/commonField";

export class GetAllSubjectModel extends CommonField{
    public subjectList:[];
    public tenantId:string;
    public schoolId:number;
    constructor() {
        super();
        this.tenantId = sessionStorage.getItem("tenantId");
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");  
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 
export class SubjectModel {
    public tenantId: string;
    public schoolId: number;
    public subjectId: number;
    public subjectName: string;
    public createdBy: string;
    public createdOn: string;
    public updatedBy: string;
    public updatedOn: string;
    constructor() {      
        this.tenantId = sessionStorage.getItem("tenantId");
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");  
        this.createdBy = sessionStorage.getItem("email");
        this.updatedBy = sessionStorage.getItem("email");
    }  
}



export class AddSubjectModel extends CommonField{
    public subject: SubjectModel;    
    constructor() {
        super();
        this.subject = new SubjectModel();   
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 

export class UpdateSubjectModel extends CommonField{
    public subject: [SubjectModel];    
    constructor() {
        super();
        this.subject = [new SubjectModel()];   
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 

export class ProgramsModel {
    public tenantId: string;
    public schoolId: number;
    public programId: number;
    public programName: string;
    public createdBy: string;
    public createdOn: string;
    public updatedBy: string;
    public updatedOn: string;
    constructor() {      
        this.tenantId = sessionStorage.getItem("tenantId");
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");  
        this.createdBy = sessionStorage.getItem("email");
        this.updatedBy = sessionStorage.getItem("email");
    }  
}

export class GetAllProgramModel extends CommonField{
    public programList:[];
    public tenantId:string;
    public schoolId:number;
    constructor() {
        super();
    
        this.tenantId = sessionStorage.getItem("tenantId");
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");  
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 
export class AddProgramModel extends CommonField{
    public programs: ProgramsModel;    
    constructor() {
        super();
        this.programs = new ProgramsModel();   
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 
