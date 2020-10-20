import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AddGradeLevelModel, GelAllGradeEquivalencyModel, GetAllGradeLevelsModel } from '../models/gradeLevelModel';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GradeLevelService {

  apiUrl:string = environment.apiURL;
  constructor(private http: HttpClient) { }

  getAllGradeLevels(obj: GetAllGradeLevelsModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Gradelevel/getAllGradeLevels";   
    return this.http.post<GetAllGradeLevelsModel>(apiurl,obj)
  }

  addGradelevel(obj:AddGradeLevelModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Gradelevel/addGradelevel";
    return this.http.post<AddGradeLevelModel>(apiurl,obj)
  }

  updateGradelevel(obj:AddGradeLevelModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Gradelevel/updateGradelevel";
    return this.http.put<AddGradeLevelModel>(apiurl,obj)
  }

  deleteGradelevel(obj:AddGradeLevelModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Gradelevel/deleteGradelevel";
    return this.http.post<AddGradeLevelModel>(apiurl,obj)
  }

  getAllGradeEquivalency(obj:GelAllGradeEquivalencyModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Gradelevel/getAllGradeEquivalency";
    return this.http.post<GelAllGradeEquivalencyModel>(apiurl,obj)
  }
}
