import { CommonField } from "../models/commonField";

export class GetAllGradeLevelsModel{
    public tableGradelevelList:[];
    public tenantId: string;
    public schoolId: number;
    public _tenantName: string;
    public _token: string;
    public _failure: true;
    public _message: string;   
    
    constructor(){
          this.tenantId = sessionStorage.getItem("tenantId");
    }
}

class tblGradelevel {
    public tenantId: string;
    public schoolId: number;
    public gradeId: number;
    public shortName: string;
    public title: string;
    public gradeLevelEquivalency: string;
    public ageRange: string;
    public educationalStage: string;
    public nextGrade: string;
    public nextGradeId:number;
    public sortOrder: number;
    public lastUpdated: string;
    public updatedBy: string

    constructor(){
        this.tenantId = sessionStorage.getItem("tenantId");
        this.gradeLevelEquivalency=null;
        this.ageRange=null;
        this.educationalStage=null;
        this.updatedBy = "Souvik";
    }
}
export class AddGradeLevelModel extends CommonField{
        public tblGradelevel: tblGradelevel
        constructor(){
            super();
            this.tblGradelevel = new tblGradelevel();
        }

}