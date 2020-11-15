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
    { label: 'Name', property: 'studentName', type: 'text', visible: true },
    { label: 'Student ID', property: 'studentId', type: 'text', visible: true },
    { label: 'Alternate ID', property: 'studentAlternateId', type: 'text', visible: true },    
    { label: 'Phone', property: 'phone', type: 'text', visible: true }
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
    console.log(this.sort)
    this.getAllStudent=new StudentListModel();
    this.sort.sortChange.subscribe((res) => {
      this.getAllStudent.pageNumber=this.pageNumber
      this.getAllStudent.pageSize=this.pageSize;
      this.getAllStudent.sortingModel.sortColumn=res.active;
      if(this.searchCtrl.value!=null){
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
        this.getAllStudent.pageNumber=this.pageNumber;
        this.getAllStudent.pageSize=this.pageSize;
        this.callAllStudent();
        }
        else
        {
          this.getAllStudent.pageNumber=this.pageNumber;
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
    this.studentService.setViewGeneralInfoData("");
    this.router.navigate(["school/students/student-generalinfo"]);
  }

  getPageEvent(event){
    if(this.sort.active!=undefined && this.sort.direction!=""){
      this.getAllStudent.sortingModel.sortColumn=this.sort.active;
      this.getAllStudent.sortingModel.sortDirection=this.sort.direction;
    }
    if(this.searchCtrl.value!=null){
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
        this.allStudentList = data.studentMaster;
        data.studentMaster.map((value:any) => {
          var obj = {};
          var middleName="";
         if(value.middleName !== null){
           middleName = value.middleName
         }
          obj = {
            studentName: value.firstGivenName+' '+middleName+' '+value.lastFamilyName,
            studentId: value.studentId,
            studentAlternateId: value.alternateId,
            phone: value.homePhone,
            }   
            filterArr.push(obj)               
        });        
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

  viewGeneralInfo(data){  
    var obj ={}
    this.allStudentList.map((value:any) => {
      if(data.studentId === value.studentId){
        obj = value;
      }              
    }); 
    this.studentService.setViewGeneralInfoData(obj);
    this.router.navigate(["school/students/student-generalinfo"]); 
  }
}