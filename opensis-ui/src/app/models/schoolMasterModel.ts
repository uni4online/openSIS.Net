
export class SchoolMasterModel {  

    public School_Id:Number;
    public School_Alt_Id:string;
    public School_State_Id:string
    public School_District_Id: string;
    public School_Level: string;
    public School_Classification: string;
    public School_Name: string;
    public Alternate_Name: string;
    public Street_Address_1: string;
    public Street_Address_2: string;
    public City: string;
    public County: string;
    public Division: string;
    public State: string;
    public District: string;
    public Zip: string;
    public Country: string;
    public Current_Period_ends: string;
    public Max_api_checks: Number;
    public Features:string;
    public Created_By: string;
    public Date_Created: string;
    public Modified_By: string;
    public Date_Modifed:string;

    constructor() {        
       
        this.School_Id = 0;
        this.School_Alt_Id = "";
        this.School_State_Id = "";
        this.School_District_Id = "";
        this.School_Level = "";
        this.School_Classification = "";
        this.School_Name = "";
        this.Alternate_Name = "";
        this.Street_Address_1 = "";
        this.Street_Address_2 = "";
        this.City = "";
        this.County = "";
        this.Division = "";
        this.State = "";
        this.District = "";
        this.Zip = "";
        this.Country = "";
        this.Current_Period_ends = "";
        this.Max_api_checks = 0;
        this.Features = "";
        this.Created_By = "";
        this.Date_Created = "";
        this.Modified_By = "";
        this.Date_Modifed = "";
       
      }
}