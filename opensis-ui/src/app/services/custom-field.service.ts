import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {CustomFieldAddView, CustomFieldListViewModel} from '../models/customFieldModel'
@Injectable({
  providedIn: 'root'
})
export class CustomFieldService {
  apiUrl:string = environment.apiURL;
  constructor(private http: HttpClient) { }
  getAllCustomField(obj:CustomFieldListViewModel){
    let apiurl=this.apiUrl+obj._tenantName+"/CustomField/getAllCustomField" ;
    return this.http.post<CustomFieldListViewModel>(apiurl,obj)
  }
  deleteCustomField(obj:CustomFieldAddView){
    let apiurl=this.apiUrl+obj._tenantName+"/CustomField/deleteCustomField" ;
    return this.http.post<CustomFieldAddView>(apiurl,obj)
  }
  updateCustomField(obj:CustomFieldAddView){
    let apiurl=this.apiUrl+obj._tenantName+"/CustomField/updateCustomField" ;
    return this.http.put<CustomFieldAddView>(apiurl,obj)
  }
  addCustomField(obj:CustomFieldAddView){
    let apiurl=this.apiUrl+obj._tenantName+"/CustomField/addCustomField" ;
    return this.http.post<CustomFieldAddView>(apiurl,obj)
  }
  

}
