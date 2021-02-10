import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {CustomFieldAddView, CustomFieldDragDropModel, CustomFieldListViewModel} from '../models/customFieldModel';
import {FieldsCategoryAddView, FieldsCategoryListView} from '../models/fieldsCategoryModel'
import { BlockAddViewModel, BlockListViewModel, BlockPeriodAddViewModel, BlockPeriodSortOrderViewModel } from '../models/schoolPeriodModel';
@Injectable({
  providedIn: 'root'
})
export class SchoolPeriodService {
  apiUrl: string = environment.apiURL;
  constructor(private http: HttpClient) { }
  
  deleteBlockPeriod(obj: BlockPeriodAddViewModel) {
    let apiurl = this.apiUrl + obj._tenantName + "/Period/deleteBlockPeriod";
    return this.http.post<BlockPeriodAddViewModel>(apiurl, obj)
  }
  updateBlockPeriod(obj: BlockPeriodAddViewModel) {
    let apiurl = this.apiUrl + obj._tenantName + "/Period/updateBlockPeriod";
    return this.http.put<BlockPeriodAddViewModel>(apiurl, obj)
  }
  addBlockPeriod(obj: BlockPeriodAddViewModel) {
    let apiurl = this.apiUrl + obj._tenantName + "/Period/addBlockPeriod";
    return this.http.post<BlockPeriodAddViewModel>(apiurl, obj)
  }

  addBlock(obj:BlockAddViewModel){
    let apiurl=this.apiUrl+obj._tenantName+"/Period/addBlock" ;
    return this.http.post<BlockAddViewModel>(apiurl,obj);
  }
  updateBlock(obj:BlockAddViewModel){
    let apiurl=this.apiUrl+obj._tenantName+"/Period/updateBlock" ;
    return this.http.put<BlockAddViewModel>(apiurl,obj);
  }
  deleteBlock(obj:BlockAddViewModel){
    let apiurl=this.apiUrl+obj._tenantName+"/Period/deleteBlock" ;
    return this.http.post<BlockAddViewModel>(apiurl,obj);
  }
  getAllBlockList(obj:BlockListViewModel){
    let apiurl=this.apiUrl+obj._tenantName+"/Period/getAllBlockList" ;
    return this.http.post<BlockListViewModel>(apiurl,obj);
  }
  updateBlockPeriodSortOrder(obj:BlockPeriodSortOrderViewModel){
    let apiurl=this.apiUrl+obj._tenantName+"/Period/updateBlockPeriodSortOrder" ;
    return this.http.put<BlockPeriodSortOrderViewModel>(apiurl,obj);
  }
  
  private blockPeriodList: any;
  setBlockPeriodList(value: any) {
    this.blockPeriodList = value;
  }
  getBlockPeriodList() {
    return this.blockPeriodList;
  }

}
