import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'EmtyValueCheckPipe'
})
export class EmtyValueCheckPipe implements PipeTransform {

  transform(value: string): string {
    if(value!==null && value!==undefined){
      if(value.trim()!==""){
        return value.trim();
      }else{
        return "-";
      }
    }
    else{
      return "-"
    }
  }

}
