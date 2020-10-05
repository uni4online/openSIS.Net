import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AddGradeLevelModel, GetAllGradeLevelsModel } from '../models/gradeLevelModel';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GradeLevelService {

  apiUrl:string = environment.apiURL;
  constructor(private http: HttpClient) { }

  GetGradeLevels(obj: GetAllGradeLevelsModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Gradelevel/getAllGradeLevels";   
    return this.http.post<GetAllGradeLevelsModel>(apiurl,obj)
  }

  AddGradeLevel(obj:AddGradeLevelModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Gradelevel/addGradelevel";
    return this.http.post<AddGradeLevelModel>(apiurl,obj)
  }

  UpdateGradeLevel(obj:AddGradeLevelModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Gradelevel/updateGradelevel";
    return this.http.put<AddGradeLevelModel>(apiurl,obj)
  }

  DeleteGradeLevel(obj:AddGradeLevelModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Gradelevel/deleteGradelevel";
    return this.http.post<AddGradeLevelModel>(apiurl,obj)
  }

  private message = new BehaviorSubject(false);
  sharedMessage = this.message.asObservable();

  updateGradeLevelTable(message: boolean) {
    this.message.next(message)
  }
}
