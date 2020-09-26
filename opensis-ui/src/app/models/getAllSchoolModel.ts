export class GetAllSchoolModel {
        pageNumber: number;
        pageSize: Number;
        _tenantName: String;
        _token: String;
        _failure: Boolean;
        _message: String;
    constructor() {
        this.pageNumber=1;
        this.pageSize=10;
        this._tenantName="";
        this._token="";
        this._failure=false;
        this._message="";
    }
}

export class AllSchoolListModel{
    getSchoolForView:[];
    totalCount:Number;
    pageNumber:Number;
    _pageSize:Number;
    _tenantName:string;
    _token:string;
    _failure:Boolean;
    _message:String;
}

export class OnlySchoolListModel{
    getSchoolForView: [];
      tenantId: string;
      totalCount: number;
      pageNumber: number;
      _pageSize: number;
      _tenantName: string;
      _token: string;
      _failure: boolean;
      _message: string

      constructor(){
        this.getSchoolForView=null;
        this.tenantId="";
        this.totalCount=null;
        this.pageNumber=null;
        this._pageSize=null;
        this._tenantName="";
        this._token="";
        this._failure=false;
        this._message="";
      }
}


