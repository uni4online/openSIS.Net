import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AttendanceCodeCategoryModel, AttendanceCodeModel, GetAllAttendanceCategoriesListModel, GetAllAttendanceCodeModel } from '../models/attendanceCodeModel';
@Injectable({
  providedIn: 'root'
})
export class AttendanceCodeService {

  apiUrl: string = environment.apiURL;
  constructor(private http: HttpClient) { }

addAttendanceCodeCategories(AttendanceCategory:AttendanceCodeCategoryModel){
  let apiurl = this.apiUrl + AttendanceCategory._tenantName + "/AttendanceCode/addAttendanceCodeCategories";
  return this.http.post<AttendanceCodeCategoryModel>(apiurl, AttendanceCategory)
}

getAllAttendanceCodeCategories(AttendanceCategoryList:GetAllAttendanceCategoriesListModel){
  let apiurl = this.apiUrl + AttendanceCategoryList._tenantName + "/AttendanceCode/getAllAttendanceCodeCategories";
  return this.http.post<GetAllAttendanceCategoriesListModel>(apiurl, AttendanceCategoryList) 
}

updateAttendanceCodeCategories(AttendanceCategory:AttendanceCodeCategoryModel){
  let apiurl = this.apiUrl + AttendanceCategory._tenantName + "/AttendanceCode/updateAttendanceCodeCategories";
  return this.http.put<AttendanceCodeCategoryModel>(apiurl, AttendanceCategory) 
}

deleteAttendanceCodeCategories(AttendanceCategory:AttendanceCodeCategoryModel){
  let apiurl = this.apiUrl + AttendanceCategory._tenantName + "/AttendanceCode/deleteAttendanceCodeCategories";
  return this.http.post<AttendanceCodeCategoryModel>(apiurl, AttendanceCategory)
}

getAllAttendanceCode(AttendanceCode:GetAllAttendanceCodeModel){
  let apiurl = this.apiUrl + AttendanceCode._tenantName + "/AttendanceCode/getAllAttendanceCode";
  return this.http.post<GetAllAttendanceCodeModel>(apiurl, AttendanceCode)
}

addAttendanceCode(AttendanceCode:AttendanceCodeModel){
  let apiurl = this.apiUrl + AttendanceCode._tenantName + "/AttendanceCode/addAttendanceCode";
  return this.http.post<AttendanceCodeModel>(apiurl, AttendanceCode)
}

updateAttendanceCode(AttendanceCode:AttendanceCodeModel){
  let apiurl = this.apiUrl + AttendanceCode._tenantName + "/AttendanceCode/updateAttendanceCode";
  return this.http.put<AttendanceCodeModel>(apiurl, AttendanceCode)
}

deleteAttendanceCode(AttendanceCode:AttendanceCodeModel){
  let apiurl = this.apiUrl + AttendanceCode._tenantName + "/AttendanceCode/deleteAttendanceCode";
  return this.http.post<AttendanceCodeModel>(apiurl, AttendanceCode)
}


}
