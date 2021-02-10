import { Component, OnInit, ViewChild } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import { MatSort } from '@angular/material/sort';
import { LoaderService } from '../../../services/loader.service';
import { MatTableDataSource } from '@angular/material/table';
import { EditAttendanceCodeComponent } from '../attendance-codes/edit-attendance-code/edit-attendance-code.component';
import { AttendanceCategoryComponent } from '../attendance-codes/attendance-category/attendance-category.component';
import {MatTabGroup } from '@angular/material/tabs';
import { ConfirmDialogComponent } from '../../shared-module/confirm-dialog/confirm-dialog.component';
import { AttendanceCodeService } from '../../../services/attendance-code.service';
import { AttendanceCodeCategoryModel, AttendanceCodeModel, GetAllAttendanceCategoriesListModel, GetAllAttendanceCodeModel } from '../../../models/attendanceCodeModel';
import {AttendanceCodeEnum} from '../../../enums/attendance-code.enum';

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
  @ViewChild(MatTabGroup) tabGroup: MatTabGroup;
  
  @ViewChild(MatSort) sort: MatSort;
  isCategoryUpdated=false;
  isCategoryDeleted=false;
  searchKey: string;
  selectedAttendanceCategory = 1;
  attedanceStateCode=AttendanceCodeEnum;
  columns = [
    { label: 'Title', property: 'title', type: 'text', visible: true },
    { label: 'Short Name', property: 'shortName', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sortOrder', type: 'number', visible: true },
    { label: 'Allow Entry By', property: 'allowEntryBy', type: 'text', visible: true },
    { label: 'Default for Teacher & Office', property: 'defaultCode', type: 'text', visible: false },
    { label: 'State Code', property: 'stateCode', type: 'text', visible: false },
    { label: 'action', property: 'action', type: 'text', visible: true }
  ];

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;
  loading: boolean;
  editMode=false;
  attendanceCategoryModel:AttendanceCodeCategoryModel=new AttendanceCodeCategoryModel();
  getAllAttendanceCategoriesListModel:GetAllAttendanceCategoriesListModel=new GetAllAttendanceCategoriesListModel();
  attendanceCodeModel:AttendanceCodeModel=new AttendanceCodeModel();
  getAllAttendanceCodeModel:GetAllAttendanceCodeModel=new GetAllAttendanceCodeModel()
  attendanceCategories=[]
  attendanceCodeList:MatTableDataSource<GetAllAttendanceCodeModel>;
  constructor(private dialog: MatDialog,
    public translateService: TranslateService,
    private loaderService: LoaderService,
    private attendanceCodeService:AttendanceCodeService,
    private snackbar: MatSnackBar) {
    translateService.use('en');
    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    });
    
  }

  ngOnInit(): void {
    this.getAllAttendanceCategory();
  }

  onClick(id) {
    this.selectedAttendanceCategory = id;
    this.getAllAttendanceCode();
  }

  goToAddCategory() {
    this.dialog.open(AttendanceCategoryComponent, {
      width: '500px'
    }).afterClosed().subscribe((res) => {
      if(res){
        this.getAllAttendanceCategory();
      }
    });
  }

  openEditCategory(categoryDetails) {
    this.editMode=true;
    this.dialog.open(AttendanceCategoryComponent,{
      data:{
        editMode:this.editMode,
        categoryDetails:categoryDetails,
      },
      width:'600px'}).afterClosed().subscribe((res)=>{
        if(res){
        this.isCategoryUpdated=true;
          this.getAllAttendanceCategory();
        }
      });
  }

  openDeleteCategory(categoryDetails) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Are you sure?",
          message: "You are about to delete."}
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
      if(dialogResult){
        let index = this.attendanceCategories.findIndex((x) => {
          return x.attendanceCategoryId === categoryDetails.attendanceCategoryId
        });
        let shiftIndex:number;
        if(index==0){
          if(this.attendanceCategories.length>1){
            shiftIndex=index+1
          }else{
            shiftIndex=-1;
          }
        }else{
          shiftIndex=index-1;
        }
        
        this.isCategoryDeleted=true;
        this.deleteAttendanceCategory(categoryDetails,shiftIndex);
      }
   });
    }

    
  goToAddAttendanceCode() {
    this.dialog.open(EditAttendanceCodeComponent, {
      data:{
        editMode:false,
        attendanceCategoryId:this.selectedAttendanceCategory
      },width:'600px'
    }).afterClosed().subscribe((res)=>{
      if(res){
        this.getAllAttendanceCode();
      }
    });
  }

  openEditAttendance(attendanceCodeDetails){
    this.dialog.open(EditAttendanceCodeComponent, {
      data:{
        editMode:true,
        editDetails:attendanceCodeDetails
      },
      width:'600px'
    }).afterClosed().subscribe((res)=>{
      if(res){
        this.getAllAttendanceCategory();
      }
    });;
  }

  openDeleteAttendance(attendanceDetails){
  const dialogRef = this.dialog.open(ConfirmDialogComponent, {
    maxWidth: "400px",
    data: {
        title: "Are you sure?",
        message: "You are about to delete."}
  });
  dialogRef.afterClosed().subscribe(dialogResult => {
    if(dialogResult){
      this.deleteAttendanceCode(attendanceDetails);
    }
 });
  }

  // *********Attendance Category API Implementation(Mat Tab)*********

  // Get All Attendance Category
  getAllAttendanceCategory() {
    this.getAllAttendanceCategoriesListModel.schoolId=+sessionStorage.getItem("selectedSchoolId");
    this.attendanceCodeService.getAllAttendanceCodeCategories(this.getAllAttendanceCategoriesListModel).subscribe((res)=>{
      if(res._failure){
        if(res._message==="NO RECORD FOUND"){
          this.attendanceCategories=[];
          this.snackbar.open(res._message, '', {
            duration: 10000
          });
         
        } else{
          this.snackbar.open('Attendance Category List failed. ' + res._message, '', {
            duration: 10000
          });
        }
      }else{     
        this.attendanceCategories = res.attendanceCodeCategoriesList;
        if(this.isCategoryDeleted){
          this.tabGroup.selectedIndex=this.tabGroup.selectedIndex-1;
        }
        if(this.tabGroup!=undefined && !this.isCategoryUpdated && !this.isCategoryDeleted){
          this.tabGroup.selectedIndex=this.attendanceCategories.length-1;
          this.selectedAttendanceCategory=this.attendanceCategories[this.attendanceCategories.length-1]?.attendanceCategoryId;
          this.getAllAttendanceCode();
        }else if(this.attendanceCategories.length>0 && !this.isCategoryUpdated && !this.isCategoryDeleted){
          this.selectedAttendanceCategory=this.attendanceCategories[0]?.attendanceCategoryId;
          this.getAllAttendanceCode();
        } 
        this.isCategoryUpdated=false;
        this.isCategoryDeleted=false;
      }
    })
  }

  // Delete Attendance Category
  deleteAttendanceCategory(categoryDetails,indexNeedToBeSelectAfterDelete) {
    this.attendanceCategoryModel.attendanceCodeCategories.schoolId= +sessionStorage.getItem("selectedSchoolId");
    this.attendanceCategoryModel.attendanceCodeCategories.attendanceCategoryId=categoryDetails.attendanceCategoryId;
    this.attendanceCategoryModel.attendanceCodeCategories.academicYear=categoryDetails.academicYear;
    this.attendanceCodeService.deleteAttendanceCodeCategories(this.attendanceCategoryModel).subscribe((res)=>{
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Attendance Category is Failed to Delete!. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }else if (res._failure) {
        this.snackbar.open(res._message, '', {
          duration: 10000
        });
      } else {
        this.snackbar.open('Attendance Category is Deleted!', '', {
          duration: 10000
        });
        if(indexNeedToBeSelectAfterDelete!=-1){
          this.selectedAttendanceCategory=this.attendanceCategories[indexNeedToBeSelectAfterDelete].attendanceCategoryId;
        }
        this.getAllAttendanceCategory();
      }
    });
  }

  // ********* Attendance Code API Implementation(Table) **********

  // Get All Attendance Codes
  getAllAttendanceCode() {
  this.getAllAttendanceCodeModel.attendanceCategoryId=this.selectedAttendanceCategory;
  this.getAllAttendanceCodeModel.schoolId=+sessionStorage.getItem("selectedSchoolId");
  this.attendanceCodeService.getAllAttendanceCode(this.getAllAttendanceCodeModel).subscribe((res)=>{
    if(res._failure){
      if(res._message==="NO RECORD FOUND"){
        this.attendanceCodeList = new MatTableDataSource([]);
        this.attendanceCodeList.sort = this.sort;       
      } else{
        this.snackbar.open('Attendance Codes failed. ' + res._message, '', {
          duration: 10000
        });
      }
    }else{
      this.attendanceCodeList = new MatTableDataSource(res.attendanceCodeList);
      this.attendanceCodeList.sort = this.sort;
    }
   
  })
  }

  // Delete Attendance Code
  deleteAttendanceCode(attendanceDetails) {
    this.attendanceCodeModel.attendanceCode.schoolId=attendanceDetails.schoolId;
    this.attendanceCodeModel.attendanceCode.attendanceCategoryId=attendanceDetails.attendanceCategoryId;
    this.attendanceCodeModel.attendanceCode.attendanceCode1=attendanceDetails.attendanceCode1;

    this.attendanceCodeService.deleteAttendanceCode(this.attendanceCodeModel).subscribe((res)=>{
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Attendance Code is Failed to Delete!. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }else if (res._failure) {
        this.snackbar.open(res._message, '', {
          duration: 10000
        });
      } else {
        this.snackbar.open('Attendance Code is Deleted!', '', {
          duration: 10000
        });
        this.getAllAttendanceCode();
      }
    })
  }

  // Column Filter
  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

// Table Search
  onSearchClear(){
    this.searchKey="";
    this.applyFilter();
  }

  applyFilter(){
    this.attendanceCodeList.filter = this.searchKey.trim().toLowerCase()
  }

}
