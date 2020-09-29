import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient} from '@angular/common/http';
import { SchoolAddViewModel } from '../models/schoolMasterModel';
import { AllSchoolListModel, GetAllSchoolModel, OnlySchoolListModel } from '../models/getAllSchoolModel';

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
  
  GetAllSchoolList(obj: OnlySchoolListModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/School/getAllSchoolList";
    return this.http.post<AllSchoolListModel>(apiurl,obj);
  }

  GetGeneralInfoById(obj: SchoolAddViewModel){   
    //console.log("view",JSON.stringify(obj)) 
    let apiurl = this.apiUrl + obj._tenantName+ "/School/viewSchool"; 
    return this.http.post<SchoolAddViewModel>(apiurl,obj)
  }  

  SaveGeneralInfo(obj: SchoolAddViewModel){  
   //console.log("add",JSON.stringify(obj)) 
   if(obj.tblSchoolMaster.longitude != null){
    obj.tblSchoolMaster.longitude=Number(obj.tblSchoolMaster.longitude);
  }
  if(obj.tblSchoolMaster.latitude != null){
    obj.tblSchoolMaster.latitude=Number(obj.tblSchoolMaster.latitude);
  }
    let apiurl = this.apiUrl + obj._tenantName+ "/School/addSchool"; 
    return this.http.post<SchoolAddViewModel>(apiurl,obj)
  }  
  UpdateGeneralInfo(obj: SchoolAddViewModel){  
    if(obj.tblSchoolMaster.longitude != null){
      obj.tblSchoolMaster.longitude=Number(obj.tblSchoolMaster.longitude);
    }
    if(obj.tblSchoolMaster.latitude != null){
      obj.tblSchoolMaster.latitude=Number(obj.tblSchoolMaster.latitude);
    }
    //console.log("edit",JSON.stringify(obj))
    let apiurl = this.apiUrl + obj._tenantName+ "/School/updateSchool"; 
    return this.http.put<SchoolAddViewModel>(apiurl,obj)
  }  

  
}
