import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { SchoolViewModel,SchoolListViewModel } from '../models/schoolModel';
@Injectable({
  providedIn: 'root'
})
export class SchoolService {

  apiUrl:string = environment.apiURL;
  constructor(private http: HttpClient) { }

  GetSchool(obj: SchoolViewModel){
    let apiurl = this.apiUrl + obj._tenantName+ "/School/getAllSchools";
    console.log(apiurl);
    return this.http.post<SchoolListViewModel>(apiurl,obj).pipe(
      catchError(this.handleError<SchoolListViewModel>('GetSchool'))
    );
  }
  

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    //this.messageService.add(`HeroService: ${message}`);
  }

}
