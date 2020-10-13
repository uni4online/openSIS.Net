import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { NoticeAddViewModel, NoticeListViewModel } from '../models/noticeModel';
import { NoticeDeleteModel } from '../models/noticeDeleteModel';

@Injectable({
  providedIn: 'root'
})
export class NoticeService {
  private noticeSource = new BehaviorSubject(false);
  currentNotice = this.noticeSource.asObservable();

  apiUrl: string = environment.apiURL;
  constructor(private http: HttpClient) { }

  addNotice(notice: NoticeAddViewModel) {
    let apiurl = this.apiUrl + notice._tenantName + "/Notice/addNotice";
    return this.http.post<NoticeAddViewModel>(apiurl, notice)
  }
  updateNotice(notice: NoticeAddViewModel) {
    let apiurl = this.apiUrl + notice._tenantName + "/Notice/updateNotice";
    return this.http.post<NoticeAddViewModel>(apiurl, notice)
  }
  getAllNotice(notice: NoticeListViewModel) {
    let apiurl = this.apiUrl + notice._tenantName + "/Notice/getAllNotice";
    return this.http.post<NoticeListViewModel>(apiurl, notice)
  }
  deleteNotice(notice: NoticeDeleteModel) {
    let apiurl = this.apiUrl + notice._tenantName + "/Notice/deleteNotice";
    return this.http.post<NoticeDeleteModel>(apiurl, notice)
  }
  viewNotice(notice: NoticeAddViewModel) {
    let apiurl = this.apiUrl + notice._tenantName + "/Notice/viewNotice";
    return this.http.post<NoticeAddViewModel>(apiurl, notice)
  }
  changeNotice(message: boolean) {
    this.noticeSource.next(message)
  } 
}
