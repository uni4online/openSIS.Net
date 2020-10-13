import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/twotone-search';
import icAdd from '@iconify/icons-ic/twotone-add';
import icFilterList from '@iconify/icons-ic/twotone-filter-list';
import { EditGradeLevelsComponent } from '../grade-levels/edit-grade-levels/edit-grade-levels.component';
import { TranslateService } from '@ngx-translate/core';
import { AddGradeLevelModel, GetAllGradeLevelsModel } from '../../../models/gradeLevelModel';
import { MatTableDataSource } from '@angular/material/table';
import { GradeLevelService } from '../../../services/grade-level.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { LoaderService } from '../../../services/loader.service';
import { ConfirmDialogComponent } from '../../shared-module/confirm-dialog/confirm-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'vex-grade-levels',
  templateUrl: './grade-levels.component.html',
  styleUrls: ['./grade-levels.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class GradeLevelsComponent implements OnInit {
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator; 
  @ViewChild(MatSort) sort:MatSort

  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icAdd = icAdd;
  icFilterList = icFilterList;
  getAllGradeLevels:GetAllGradeLevelsModel = new GetAllGradeLevelsModel();
  gradeLevelList:MatTableDataSource<GetAllGradeLevelsModel>;
  sendGradeLevelsToDialog:[];
  editMode:boolean;
  sendDetailsToEditComponent:[];
  loading:boolean=false;
  searchKey:string;
  deleteGradeLevel:AddGradeLevelModel=new AddGradeLevelModel();
  columns = [
    { label: 'Title', property: 'title', type: 'text', visible: true },
    { label: 'Short Name', property: 'shortName', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sortOrder', type: 'number', visible: true},
    { label: 'Next Grade', property: 'nextGrade', type: 'text', visible: true},
    { label: 'Action', property: 'action', type: 'text', visible: true }
  ];
  constructor(private dialog: MatDialog,
    public translateService:TranslateService,
    private gradeLevelService:GradeLevelService,
    private loaderService:LoaderService,
    private snackbar: MatSnackBar) { 
      translateService.use('en');
    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    }); 
  }
    ngOnInit(): void {
      this.GetAllGradeLevel();
    }

  openAddNew() {
    this.editMode=false;
    this.dialog.open(EditGradeLevelsComponent, {
      data: {editMode:this.editMode,gradeLevels:this.sendGradeLevelsToDialog},
      width: '600px'
    }).afterClosed().subscribe((res) => {
      if(res){
        this.GetAllGradeLevel();
      }            
    });
  }

  openEdit(editDetails){
    this.editMode = true;
    this.dialog.open(EditGradeLevelsComponent,{
      data:{
        editMode:this.editMode,
        editDetails:editDetails,
        gradeLevels:this.sendGradeLevelsToDialog
      },
      width:'600px'
    }).afterClosed().subscribe((res) => {
      if(res){
        this.GetAllGradeLevel();
      }            
    });
  }


  GetAllGradeLevel(){
    this.getAllGradeLevels.schoolId=+sessionStorage.getItem("selectedSchoolId");
    this.getAllGradeLevels._tenantName=sessionStorage.getItem("tenant");
    this.getAllGradeLevels._token=sessionStorage.getItem("token");
    this.gradeLevelService.getAllGradeLevels(this.getAllGradeLevels).subscribe((res)=>{
        this.gradeLevelList = new MatTableDataSource(res.tableGradelevelList);
        this.gradeLevelList.sort = this.sort;
        this.sendGradeLevelsToDialog=res.tableGradelevelList;
    })
  }

  onSearchClear(){
    this.searchKey="";
    this.applyFilter();
  }

  applyFilter(){
    this.gradeLevelList.filter = this.searchKey.trim().toLowerCase()
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

  confirmDelete(deleteDetails){
    // call our modal window
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Are you sure?",
          message: "You are about to delete "+deleteDetails.title+"."}
    });
    // listen to response
    dialogRef.afterClosed().subscribe(dialogResult => {
      // if user pressed yes dialogResult will be true, 
      // if user pressed no - it will be false
      if(dialogResult){
        this.DeleteGradeLevel(deleteDetails);
      }
   });
  }

  DeleteGradeLevel(deleteDetails){
    this.deleteGradeLevel.tblGradelevel.schoolId = deleteDetails.schoolId;
    this.deleteGradeLevel.tblGradelevel.gradeId = deleteDetails.gradeId;
    this.deleteGradeLevel._tenantName=sessionStorage.getItem("tenant");
    this.deleteGradeLevel._token=sessionStorage.getItem("token");
    console.log(this.deleteGradeLevel)
    this.gradeLevelService.deleteGradelevel(this.deleteGradeLevel).subscribe((res)=>{
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Grade Level Deletion failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }else if (res._failure) {
      console.log(res);
        this.snackbar.open(res._message, 'LOL THANKS', {
          duration: 10000
        });
      } else {
        this.snackbar.open('Grade Level Deleted Successfully.', '', {
          duration: 10000
        });
        console.log(res)
        this.GetAllGradeLevel();
      }
    })
  }
}
