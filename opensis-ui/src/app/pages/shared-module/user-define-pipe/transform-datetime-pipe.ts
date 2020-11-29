import { Pipe, PipeTransform } from '@angular/core';
import * as _moment from 'moment';
import { default as _rollupMoment } from 'moment';
const moment =  _rollupMoment || _moment;
@Pipe({
  name: 'transformDateTimePipe'
})
export class TransformDateTimePipe implements PipeTransform {

  transform(value: string): string {
    if(value!==null && value!==undefined){
      if(value.trim()!==""){
        let dateTimeArr = value.split('T');
        let formattedDate =  moment(dateTimeArr[0]).format('MMM D, YYYY');
        let formattedtime = moment(dateTimeArr[1], ["hh:mm:ss"]).format("h:mm A");
        let formattedDateTime = formattedDate+' '+formattedtime;
        return formattedDateTime
      }else{
        return "-";
      }
    }
    else{
      return "-"
    }
  }

}
