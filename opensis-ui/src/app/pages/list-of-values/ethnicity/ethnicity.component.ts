import { Component, OnInit, Input, ViewChild } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import icImpersonate from '@iconify/icons-ic/twotone-account-circle';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import { EditEthnicityComponent } from './edit-ethnicity/edit-ethnicity.component';
import { LovAddView, LovList } from '../../../models/lovModel';
import { LoaderService } from '../../../services/loader.service';
import { CommonService } from '../../../services/common.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ConfirmDialogComponent } from '../../shared-module/confirm-dialog/confirm-dialog.component';
import { ExcelService } from '../../../services/excel.service';
import { SharedFunction } from '../../shared/shared-function';

@Component({
  selector: 'vex-ethnicity',
  templateUrl: './ethnicity.component.html',
  styleUrls: ['./ethnicity.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class EthnicityComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Title', property: 'lovColumnValue', type: 'text', visible: true },
    { label: 'Created By', property: 'createdBy', type: 'text', visible: true },
    { label: 'Create Date', property: 'createdOn', type: 'text', visible: true },
    { label: 'Updated By', property: 'updatedBy', type: 'text', visible: true },
    { label: 'Update Date', property: 'updatedOn', type: 'text', visible: true },
    { label: 'Actions', property: 'actions', type: 'text', visible: true }
  ];

  ethnicityListViewModel: LovList = new LovList()
  lovAddView:LovAddView= new LovAddView();
  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icImpersonate = icImpersonate;
  icFilterList = icFilterList;
  ethnicityForExcel =[];
  loading: Boolean;
  listCount;
  searchKey: string;
  ethnicityListModel: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator
  @ViewChild(MatSort) sort: MatSort;


  constructor(private router: Router, private dialog: MatDialog,
    public translateService: TranslateService,
    private loaderService: LoaderService,
    private snackbar: MatSnackBar,
    private commonService: CommonService,
    private excelService:ExcelService,
    public commonfunction:SharedFunction) {
    translateService.use('en');

    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    });
  }


  ngOnInit(): void {
    this.getAllEthnicity();
  }

  getAllEthnicity() {
    this.ethnicityListViewModel.lovName = "Ethnicity";
    this.commonService.getAllDropdownValues(this.ethnicityListViewModel).subscribe(
      (res: LovList) => {
        if (typeof (res) == 'undefined') {
          this.snackbar.open('Ethnicity List failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else {
          if (res._failure) {
            this.ethnicityListModel=new MatTableDataSource(res.dropdownList) ;
            this.listCount=this.ethnicityListModel.data;
            this.snackbar.open('' + res._message, '', {
              duration: 10000
            });
          }
          else {
            this.ethnicityListModel = new MatTableDataSource(res.dropdownList);
            this.ethnicityForExcel = res.dropdownList;
            this.listCount=this.ethnicityListModel.data;
            this.ethnicityListModel.sort = this.sort;
          }
        }
      })
  }
  exportEthnicityListToExcel(){
    if(this.ethnicityForExcel.length!=0){
      let ethnicity=this.ethnicityForExcel?.map((item)=>{
        return{
          Title: item.lovColumnValue,
          CreatedBy: item.createdBy!==null ? item.createdBy: '-',
          CreateDate: this.commonfunction.transformDateWithTime(item.createdOn),
          UpdatedBy: item.updatedBy!==null ? item.updatedBy: '-',
          UpdateDate:  this.commonfunction.transformDateWithTime(item.updatedOn)
        }
      });
      this.excelService.exportAsExcelFile(ethnicity,'Ethnicity_List_')
     }
     else{
    this.snackbar.open('No Records Found. Failed to Export Ethnicity List','', {
      duration: 5000
    });
  }
}

  onSearchClear() {
    this.searchKey = "";
    this.applyFilter();
  }

  applyFilter() {
    this.ethnicityListModel.filter = this.searchKey.trim().toLowerCase()
  }

  deleteEthnicityData(element){
    this.lovAddView.dropdownValue=element
    this.commonService.deleteDropdownValue(this.lovAddView).subscribe(
      (res:LovAddView)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open('Ethnicity Deletion failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (res._failure) {
            this.snackbar.open('Ethnicity Deletion failed. ' + res._message, '', {
              duration: 10000
            });
          } 
          else { 
            this.snackbar.open('Ethnicity Deleted Successfully. ' + res._message, '', {
              duration: 10000
            });
            this.getAllEthnicity()
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
          message: "You are about to delete "+element.lovColumnValue+"."}
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
      if(dialogResult){
        this.deleteEthnicityData(element);
      }
   });
  }

  getPageEvent(event) {
    // this.getAllSchool.pageNumber=event.pageIndex+1;
    // this.getAllSchool.pageSize=event.pageSize;
    // this.callAllSchool(this.getAllSchool);
  }

  goToAdd() {
    this.dialog.open(EditEthnicityComponent, {
      data: null,
      width: '500px'
    }).afterClosed().subscribe(data => {
      if (data === 'submited') {
        this.getAllEthnicity();
      }
    });
  }

  openEditdata(element) {
    this.dialog.open(EditEthnicityComponent, {
      data: element,
      width: '500px'
    }).afterClosed().subscribe((data) => {
      if (data === 'submited') {
        this.getAllEthnicity();
      }
    })
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
