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
        this.subjectId = 0;
        this.createdBy = sessionStorage.getItem("email");
        this.updatedBy = sessionStorage.getItem("email");
    }  
}



export class AddSubjectModel extends CommonField{
    public subjectList: [SubjectModel];    
    constructor() {
        super();
        this.subjectList = [new SubjectModel()];   
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 

export class UpdateSubjectModel extends CommonField{
    public subjectList: [SubjectModel];    
    constructor() {
        super();
        this.subjectList = [new SubjectModel()];   
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 
export class MassUpdateSubjectModel extends CommonField{
    public subjectList: [{}];    
    constructor() {
        super();
        this.subjectList = [{}];   
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 
export class DeleteSubjectModel extends CommonField{
    public subject: SubjectModel;    
    constructor() {
        super();
        this.subject = new SubjectModel();   
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
        this.programId = 0;
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
    public programList: [ProgramsModel];    
    constructor() {
        super();
        this.programList = [new ProgramsModel()];   
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 

export class UpdateProgramModel extends CommonField{
    public programList: [ProgramsModel];    
    constructor() {
        super();
        this.programList = [new ProgramsModel()];   
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 

export class MassUpdateProgramModel extends CommonField{
    public programList: [{}];    
    constructor() {
        super();
        this.programList = [{}];   
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 
export class DeleteProgramModel extends CommonField{
    public programs: ProgramsModel;    
    constructor() {
        super();
        this.programs = new ProgramsModel();   
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 

export class GetAllCourseListModel extends CommonField{
    public courseList:[];
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
export class CourseStandardModel {
    public tenantId: string;
    public schoolId: number;
    public courseId:number;
    public standardRefNo: string;
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
export class CourseModel {
    public tenantId: string;
    public schoolId: number;
    public courseId: number;
    public courseTitle: string;
    public courseShortName: string;
    public courseGradeLevel: string;
    public courseProgram: string;
    public courseSubject: string;
    public courseCategory: number;
    public creditHours: string;
    public courseDescription: string;
    public isCourseActive: boolean;
    public createdBy: string;
    public createdOn: string;
    public updatedBy: string;
    public updatedOn: string;
    public courseStandard:[CourseStandardModel];
    constructor() {      
        this.tenantId = sessionStorage.getItem("tenantId");
        this.schoolId = +sessionStorage.getItem("selectedSchoolId");      
        this.createdBy = sessionStorage.getItem("email");
        this.updatedBy = sessionStorage.getItem("email");
        this.courseStandard = [new CourseStandardModel()]
    }  
}
export class AddCourseModel extends CommonField{
    public course: CourseModel; 
    public programId:number;
    public subjectId:number;   
    constructor() {
        super();
        this.course = new CourseModel();          
        this._tenantName = sessionStorage.getItem("tenant");
        this._token = sessionStorage.getItem("token");
    }
} 