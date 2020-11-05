import { Component, OnInit, Input, ViewChild } from '@angular/core';
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
import { CustomFieldService } from '../../../services/custom-field.service';
import {CustomFieldAddView, CustomFieldListViewModel} from '../../../models/customFieldModel';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ConfirmDialogComponent } from '../../shared-module/confirm-dialog/confirm-dialog.component';
import { LoaderService } from '../../../services/loader.service';

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
  
  columns = [
    /* { label: '', property: 'type', type: 'text', visible: true }, */
    { label: 'Field Name', property: 'field_name', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sortOrder', type: 'number', visible: false },
    { label: 'Field Type', property: 'field_type', type: 'text', visible: true },
    { label: 'Select Options', property: 'selectOptions', type: 'text', visible: true },
    { label: 'Required', property: 'required', type: 'checkbox', visible: false },
    { label: 'In Used', property: 'inUsed', type: 'checkbox', visible: true },
    { label: 'System Field', property: 'systemField', type: 'checkbox', visible: false },
    { label: 'Action', property: 'action', type: 'text', visible: true }
  ];

  EnrollmentCodeModelList;

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;
  loading:boolean;
  customFieldListViewModel:CustomFieldListViewModel= new CustomFieldListViewModel();
  customFieldAddView:CustomFieldAddView= new CustomFieldAddView()

  constructor(
    private snackbar: MatSnackBar,
    private dialog: MatDialog,
    public translateService:TranslateService,
    private customFieldservice:CustomFieldService,
    private loaderService:LoaderService
    ) {
    translateService.use('en');
    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    }); 
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
  customFieldList: MatTableDataSource<any>;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit(): void {
    this.getAllCustomField()
  }

   goToAdd(){   
    this.dialog.open(EditSchoolFieldsComponent, {
      width: '600px'
    }).afterClosed().subscribe((data)=>{
      if(data==='submited'){
        this.getAllCustomField();
      }
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
  getAllCustomField(){
    
    this.customFieldservice.getAllCustomField(this.customFieldListViewModel).subscribe(
      (res:CustomFieldListViewModel)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open('Custom Field list failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (res._failure) {     
            this.snackbar.open('Custom Field list failed. ' + res._message, 'LOL THANKS', {
              duration: 10000
            });
          } 
          else { 
            this.customFieldList=new MatTableDataSource(res.customFieldsList) ;
           // res.customFieldsList[0].sortOrder
            this.customFieldList.sort=this.sort;     
          }
        }
      }
    );
  }
  deleteRoomdata(element){
    this.customFieldAddView.customFields=element
    this.customFieldservice.deleteCustomField(this.customFieldAddView).subscribe(
      (res:CustomFieldAddView)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open('Custom Field failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (res._failure) {
            this.snackbar.open('Custom Field failed. ' + res._message, 'LOL THANKS', {
              duration: 10000
            });
          } 
          else { 
            this.getAllCustomField()
          }
        }
      }
    )
  }
  confirmDelete(element){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Are you sure?",
          message: "You are about to delete "+element.title+"."}
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
      if(dialogResult){
        this.deleteRoomdata(element);
      }
   });
}
openEditdata(element){
  this.dialog.open(EditSchoolFieldsComponent, {
    data: element,
      width: '800px'
  }).afterClosed().subscribe((data)=>{
    if(data==='submited'){
      this.getAllCustomField();
    }
  })
}

}
