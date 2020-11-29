import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { StudentAddModel,StudentListModel,StudentResponseListModel,GetAllStudentDocumentsList,StudentDocumentAddModel} from '../models/studentModel';
import { StudentCommentsAddView, StudentCommentsListViewModel } from '../models/studentCommentsModel'
import { BehaviorSubject, Subject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class StudentService {
  apiUrl:string = environment.apiURL;
  private currentYear = new BehaviorSubject(false);
  currentY = this.currentYear.asObservable();

  constructor(private http: HttpClient) { }

  AddStudent(obj: StudentAddModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/addStudent";  
    obj.studentMaster.studentPhoto=this.studentImage; 
    return this.http.post<StudentAddModel>(apiurl,obj)
  }

  viewStudent(obj: StudentAddModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/viewStudent"; 
    return this.http.post<StudentAddModel>(apiurl,obj)
  } 

  UpdateStudent(obj: StudentAddModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/updateStudent";
    obj.studentMaster.studentPhoto=this.studentImage; 
    return this.http.put<StudentAddModel>(apiurl,obj)
  }

  GetAllStudentList(obj: StudentListModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/getAllStudentList";   
    return this.http.post<StudentResponseListModel>(apiurl,obj) 
  } 
   
private category = new Subject;
  categoryToSend=this.category.asObservable();

  changeCategory(category:number){
      this.category.next(category);
  }
  
  private studentDetails;
  setStudentDetails(data){
    this.studentDetails=data;
  }
  getStudentDetails(){
    return this.studentDetails;
  }

  private studentId:number;
  setStudentId(id: number) {
    this.studentId=id
  }
  getStudentId(){
   return this.studentId;
   }

  private studentImage;
  setStudentImage(imageInBase64){
    this.studentImage=imageInBase64;
  }

  //Student comment
  addStudentComment(obj:StudentCommentsAddView){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/addStudentComment";
    return this.http.post<StudentCommentsAddView>(apiurl,obj) 
  }
  updateStudentComment(obj:StudentCommentsAddView){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/updateStudentComment";
    return this.http.put<StudentCommentsAddView>(apiurl,obj) 
  }
  deleteStudentComment(obj:StudentCommentsAddView){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/deleteStudentComment";
    return this.http.post<StudentCommentsAddView>(apiurl,obj) 
  }
  getAllStudentCommentsList(obj:StudentCommentsListViewModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/getAllStudentCommentsList";
    return this.http.post<StudentCommentsListViewModel>(apiurl,obj) 
  }

  
  

  // to Update student details in General for first time.
  private studentDetailsForGeneralInfo = new Subject;
  getStudentDetailsForGeneral=this.studentDetailsForGeneralInfo.asObservable();

  sendDetails(studentDetailsForGeneralInfo){
      this.studentDetailsForGeneralInfo.next(studentDetailsForGeneralInfo);
  }
  GetAllStudentDocumentsList(obj: GetAllStudentDocumentsList){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/getAllStudentDocumentsList";   
    return this.http.post<GetAllStudentDocumentsList>(apiurl,obj) 
  }
  AddStudentDocument(obj: StudentDocumentAddModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/addStudentDocument";   
    return this.http.post<StudentDocumentAddModel>(apiurl,obj)
  }
  DeleteStudentDocument(obj: StudentDocumentAddModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Student/deleteStudentDocument";   
    return this.http.post<StudentDocumentAddModel>(apiurl,obj)
  }
  

  
}
