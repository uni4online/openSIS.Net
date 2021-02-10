import { Injectable } from '@angular/core';
import * as _moment from 'moment';
import { default as _rollupMoment } from 'moment';
const moment = _rollupMoment || _moment;
@Injectable({
  providedIn: 'root'
})
export class SharedFunction {

  trimData(data) {
    if (data !== undefined && typeof (data) == "string") {
      if (data !== null) {
        return data.trim();
      } else {
        return data;
      }

    }

  }
  formatDateSaveWithoutTime(date) {
    if (date === undefined || date === null) {
      return undefined;
    } else {
      let dt = moment(date).format('YYYY-MM-DD');
      return dt;
    }
  }

  formatDateSaveWithTime(date) {
    if (date === undefined || date === null) {
      return undefined;
    } else {
      let dt = moment(date).format('YYYY-MM-DD hh:mm:ss tt');
      return dt;
    }
  }

  autoGeneratePassword() {
    var chars = "abcdefghijklmnopqrstuvwxyz!@#$%^&*()-+<>ABCDEFGHIJKLMNOP1234567890";
    var pass = "";
    for (var x = 0; x < 8; x++) {
      var i = Math.random() * chars.length;
      pass += chars.charAt(i);
    }

    return pass;
  }

  formatDate(date) {
    if (date !== "" && date !== null && date !== undefined && date != "-") {
      var formattedDate = date.split('T');
      var formattedDateAfterConversion = moment(formattedDate[0]).format('MMM D, YYYY');
      return formattedDateAfterConversion;
    } else {
      return "-";
    }

  }
  formatDateInEditMode(date) {
    if (date !== "" && date !== null && date != "-") {
      var formattedDate = new Date(date);
      return moment(formattedDate).format('YYYY-MM-DD');
    } else {
      return null;
    }

  }
  serverToLocalDateAndTime(utcDate) {
    if (utcDate !== "" && utcDate !== null && utcDate !== undefined && utcDate != "-") {
      let localTime = moment.utc(utcDate).local().format('YYYY-MM-DD HH:mm:ss');
      return localTime;
    } else {
      return utcDate;
    }
  }

  transformDateWithTime(value: string): string {
    if(value!==null && value!==undefined){
      if(value.trim()!==""){       
        let getDateTime = this.serverToLocalDateAndTime(value);
        let formattedDateTime = moment(getDateTime, ["YYYY-MM-DD hh:mm:ss"]).format("DD-MM-YYYY"+" | "+"h:mm A");
        return formattedDateTime
      }else{
        return "-";
      }
    }
    else{
      return "-"
    }
  }

  checkEmptyObject(data) {
    if (data && (Object.keys(data).length !== 0 || Object.keys(data).length > 0)) {
      return true;
    } else {
      return false;
    }
  }

  getAge(birthDate) {
    if (birthDate !== null && birthDate != undefined) {
      //extract and collect only date from date-time string  
      var mdate = birthDate.toString();
      var dobYear = parseInt(mdate.substring(0, 4), 10);
      var dobMonth = parseInt(mdate.substring(5, 7), 10);
      var dobDate = parseInt(mdate.substring(8, 10), 10);

      //get the current date from system  
      var today = new Date();
      //date string after broking  
      var birthday = new Date(dobYear, dobMonth - 1, dobDate);

      //calculate the difference of dates  
      var diffInMillisecond = today.valueOf() - birthday.valueOf();

      //convert the difference in milliseconds and store in day and year variable          
      var yearAge = Math.floor(diffInMillisecond / 31536000000);
      var dayAge = Math.floor((diffInMillisecond % 31536000000) / 86400000);



      var monthAge = Math.floor(dayAge / 30);
      var dayAgedayAge = dayAge % 30;

      var tMnt = (monthAge + (yearAge * 12));
      var tDays = (tMnt * 30) + dayAge;


      var age = yearAge + " years " + monthAge + " months ";

      return age;

    }

  }

}
