import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'EmtyBooleanCheckPipe'
})
export class EmtyBooleanCheckPipe implements PipeTransform {

  transform(value: boolean): string {
    if(value===false){
      return "No";
      
    }
    else if (value===true){
      return "Yes";
    }
    else if(value ===null){
      return "-";
    }
   
    else{
      return "-";
    }
  }

}
