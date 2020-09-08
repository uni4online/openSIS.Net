import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import {Observable, of } from 'rxjs';
import { UserViewModel } from '../models/userModel';
import { JwtHelperService } from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiUrl:string = environment.apiURL;   
  constructor(private http: HttpClient,public jwtHelper: JwtHelperService) { }

  
  ValidateLogin(obj: UserViewModel ){
    let apiurl = this.apiUrl + obj._tenantName+ "/User/ValidateLogin";
    return this.http.post<UserViewModel>(apiurl,obj)
  }

  private log(message: string) {
    //this.messageService.add(`HeroService: ${message}`);
  }
  
  public isAuthenticated(): boolean {
    const token = sessionStorage.getItem('token');   
    return !this.jwtHelper.isTokenExpired(token);
  }

}
