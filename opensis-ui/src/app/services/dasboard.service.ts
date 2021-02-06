import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { DashboardViewModel } from '../models/dashboardModel';
import { Observable,BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DasboardService {

  apiUrl:string = environment.apiURL;
  private dashboardSubject = new BehaviorSubject(false);;
  constructor(private http: HttpClient) { }

  getDashboardView(obj: DashboardViewModel) {
    let apiurl = this.apiUrl + obj._tenantName + "/Common/getDashboardView";
    return this.http.post<DashboardViewModel>(apiurl, obj)
  }


  sendPageLoadEvent(event) {
    this.dashboardSubject.next(event);
  }
  getPageLoadEvent(): Observable<any> {
    return this.dashboardSubject.asObservable();
  }
}