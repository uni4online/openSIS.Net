import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
@Injectable({
  providedIn: 'root'
})
export class ValidationService {
  
  static emailValidator(control) {
    if(control.dirty && control.value !== '') {
      if (
        control.value.match(
          /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/
        )
      ) {
        
        return null;
      } else {
      
        return { invalidEmailAddress: true };
      }
    }
  }

  static phoneValidator(control) {
    if(control.value !== '') {
    if (
      control.value.match(
        ///^[0-9]{10}$/
        /^[0-9]{3}-[0-9]{3}-[0-9]{4}$/
      )
    ) {
      
      return null;
    } else {
     
      return { invalidPhoneNumber: true };
    }
  }
  }

  static websiteValidator(control) {
    if(control.dirty && control.value !== '') {
      if (control.value.match(/(https?:\/\/)?(www\.)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)|(https?:\/\/)?(www\.)?(?!ww)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/)) {
        
        return null;
      } else {
      
        return { invalidWebsite: true };
      }
    }
  }
  static dateComparisonValidator(from: string, to: string) {
   
    return (group: FormGroup): {[key: string]: any} => {
      let f = group.controls[from];
      let t = group.controls[to];
      if (f.value > t.value) 
      {    
        return { invalidDateComparison: true };
      }else{         
        return null;
      }
    }     
  }

  static compareValidation(firstValue,secondValue):boolean{
    
    if(firstValue>secondValue){
      return false
    }
    else{
      return true
    }
  }
  static defaultSelectionValidator(control) {
    if(control.dirty && control.value !== '') {
      if (control.value.match(/([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))/ )) {
        return null;
      }
      else if(control.value.match( /(Yes)/)){
        return null;
      }
      else if(control.value.match( /(No)/)){
        return null;
      }
       else {
        return { invalidDefaultSelection: true };
      }
    }
  }
  static noWhitespaceValidator(control) {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { 'whitespace': true };
  }
}
