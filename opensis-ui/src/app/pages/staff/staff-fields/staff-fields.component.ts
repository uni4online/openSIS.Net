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
import { EditStaffFieldsComponent } from './edit-staff-fields/edit-staff-fields.component';
import { StaffFieldsCategoryComponent } from './staff-fields-category/staff-fields-category.component';

@Component({
  selector: 'vex-staff-fields',
  templateUrl: './staff-fields.component.html',
  styleUrls: ['./staff-fields.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class StaffFieldsComponent implements OnInit {
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
      {type: 'Default', field_name: 'First Name', sort_order: 1, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Last Name", sort_order: 2, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Email Address", sort_order: 3, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Mobile Phone", sort_order: 4, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Work Phone", sort_order: 5, field_type: 'TextBox', in_use: 'Yes'},
      {type: 'Default', field_name: "Home Phone", sort_order: 6, field_type: 'TextBox', in_use: 'Yes'}
    ]
  }

  ngOnInit(): void {
  }

   goToAdd(){   
    this.dialog.open(EditStaffFieldsComponent, {
      width: '600px'
    });
   }

   goToAddCategory(){   
    this.dialog.open(StaffFieldsCategoryComponent, {
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
