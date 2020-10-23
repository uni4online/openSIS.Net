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
import { EditSchoolFieldsComponent } from './edit-school-fields/edit-school-fields.component';
import { SchoolFieldsCategoryComponent } from './school-fields-category/school-fields-category.component';

@Component({
  selector: 'vex-school-fields',
  templateUrl: './school-fields.component.html',
  styleUrls: ['./school-fields.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class SchoolFieldsComponent implements OnInit {
  @Input()
  columns = [
    { label: '', property: 'type', type: 'text', visible: true },
    { label: 'Field Name', property: 'field_name', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sort_order', type: 'number', visible: true },
    { label: 'Field Type', property: 'field_type', type: 'text', visible: true },
    { label: 'In Use', property: 'in_use', type: 'checkbox', visible: true },
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
      {type: 'Default', field_name: 'School NameP', sort_order: 1, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "School No./Code", sort_order: 2, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Address", sort_order: 3, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "City", sort_order: 4, field_type: 'TextBox', in_use: 'No'},
      {type: 'Default', field_name: "State", sort_order: 4, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Zip/Postal Code", sort_order: 4, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Country", sort_order: 4, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Principal", sort_order: 4, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Email", sort_order: 4, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Website", sort_order: 4, field_type: 'TextBox', in_use: 'No'},
      {type: 'Default', field_name: "Phone", sort_order: 4, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Custom', field_name: "Custom Field 1", sort_order: 4, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Custom', field_name: "Custom Field 2", sort_order: 4, field_type: 'TextBox', in_use: 'No'}
    ]
  }

  ngOnInit(): void {
  }

   goToAdd(){   
    this.dialog.open(EditSchoolFieldsComponent, {
      width: '600px'
    });
   }

   goToAddCategory(){   
    this.dialog.open(SchoolFieldsCategoryComponent, {
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
