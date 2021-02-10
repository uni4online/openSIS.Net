import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { MatAccordion } from '@angular/material/expansion';

@Component({
  selector: 'vex-search-student',
  templateUrl: './search-student.component.html',
  styleUrls: ['./search-student.component.scss']
})
export class SearchStudentComponent implements OnInit {

  @ViewChild(MatAccordion) accordion: MatAccordion;
  @Output() showHideAdvanceSearch = new EventEmitter<boolean>();

  constructor() { }

  ngOnInit(): void {
  }

  hideAdvanceSearch(){
    this.showHideAdvanceSearch.emit(false);
  }

}
