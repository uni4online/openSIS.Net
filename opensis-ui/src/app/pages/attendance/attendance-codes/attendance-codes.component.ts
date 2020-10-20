import { Component, OnInit,Input } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router} from '@angular/router';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import { MatSelectChange } from '@angular/material/select';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { LoaderService } from '../../../services/loader.service';
import { MatTableDataSource } from '@angular/material/table';
import { EditAttendanceCodeComponent } from '../attendance-codes/edit-attendance-code/edit-attendance-code.component';
import { AttendanceCategoryComponent } from '../attendance-codes/attendance-category/attendance-category.component';

@Component({
  selector: 'vex-attendance-codes',
  templateUrl: './attendance-codes.component.html',
  styleUrls: ['./attendance-codes.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class AttendanceCodesComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Title', property: 'title', type: 'text', visible: true },
    { label: 'Short Name', property: 'short_name', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sort_order', type: 'text', visible: true },
    { label: 'Type', property: 'type', type: 'text', visible: true },
    { label: 'Default for Teacher & Office', property: 'default_for_teacher_and_office', type: 'text', visible: false },
    { label: 'State Code', property: 'state_code', type: 'text', visible: false },
    { label: 'action', property: 'action', type: 'text', visible: true }
  ];

  EnrollmentCodeModelList;

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;
  loading:Boolean;

  constructor(private router: Router,private dialog: MatDialog,public translateService:TranslateService) {
    translateService.use('en');
    this.EnrollmentCodeModelList = [
      {title: 'Present', short_name: 'P', sort_order: 1, type: 'Teacher & Office', default_for_teacher_and_office: 'Yes', state_code: 'Present'},
      {title: 'Absent', short_name: "A", sort_order: 2, type: 'Teacher & Office', default_for_teacher_and_office: 'No', state_code: 'Absent'},
      {title: 'Tardy', short_name: "T", sort_order: 3, type: 'Teacher & Office', default_for_teacher_and_office: 'No', state_code: 'Present'},
      {title: 'Late', short_name: "L", sort_order: 4, type: 'Teacher & Office', default_for_teacher_and_office: 'No', state_code: 'Present'}
    ]
  }

  ngOnInit(): void {
  }

  goToAdd(){   
    this.dialog.open(EditAttendanceCodeComponent, {
      width: '600px'
    });
  }

  goToAddCategory(){   
    this.dialog.open(AttendanceCategoryComponent, {
      width: '500px'
    });
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
