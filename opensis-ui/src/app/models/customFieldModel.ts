
import { CommonField } from "../models/commonField";
export class CustomFieldModel {
    tenantId: string;
    schoolId: number;
    fieldId: number;
    type: string;
    search: boolean;
    title: string;
    sortOrder: number;
    selectOptions: string;
    categoryId: number;
    systemField: boolean;
    required: boolean;
    defaultSelection: string;
    hide: boolean;
    lastUpdate: string;
    updatedBy: string;
    constructor(){
        this.tenantId=sessionStorage.getItem('tenantId');
        this.schoolId=+sessionStorage.getItem('selectedSchoolId');
        this.fieldId=0;
        this.type="";
        this.search=true;
        this.title="";
        this.sortOrder=0;
        this.selectOptions="";
        this.categoryId=0;
        this.systemField=false;
        this.required=false;
        this.defaultSelection="";
        this.hide=false;
        this.lastUpdate="2020-10-22T09:14:04.336Z";
        this.updatedBy="";
    }
  }
export class CustomFieldAddView extends CommonField{
    customFields:CustomFieldModel
    constructor(){
        super();
        this._tenantName=sessionStorage.getItem("tenant");
        this._token=sessionStorage.getItem("token");
        this.customFields=new CustomFieldModel()
    }
}
export class CustomFieldListViewModel extends CommonField {
    public customFieldsList: [CustomFieldModel];
    public tenantId:string;
    public schoolId: number;
    constructor() {
        super();
        this._tenantName=sessionStorage.getItem("tenant")
        this.tenantId=sessionStorage.getItem("tenantId")
        this.schoolId=+sessionStorage.getItem("selectedSchoolId")
        this._token=sessionStorage.getItem("token")
    }
}
