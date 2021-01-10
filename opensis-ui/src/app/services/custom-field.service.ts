import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {CustomFieldAddView, CustomFieldDragDropModel, CustomFieldListViewModel} from '../models/customFieldModel';
import {FieldsCategoryAddView, FieldsCategoryListView} from '../models/fieldsCategoryModel'
@Injectable({
  providedIn: 'root'
})
export class CustomFieldService {
  apiUrl: string = environment.apiURL;
  constructor(private http: HttpClient) { }
  getAllCustomField(obj: CustomFieldListViewModel) {
    let apiurl = this.apiUrl + obj._tenantName + "/CustomField/getAllCustomField";
    return this.http.post<CustomFieldListViewModel>(apiurl, obj)
  }
  deleteCustomField(obj: CustomFieldAddView) {
    let apiurl = this.apiUrl + obj._tenantName + "/CustomField/deleteCustomField";
    return this.http.post<CustomFieldAddView>(apiurl, obj)
  }
  updateCustomField(obj: CustomFieldAddView) {
    let apiurl = this.apiUrl + obj._tenantName + "/CustomField/updateCustomField";
    return this.http.put<CustomFieldAddView>(apiurl, obj)
  }
  addCustomField(obj: CustomFieldAddView) {
    let apiurl = this.apiUrl + obj._tenantName + "/CustomField/addCustomField";
    return this.http.post<CustomFieldAddView>(apiurl, obj)
  }

  addFieldsCategory(obj:FieldsCategoryAddView){
    let apiurl=this.apiUrl+obj._tenantName+"/CustomField/addFieldsCategory" ;
    return this.http.post<FieldsCategoryAddView>(apiurl,obj);
  }
  updateFieldsCategory(obj:FieldsCategoryAddView){
    let apiurl=this.apiUrl+obj._tenantName+"/CustomField/updateFieldsCategory" ;
    return this.http.put<FieldsCategoryAddView>(apiurl,obj);
  }
  deleteFieldsCategory(obj:FieldsCategoryAddView){
    let apiurl=this.apiUrl+obj._tenantName+"/CustomField/deleteFieldsCategory" ;
    return this.http.post<FieldsCategoryAddView>(apiurl,obj);
  }
  getAllFieldsCategory(obj:FieldsCategoryListView){
    let apiurl=this.apiUrl+obj._tenantName+"/CustomField/getAllFieldsCategory" ;
    return this.http.post<FieldsCategoryListView>(apiurl,obj);
  }
  updateCustomFieldSortOrder(obj:CustomFieldDragDropModel){
    let apiurl=this.apiUrl+obj._tenantName+"/CustomField/updateCustomFieldSortOrder" ;
    return this.http.put<CustomFieldDragDropModel>(apiurl,obj);
  }
  
}
