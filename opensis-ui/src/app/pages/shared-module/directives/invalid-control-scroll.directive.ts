import {
    Directive,
    HostListener,
    ElementRef
  } from "@angular/core";
  import { FormGroupDirective } from "@angular/forms";
  
  @Directive({
    selector: "[appInvalidControlScroll]"
  })
  export class InvalidControlScrollDirective {
   
    constructor(
      private el: ElementRef,
      private formGroupDir: FormGroupDirective
    ) {}
  
    @HostListener("ngSubmit") onSubmit() {
      if (this.formGroupDir.control.invalid) {
        this.scrollToFirstInvalidControl();
      }
    }

    scrollToFirstInvalidControl() {
    const firstInvalidControl: HTMLElement = this.el.nativeElement.querySelector(
      'mat-select.ng-invalid, textarea.ng-invalid, input.ng-invalid'
    );
      firstInvalidControl.scrollIntoView({ behavior: 'smooth',block: 'center' });
  }
  }