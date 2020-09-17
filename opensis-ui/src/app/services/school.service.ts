import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient} from '@angular/common/http';
import { SchoolViewModel,SchoolListViewModel } from '../models/schoolModel';
import { SchoolAddViewModel } from '../models/schoolDetailsModel';
import { AllSchoolListModel, GetAllSchoolModel } from '../models/getAllSchoolModel';
@Injectable({
  providedIn: 'root'
})
export class SchoolService {
  apiUrl:string = environment.apiURL;
  constructor(private http: HttpClient) { }

  GetSchool(obj: GetAllSchoolModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/School/getAllSchools";   
    return this.http.post<AllSchoolListModel>(apiurl,obj)
  }

  SaveGeneralInfo(obj: SchoolAddViewModel){    
    let apiurl = this.apiUrl + obj._tenantName+ "/School/addSchool"; 
    return this.http.post<SchoolAddViewModel>(apiurl,obj)
  }  
}
