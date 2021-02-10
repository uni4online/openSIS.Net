import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CheckUserEmailAddressViewModel, UserViewModel } from '../models/userModel';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CryptoService } from '../services/Crypto.service';
import { LanguageModel } from '../models/languageModel';
@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiUrl: string = environment.apiURL;
  constructor(private http: HttpClient,
    public jwtHelper: JwtHelperService,
    private cryptoService: CryptoService) { }

  getAllLanguage(obj: LanguageModel) {
    let apiurl = this.apiUrl + obj._tenantName + "/Common/getAllLanguage";
    return this.http.post<LanguageModel>(apiurl, obj)
  }

  getAllLanguageForLogin(obj: LanguageModel) {
    let apiurl = this.apiUrl + obj._tenantName + "/Common/getAllLanguageForLogin";
    return this.http.post<LanguageModel>(apiurl, obj)
  }

  ValidateLogin(obj: UserViewModel) {
    obj.password = this.cryptoService.encrypt(obj.password);
    let apiurl = this.apiUrl + obj._tenantName + "/User/ValidateLogin";
    return this.http.post<UserViewModel>(apiurl, obj)
  }

  public isAuthenticated(): boolean {
    const token = sessionStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  checkUserLoginEmail(obj : CheckUserEmailAddressViewModel){
    let apiurl = this.apiUrl + obj._tenantName + "/User/checkUserLoginEmail";
    return this.http.post<CheckUserEmailAddressViewModel>(apiurl, obj)
  }

}
