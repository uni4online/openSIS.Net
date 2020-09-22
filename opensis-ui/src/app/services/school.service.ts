import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
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

  GetGeneralInfoById(obj: SchoolAddViewModel){   
    //console.log(JSON.stringify(obj)) 
    let apiurl = this.apiUrl + obj._tenantName+ "/School/viewSchool"; 
    return this.http.post<SchoolAddViewModel>(apiurl,obj)
  }  

  SaveGeneralInfo(obj: SchoolAddViewModel){  
    console.log('save',JSON.stringify(obj))   
    let apiurl = this.apiUrl + obj._tenantName+ "/School/addSchool"; 
    return this.http.post<SchoolAddViewModel>(apiurl,obj)
  }  
  UpdateGeneralInfo(obj: SchoolAddViewModel){  
    console.log('edit',JSON.stringify(obj))   
    let apiurl = this.apiUrl + obj._tenantName+ "/School/updateSchool"; 
    return this.http.put<SchoolAddViewModel>(apiurl,obj)
  }  
}
