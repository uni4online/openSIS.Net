import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, of, Subject } from 'rxjs';
import { GetAllMembersList, GetAllMembers } from '../models/membershipNameModel';
import { NoticeAddViewModel, NoticeListViewModel } from '../models/noticeModel';
import { NoticeDeleteModel } from '../models/noticeDeleteModel';
@Injectable({
  providedIn: 'root'
})
export class NoticeService {

  apiUrl: string = environment.apiURL;
  constructor(private http: HttpClient) { }

  getAllMembers(obj: GetAllMembersList) {
    let apiurl = this.apiUrl + obj._tenantName + "/Membership/getAllMembers";
    return this.http.post<GetAllMembersList>(apiurl, obj)
  }
  saveNotice(notice: NoticeAddViewModel) {
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
  deleteNoticeById(notice: NoticeDeleteModel) {
    let apiurl = this.apiUrl + notice._tenantName + "/Notice/deleteNotice";
    return this.http.post<NoticeDeleteModel>(apiurl, notice)
  }
  editNoticeById(notice: NoticeAddViewModel) {
    let apiurl = this.apiUrl + notice._tenantName + "/Notice/viewNotice";
    return this.http.post<NoticeAddViewModel>(apiurl, notice)
  }
}
