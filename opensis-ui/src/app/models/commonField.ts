export class CommonField  {
    public _failure: boolean;
    public _message:string;
    public _tenantName:string;
    public _token:string;
    public language:string;

    constructor(){
        this._failure=false;
        this._message="";
        this._tenantName="";
        this._token="";
        this.language ="en";
    }
}