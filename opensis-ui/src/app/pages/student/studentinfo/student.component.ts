import { Component, Input, OnInit,Output,EventEmitter, ViewChild } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import { StudentService } from '../../../services/student.service';
import { StudentListModel} from '../../../models/studentModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router} from '@angular/router';
import { SelectionModel } from '@angular/cdk/collections';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { MatPaginator} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { LoaderService } from '../../../services/loader.service';
import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

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
  columns = [
    { label: 'Name', property: 'firstGivenName', type: 'text', visible: true },
    { label: 'Student ID', property: 'studentId', type: 'text', visible: true },
    { label: 'Alternate ID', property: 'alternateId', type: 'text', visible: true },    
    { label: 'Phone', property: 'homePhone', type: 'text', visible: true }
  ];

  selection = new SelectionModel<any>(true, []);
  totalCount:number=0;
  pageNumber:number;
  pageSize:number;
  searchCtrl: FormControl;
  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icSearch = icSearch;
  icFilterList = icFilterList;
  fapluscircle = "fa-plus-circle";
  tenant = "";
  loading:Boolean;
  allStudentList=[];
  getAllStudent: StudentListModel = new StudentListModel(); 
  StudentModelList: MatTableDataSource<StudentListModel>;
  
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator; 
  @ViewChild(MatSort) sort:MatSort

  constructor(
    private studentService: StudentService,
    private snackbar: MatSnackBar,
    private router: Router,
    private loaderService:LoaderService
    ) 
    { 
     this.getAllStudent.filterParams=null;
     this.loaderService.isLoading.subscribe((val) => {
        this.loading = val;
      });
      this.callAllStudent();
    }

  

  ngOnInit(): void {
    this.searchCtrl = new FormControl();
  }

  ngAfterViewInit() {
    //  Sorting
    this.getAllStudent=new StudentListModel();
    this.sort.sortChange.subscribe((res) => {
      this.getAllStudent.pageNumber=this.pageNumber
      this.getAllStudent.pageSize=this.pageSize;
      this.getAllStudent.sortingModel.sortColumn=res.active;
      if(this.searchCtrl.value!=null && this.searchCtrl.value!=""){
        let filterParams=[
          {
            columnName:null,
            filterValue:this.searchCtrl.value,
            filterOption:4
          }
        ]
        Object.assign(this.getAllStudent,{filterParams: filterParams});
      }
      if(res.direction==""){
        this.getAllStudent.sortingModel=null;
        this.callAllStudent();
        this.getAllStudent=new StudentListModel();
        this.getAllStudent.sortingModel=null;
      }else{
        this.getAllStudent.sortingModel.sortDirection=res.direction;
        this.callAllStudent();
      }
    });
      //  Searching
    this.searchCtrl.valueChanges.pipe(debounceTime(500),distinctUntilChanged()).subscribe((term)=>{
      if(term!='')
      {
          let filterParams=[
          {
            columnName:null,
            filterValue:term,
            filterOption:4
          }
        ]
        if(this.sort.active!=undefined && this.sort.direction!=""){
          this.getAllStudent.sortingModel.sortColumn=this.sort.active;
          this.getAllStudent.sortingModel.sortDirection=this.sort.direction;
        }
        Object.assign(this.getAllStudent,{filterParams: filterParams});
        this.getAllStudent.pageNumber=1;
        this.paginator.pageIndex=0;
        this.getAllStudent.pageSize=this.pageSize;
        this.callAllStudent();
        }
        else
        {
        Object.assign(this.getAllStudent,{filterParams: null});
          this.getAllStudent.pageNumber=this.paginator.pageIndex+1;
          this.getAllStudent.pageSize=this.pageSize;
          if(this.sort.active!=undefined && this.sort.direction!=""){
            this.getAllStudent.sortingModel.sortColumn=this.sort.active;
            this.getAllStudent.sortingModel.sortDirection=this.sort.direction;
          }
          this.callAllStudent();
        }
      })
    }

  goToAdd(){   
    this.router.navigate(["school/students/student-generalinfo"]);
  }

  viewStudentDetails(id){  
    this.studentService.setStudentId(id)
    this.router.navigate(["school/students/student-generalinfo"]); 
  }

  getPageEvent(event){
    if(this.sort.active!=undefined && this.sort.direction!=""){
      this.getAllStudent.sortingModel.sortColumn=this.sort.active;
      this.getAllStudent.sortingModel.sortDirection=this.sort.direction;
    }
    if(this.searchCtrl.value!=null && this.searchCtrl.value!=""){
      let filterParams=[
        {
         columnName:null,
         filterValue:this.searchCtrl.value,
         filterOption:4
        }
      ]
     Object.assign(this.getAllStudent,{filterParams: filterParams});
    }
    this.getAllStudent.pageNumber=event.pageIndex+1;
    this.getAllStudent.pageSize=event.pageSize;
    this.callAllStudent();
  }

  callAllStudent(){
    if(this.getAllStudent.sortingModel?.sortColumn==""){
      this.getAllStudent.sortingModel=null
    }
    this.studentService.GetAllStudentList(this.getAllStudent).subscribe(data => {
      if(data._failure){
        this.snackbar.open('Student information failed. '+ data._message, 'LOL THANKS', {
        duration: 10000
        });
      }else{
        this.totalCount= data.totalCount;
        this.pageNumber = data.pageNumber;
        this.pageSize = data._pageSize;  
        let filterArr = [];
        this.allStudentList = data.getStudentListForViews;
        if(data.getStudentListForViews!=null){
          data.getStudentListForViews.map((value:any) => {
            var obj = {};
            var middleName="";
           if(value.middleName !== null){
             middleName = value.middleName
           }
            obj = {
              firstGivenName: value.firstGivenName+' '+middleName+' '+value.lastFamilyName,
              studentInternalId: value.studentInternalId,
              alternateId: value.alternateId,
              homePhone: value.mobilePhone,
              studentId:value.studentId
              }   
              filterArr.push(obj)               
          }); 
        }     
        this.StudentModelList = new MatTableDataSource(filterArr);      
        this.getAllStudent=new StudentListModel();     
      }
    });
  }


  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

  onFilterChange(value: string) {
    if (!this.StudentModelList) {
      return;
    }
    value = value.trim();
    value = value.toLowerCase();
    this.StudentModelList.filter = value;
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

}