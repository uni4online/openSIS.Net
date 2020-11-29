import { Injectable } from '@angular/core';
import * as _moment from 'moment';
import { default as _rollupMoment } from 'moment';
const moment =  _rollupMoment || _moment;
@Injectable({
    providedIn: 'root'
  })
export class SharedFunction {

    trimData(data){
        if(data !== undefined && typeof(data) == "string"){
            if(data !== null){
                return data.trim();
              }else{   
                return data;
            } 

        }
         
    }
    formatDateSaveWithoutTime(date){
      if (date === undefined || date === null) {
        return undefined;
      } else {
        let dt = moment(date).format('YYYY-MM-DD');      
        return dt;    
      }
    }

    formatDateSaveWithTime(date){
      if (date === undefined || date === null) {
        return undefined;
      } else {
        let dt = moment(date).format('YYYY-MM-DD hh:mm:ss tt');      
        return dt;    
      }
    }
    
    formatDate(date){      
        if(date !== "" && date !== null && date!== undefined && date !="-"){
          var formattedDate = date.split('T');  
          var formattedDateAfterConversion = moment(formattedDate[0]).format('MMM D, YYYY');
           return formattedDateAfterConversion;
        }else{
          return "-";
        }
        
      }
      formatDateInEditMode(date){       
       if(date !== "" && date !== null && date != "-"){
          var formattedDate =new Date(date);
          return moment(formattedDate).format('YYYY-MM-DD');
        }else{
          return null;
        }
        
      }
      serverToLocalDateAndTime(date){
        console.log(date);
        if(date !== "" && date !== null && date!== undefined && date !="-"){
          let localDate = new Date('date');
          console.log(localDate);
           return localDate;
        }else{
          return date;
        }
      }
    checkEmptyObject(data){
      if (data && (Object.keys(data).length !== 0 || Object.keys(data).length > 0) ){
        return true;
      }else{
        return false;
      }
    }
    
}
