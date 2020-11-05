import { Component, OnInit, Input } from '@angular/core';
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
import { EditStudentFieldsComponent } from './edit-student-fields/edit-student-fields.component';
import { StudentFieldsCategoryComponent } from './student-fields-category/student-fields-category.component';

@Component({
  selector: 'vex-student-fields',
  templateUrl: './student-fields.component.html',
  styleUrls: ['./student-fields.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class StudentFieldsComponent implements OnInit {
  @Input()
  columns = [
    { label: '', property: 'type', type: 'text', visible: true },
    { label: 'Field Name', property: 'field_name', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sort_order', type: 'number', visible: true },
    { label: 'Field Type', property: 'field_type', type: 'text', visible: true },
    { label: 'In Use', property: 'in_use', type: 'checkbox', visible: true },
    { label: 'action', property: 'action', type: 'text', visible: true }
  ];

  StudentFieldsModelList;

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;
  loading:Boolean;

  constructor(private router: Router,private dialog: MatDialog,public translateService:TranslateService) {
    translateService.use('en');
    this.StudentFieldsModelList = [
      {type: 'Default', field_name: 'Salutation', sort_order: 1, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Default', field_name: "Suffix", sort_order: 2, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Default', field_name: "First/Given Name", sort_order: 3, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Middle Name", sort_order: 4, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Last/Family Name", sort_order: 5, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Preferred/Common Name", sort_order: 6, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Previous/Maiden Name", sort_order: 7, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Student ID", sort_order: 8, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "ALternate ID", sort_order: 9, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "District ID", sort_order: 10, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "State ID", sort_order: 11, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Admission Number", sort_order: 12, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Roll Number", sort_order: 13, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Social Security Number", sort_order: 14, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Other Govt. Issued No.", sort_order: 15, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Date of Birth", sort_order: 16, field_type: 'Datepicker', in_use: 'Yes'},
      {type: 'Default', field_name: "Gender", sort_order: 17, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Default', field_name: "Race", sort_order: 18, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Default', field_name: "Ethnicity", sort_order: 19, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Default', field_name: "Marital Status", sort_order: 20, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Default', field_name: "Country of Birth", sort_order: 21, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Default', field_name: "Nationality", sort_order: 22, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Default', field_name: "First Language", sort_order: 23, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Default', field_name: "Second Language", sort_order: 24, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Default', field_name: "Third Language", sort_order: 25, field_type: 'Dropdown', in_use: 'Yes'},
      {type: 'Custom', field_name: "Custom Field", sort_order: 26, field_type: 'TextBox', in_use: 'Yes'}
    ]
  }

  ngOnInit(): void {
  }

   goToAdd(){   
    this.dialog.open(EditStudentFieldsComponent, {
      width: '600px'
    });
   }

   goToAddCategory(){   
    this.dialog.open(StudentFieldsCategoryComponent, {
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
