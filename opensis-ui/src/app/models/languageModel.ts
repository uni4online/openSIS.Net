import { CommonField } from "../models/commonField";


class tableLanguage {
    langId: number;
    lcid: string;
    locale: string;
    languageCode: string;
    userMaster: []
}

export class LanguageModel extends CommonField {
    
    public tableLanguage : tableLanguage;
   
    constructor() {
        super();
        this.tableLanguage=null;     
    }
}


