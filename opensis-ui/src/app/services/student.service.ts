import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { StudentAddModel,StudentListModel,StudentResponseListModel} from '../models/studentModel';
import { BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private initialData = new BehaviorSubject("");
  currentData = this.initialData.asObservable();
  apiUrl:string = environment.apiURL;
  private currentYear = new BehaviorSubject(false);
  currentY = this.currentYear.asObservable();

  constructor(private http: HttpClient) { }

  AddStudent(obj: StudentAddModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/addStudent";   
    return this.http.post<StudentAddModel>(apiurl,obj)
  }
  UpdateStudent(obj: StudentAddModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/updateStudent";   
    return this.http.put<StudentAddModel>(apiurl,obj)
  }

  GetAllStudentList(obj: StudentListModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/getAllStudentList";   
    return this.http.post<StudentResponseListModel>(apiurl,obj) 
  }  

  setViewGeneralInfoData(data){
    this.initialData.next(data)
  }
}
