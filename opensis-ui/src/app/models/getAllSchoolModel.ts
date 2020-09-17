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
    _tenantName:Number;
    _token:Number;
    _failure:Boolean;
    _message:String;
}
