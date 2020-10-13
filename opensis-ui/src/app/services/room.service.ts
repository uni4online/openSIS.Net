import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { RoomAddView, RoomListViewModel } from '../models/roomModel';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  apiUrl:string = environment.apiURL;
  constructor(private http: HttpClient) { }

  addRoom(Obj:RoomAddView){
    let apiurl=this.apiUrl+Obj._tenantName+"/Room/addRoom";  
    return this.http.post<RoomAddView>(apiurl,Obj)
  }
  updateRoom(Obj:RoomAddView){
    let apiurl=this.apiUrl+Obj._tenantName+"/Room/updateRoom";
    return this.http.put<RoomAddView>(apiurl,Obj)
  }
  deleteRoom(Obj:RoomAddView){
    let apiurl=this.apiUrl+Obj._tenantName+"/Room/deleteRoom";
    return this.http.post<RoomAddView>(apiurl,Obj)
  }
  getAllRoom(obj:RoomListViewModel){
    let apiurl= this.apiUrl+obj._tenantName+"/Room/getAllRoom" ;
    return this.http.post<RoomListViewModel>(apiurl,obj)
  }
}
