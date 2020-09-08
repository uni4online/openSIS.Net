

export class schoolDetailsModel{

    /* General info*/
    public  Affiliation:string;
    public  Associations:string;
    public  Locale:string;
    public  Lowest_Grade_Level:string;
    public  Highest_Grade_Level:string;
    public  Date_School_Opened:string;
    public  Date_School_Closed:string;
    public Status:boolean;
    public Gender:string;
    public Internet:string;
    public Electricity:boolean;
    public Telephone:string;
    public Fax:string;
    public Website:string;
    public Email:string;
    public Facebook:string;
    public Twitter:string;
    public Instagram:string;
    public Youtube:string;
    public LinkedIn:string;
    public Name_of_Principal:string; 
    public Name_of_Assistant_Principal:string; 
    public School_Logo:any;


    /* Wash info */
    /**Water fields **/
    public Running_Water :boolean;
    public Main_Source_of_Drinking_Water:string;
    public Currently_Available:boolean;
    public Handwashing_Available:boolean 
    public Soap_and_Water_Available:boolean
    public Hygene_Education:string
    
    /** Female_Toilet **/
    public Female_Toilet_Type:string;
    public Total_Female_Toilets:number;
    public Total_Female_Toilets_Usable:number;
    public Female_Toilet_Accessibility:string;
    
    /** Female_Toilet **/
    public Male_Toilet_Type:string;
    public Total_Male_Toilets:number;
    public Total_Male_Toilets_Usable:number;
    public Male_Toilet_Accessibility:string;
    
    /** Female_Toilet **/
    public Comon_Toilet_Type:string;
    public Total_Common_Toilets:number;
    public Total_Common_Toilets_Usable:number;
    public Common_Toilet_Accessibility:string;
    
    constructor(){

		this.Affiliation="";
		this.Associations="";
		this.Locale="";
		this.Lowest_Grade_Level="";
		this.Highest_Grade_Level="";
		this.Date_School_Opened="";
		this.Date_School_Closed="";
		this.Status
		this.Gender="";
		this.Internet="";
		this.Electricity
		this.Telephone="";
		this.Fax="";
		this.Website="";
		this.Email="";
		this.Facebook="";
		this.Twitter="";
		this.Instagram="";
		this.Youtube="";
		this.LinkedIn="";
		this.Name_of_Principal=""; 
		this.Name_of_Assistant_Principal="";
		this.School_Logo;

        this.Running_Water ;
        this.Main_Source_of_Drinking_Water="";
        this.Currently_Available;
        this.Handwashing_Available 
        this.Soap_and_Water_Available
        this.Hygene_Education="";

        this.Female_Toilet_Type="";
        this.Total_Female_Toilets=0;
        this.Total_Female_Toilets_Usable=0;
        this.Female_Toilet_Accessibility="";

        this.Male_Toilet_Type="";
        this.Total_Male_Toilets=0;
        this.Total_Male_Toilets_Usable=0;
        this.Male_Toilet_Accessibility="";

        this.Comon_Toilet_Type="";
        this.Total_Common_Toilets=0;
        this.Total_Common_Toilets_Usable=0;
        this.Common_Toilet_Accessibility="";
    }
    
}