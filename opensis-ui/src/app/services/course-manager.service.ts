import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { GetAllSubjectModel,AddSubjectModel} from '../models/courseManagerModel';
import { CryptoService } from './Crypto.service';
import { BehaviorSubject, Subject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class CourseManagerService {  
    apiUrl: string = environment.apiURL;
    constructor(private http: HttpClient, private cryptoService: CryptoService) { }

    GetAllSubjectList(courseManager: GetAllSubjectModel) {
        let apiurl = this.apiUrl + courseManager._tenantName + "/CourseManager/getAllSubjectList";
        return this.http.post<GetAllSubjectModel>(apiurl, courseManager)
    }
    AddSubject(courseManager: AddSubjectModel) {
        let apiurl = this.apiUrl + courseManager._tenantName + "/CourseManager/addSubject";
        return this.http.post<AddSubjectModel>(apiurl, courseManager)
    }
    UpdateSubject(courseManager: AddSubjectModel) {
        let apiurl = this.apiUrl + courseManager._tenantName + "/CourseManager/updateSubject";
        return this.http.put<AddSubjectModel>(apiurl, courseManager)
    }
    DeleteSubject(courseManager: AddSubjectModel) {
        let apiurl = this.apiUrl + courseManager._tenantName + "/CourseManager/deleteSubject";
        return this.http.post<AddSubjectModel>(apiurl, courseManager)
    }
}
