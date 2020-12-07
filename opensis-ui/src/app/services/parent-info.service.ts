import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { ViewParentInfoModel,GetAllParentModel } from '../models/parentInfoModel';

@Injectable({
  providedIn: 'root'
})
export class ParentInfoService {

  apiUrl: string = environment.apiURL;
  constructor(private http: HttpClient) { }

  ViewParentListForStudent(parentInfo:ViewParentInfoModel) {
    let apiurl = this.apiUrl + parentInfo._tenantName + "/ParentInfo/ViewParentListForStudent";
    return this.http.post<ViewParentInfoModel>(apiurl, parentInfo)
  }
  getAllParentInfo(Obj:GetAllParentModel){
    let apiurl = this.apiUrl + Obj._tenantName + "/ParentInfo/getAllParentInfo";
    return this.http.post<GetAllParentModel>(apiurl, Obj)
  }
}
