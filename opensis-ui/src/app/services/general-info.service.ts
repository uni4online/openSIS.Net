import { Injectable } from '@angular/core';
import { GeneralInfoModel } from '../models/generalInfoModel';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import {Observable, of } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class GeneralInfoService {
  apiUrl:string = environment.apiURL;
  constructor(private http: HttpClient) { }

  SaveGeneralInfo(obj: GeneralInfoModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/User/ValidateLogin";    
    return this.http.post<GeneralInfoModel>(apiurl,obj)
    .pipe(
      catchError(this.handleError<GeneralInfoModel>('SaveGeneralInfo'))      
    );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {        
      sessionStorage.setItem("httpError",error);     
      return of(result as T);
     
    };
  }
}
