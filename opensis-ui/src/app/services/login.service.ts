import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { UserViewModel } from '../models/userModel';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CryptoService } from '../services/Crypto.service';
@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiUrl: string = environment.apiURL;
  constructor(private http: HttpClient,
    public jwtHelper: JwtHelperService,
    private cryptoService: CryptoService) { }


  ValidateLogin(obj: UserViewModel) {
    obj.password = this.cryptoService.encrypt(obj.password);
    let apiurl = this.apiUrl + obj._tenantName + "/User/ValidateLogin";
    return this.http.post<UserViewModel>(apiurl, obj)
  }

  public isAuthenticated(): boolean {
    const token = sessionStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
