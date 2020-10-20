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
    checkEmptyObject(data){
      if (data && (Object.keys(data).length !== 0 || Object.keys(data).length > 0) ){
        return true;
      }else{
        return false;
      }
    }
    
}
