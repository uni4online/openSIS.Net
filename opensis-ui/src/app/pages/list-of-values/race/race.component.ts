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
import { EditRaceComponent } from './edit-race/edit-race.component';
import { ConfirmDialogComponent } from '../../shared-module/confirm-dialog/confirm-dialog.component';
import { LovAddView, LovList } from '../../../models/lovModel';
import { LoaderService } from '../../../services/loader.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { CommonService } from '../../../services/common.service';


@Component({
  selector: 'vex-race',
  templateUrl: './race.component.html',
  styleUrls: ['./race.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class RaceComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Title', property: 'lovColumnValue', type: 'text', visible: true },
    { label: 'Created By', property: 'createdBy', type: 'text', visible: true },
    { label: 'Create Date', property: 'createdOn', type: 'text', visible: true },
    { label: 'Updated By', property: 'updatedBy', type: 'text', visible: true },
    { label: 'Update Date', property: 'updatedOn', type: 'text', visible: true },
    { label: 'Actions', property: 'actions', type: 'text', visible: true }
  ];
 
  raceListViewModel: LovList = new LovList();
  lovAddView:LovAddView= new LovAddView();
  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icImpersonate = icImpersonate;
  icFilterList = icFilterList;
  loading: Boolean;
  searchKey: string;
  listCount;
  raceListModel: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator
  @ViewChild(MatSort) sort: MatSort;


  constructor(private router: Router, private dialog: MatDialog,
    public translateService: TranslateService,
    private loaderService: LoaderService,
    private snackbar: MatSnackBar,
    private commonService: CommonService) {
    translateService.use('en');

    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    });
  }


  ngOnInit(): void {
    this.getAllRace()
  }


  getPageEvent(event) {
    // this.getAllSchool.pageNumber=event.pageIndex+1;
    // this.getAllSchool.pageSize=event.pageSize;
    // this.callAllSchool(this.getAllSchool);
  }

  openAddNew() {
    this.dialog.open(EditRaceComponent, {
      data: null,
      width: '500px'
    }).afterClosed().subscribe(data => {
      if (data === 'submited') {
        this.getAllRace();
      }
    });
  }

  openEditdata(element) {
    this.dialog.open(EditRaceComponent, {
      data: element,
      width: '500px'
    }).afterClosed().subscribe((data) => {
      if (data === 'submited') {
        this.getAllRace();
      }
    })
  }

  getAllRace() {
    this.raceListViewModel.lovName = "Race";
    this.commonService.getAllDropdownValues(this.raceListViewModel).subscribe(
      (res: LovList) => {
        if (typeof (res) == 'undefined') {
          this.snackbar.open('Race List failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else {
          if (res._failure) {
            this.raceListModel=new MatTableDataSource(res.dropdownList) ;
            this.listCount=this.raceListModel.data;
            this.snackbar.open('Race List failed. ' + res._message, '', {
              duration: 10000
            });
          }
          else {
            this.raceListModel = new MatTableDataSource(res.dropdownList);
            this.listCount=this.raceListModel.data;
            this.raceListModel.sort = this.sort;
          }
        }
      })
  }

  onSearchClear(){
    this.searchKey="";
    this.applyFilter();
  }

  applyFilter(){
    this.raceListModel.filter = this.searchKey.trim().toLowerCase()
  }

  deleteRaceData(element){
    this.lovAddView.dropdownValue=element
    this.commonService.deleteDropdownValue(this.lovAddView).subscribe(
      (res:LovAddView)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open('Race Deletion failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (res._failure) {
            this.snackbar.open('Race Deletion failed. ' + res._message, '', {
              duration: 10000
            });
          } 
          else { 
            this.snackbar.open('Race Deleted Successfully. ' + res._message, '', {
              duration: 10000
            });
            this.getAllRace()
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
        this.deleteRaceData(element);
      }
   });
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
