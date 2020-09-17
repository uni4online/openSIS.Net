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
    if(control.dirty && control.value !== '') {
    if (
      control.value.match(
        /^[0-9]{10}$/
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
      if (f.value > t.value) {    
        return { invalidDateComparison: true };
        }else{         
          return null;
        }
      }     
    }
  
  
}
