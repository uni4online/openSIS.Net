import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { GetAllSectionModel ,SectionAddModel} from 'src/app/models/sectionModel';

@Injectable({
  providedIn: 'root'
})
export class SectionService {

  apiUrl:string = environment.apiURL;
  constructor(private http: HttpClient) { }

  GetAllSection(obj: GetAllSectionModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Section/getAllSection";   
    return this.http.post<GetAllSectionModel>(apiurl,obj)
  }
  SaveSection(obj: SectionAddModel){
   
    let apiurl = this.apiUrl + obj._tenantName+ "/Section/addSection";   
    return this.http.post<SectionAddModel>(apiurl,obj)
  }
  UpdateSection(obj: SectionAddModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Section/updateSection";   
    return this.http.put<SectionAddModel>(apiurl,obj)
  }

  deleteSection(obj: SectionAddModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/Section/deleteSection";   
    return this.http.post<SectionAddModel>(apiurl,obj)
  }
 
}
