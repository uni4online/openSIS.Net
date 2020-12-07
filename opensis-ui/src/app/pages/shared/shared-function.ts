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

    autoGeneratePassword() {
      var chars = "abcdefghijklmnopqrstuvwxyz!@#$%^&*()-+<>ABCDEFGHIJKLMNOP1234567890";
      var pass= "";
      for (var x = 0; x < 8; x++) {
        var i = Math.random() * chars.length;
        pass += chars.charAt(i);
      }
      
      return pass;
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
      serverToLocalDateAndTime(utcDate){
        if(utcDate !== "" && utcDate !== null && utcDate!== undefined && utcDate !="-"){
        let localTime=moment.utc(utcDate).local().format('YYYY-MM-DD HH:mm:ss');
        return localTime;
        }else{
        return utcDate;
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
