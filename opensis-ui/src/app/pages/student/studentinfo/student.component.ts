import { Component, OnInit, Input, Output,EventEmitter, ViewChild } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';

import { MatSnackBar } from '@angular/material/snack-bar';
import { Router} from '@angular/router';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { MatSelectChange } from '@angular/material/select';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { LoaderService } from '../../../services/loader.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'vex-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class StudentComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Name', property: 'student_Name', type: 'text', visible: true },
    { label: 'Student ID', property: 'student_ID', type: 'text', visible: true },
    { label: 'Alternate ID', property: 'alternate_ID', type: 'text', visible: true },
    { label: 'Grade', property: 'grade', type: 'text', visible: true },
    { label: 'Section', property: 'section', type: 'text', visible: true },
    { label: 'Phone', property: 'phone', type: 'text', visible: true }
  ];

  StudentModelList;

 
  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icSearch = icSearch;
  icFilterList = icFilterList;
  loading:Boolean;

  constructor(private router : Router) { 
    this.StudentModelList = [
      {student_Name: 'Anderson, Danny', student_ID: 1, alternate_ID: 466639635, grade: '9', Section: 'Section A', phone: '706-853-9164'},
      {student_Name: 'Aponte, Justin', student_ID: 2, alternate_ID: 332284656, grade: '10', Section: 'Section C', phone: '404-758-2922'},
      {student_Name: 'Davis, Julie', student_ID: 3, alternate_ID: 820463327, grade: '11', Section: 'Section B', phone: '585-534-4859'},
      {student_Name: 'Holmes, Javier', student_ID: 4, alternate_ID: 225394032, grade: '10', Section: 'Section B', phone: '678-347-7936'},
      {student_Name: 'Loafer, Roman', student_ID: 5, alternate_ID: 746807925, grade: '11', Section: 'Section A', phone: '470-555-3381'},
      {student_Name: 'Paiva, Laura', student_ID: 6, alternate_ID: 221861771, grade: '11', Section: 'Section C', phone: '770-314-6805'},
      {student_Name: 'Parker, Colin', student_ID: 7, alternate_ID: 487552403, grade: '11', Section: 'Section C', phone: '678-810-2209'}
    ]
  }

  ngOnInit(): void {
  }

  goToAdd(){   
    this.router.navigate(["school/students/student-generalinfo"]);
  }

  getPageEvent(event){    
    // this.getAllSchool.pageNumber=event.pageIndex+1;
    // this.getAllSchool.pageSize=event.pageSize;
    // this.callAllSchool(this.getAllSchool);
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }
  

}
