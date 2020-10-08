import { CountryModel } from '../models/countryModel';
import { StateModel } from '../models/stateModel';
import { CityModel } from '../models/cityModel';
import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { LanguageModel } from '../models/languageModel';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  apiUrl:string = environment.apiURL;
  constructor(private http: HttpClient) { }
  GetAllCountry(obj: CountryModel){  
    let apiurl = this.apiUrl + obj._tenantName+ "/Common/getAllCountries"; 
    return this.http.post<CountryModel>(apiurl,obj)
  }  
  GetAllState(obj: StateModel){  
     
    let apiurl = this.apiUrl + obj._tenantName+ "/Common/getAllStatesByCountry"; 
    return this.http.post<StateModel>(apiurl,obj)
  } 
  GetAllCity(obj: CityModel){  
  
    let apiurl = this.apiUrl + obj._tenantName+ "/Common/getAllCitiesByState"; 
    return this.http.post<CityModel>(apiurl,obj)
  }  
  GetAllLanguage(obj: LanguageModel){  
  
    let apiurl = this.apiUrl + obj._tenantName+ "/Common/getAllLanguage"; 
    return this.http.post<LanguageModel>(apiurl,obj)
  }  

 

}
