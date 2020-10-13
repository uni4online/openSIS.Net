import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient} from '@angular/common/http';
import { SchoolAddViewModel } from '../models/schoolMasterModel';
import { AllSchoolListModel, GetAllSchoolModel, OnlySchoolListModel } from '../models/getAllSchoolModel';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {
  private schoolId;
  private messageSource = new BehaviorSubject(false);
  currentMessage = this.messageSource.asObservable();
  apiUrl:string = environment.apiURL;
  constructor(private http: HttpClient) { }

  GetAllSchoolList(obj: GetAllSchoolModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/School/getAllSchoolList";   
    return this.http.post<AllSchoolListModel>(apiurl,obj)
  }
  
  GetAllSchools(obj: OnlySchoolListModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/School/getAllSchools";
    return this.http.post<AllSchoolListModel>(apiurl,obj);
  }

  ViewSchool(obj: SchoolAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/School/viewSchool"; 
    return this.http.post<SchoolAddViewModel>(apiurl,obj)
  }  

  AddSchool(obj: SchoolAddViewModel){  
   
   if(obj.schoolMaster.longitude != null){
    obj.schoolMaster.longitude=+obj.schoolMaster.longitude;
  }
  if(obj.schoolMaster.latitude != null){
    obj.schoolMaster.latitude=+obj.schoolMaster.latitude;
  }
    let apiurl = this.apiUrl + obj._tenantName+ "/School/addSchool"; 
    return this.http.post<SchoolAddViewModel>(apiurl,obj)
  }  
  UpdateSchool(obj: SchoolAddViewModel){  
    if(obj.schoolMaster.longitude != null){
      obj.schoolMaster.longitude=+obj.schoolMaster.longitude;
    }
    if(obj.schoolMaster.latitude != null){
      obj.schoolMaster.latitude=+obj.schoolMaster.latitude;
    }
  
    let apiurl = this.apiUrl + obj._tenantName+ "/School/updateSchool"; 
    return this.http.put<SchoolAddViewModel>(apiurl,obj)
  }  

   setSchoolId(id: number) {
     this.schoolId=id
   }
   getSchoolId(){
    return this.schoolId;
    }

  changeMessage(message: boolean) {
    this.messageSource.next(message)
  } 
  



  
}
