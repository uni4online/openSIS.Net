import { Component, Input, OnInit,Output,EventEmitter, ViewChild } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import { HttpClient } from 'selenium-webdriver/http';
import { SchoolService } from '../../../../services/school.service';
import { GetAllSchoolModel,AllSchoolListModel } from '../../../../models/getAllSchoolModel';

import { MatSnackBar } from '@angular/material/snack-bar';
import { Router} from '@angular/router';
import { SelectionModel } from '@angular/cdk/collections';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldDefaultOptions } from '@angular/material/form-field';
import { stagger40ms } from '../../../../../@vex/animations/stagger.animation';
import { MatSelectChange } from '@angular/material/select';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { LoaderService } from '../../../../services/loader.service';

@Component({
  selector: 'vex-school-details',
  templateUrl: './school-details.component.html',
  styleUrls: ['./school-details.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ],
})
export class SchoolDetailsComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Name', property: 'schoolName', type: 'text', visible: true },
    { label: 'Address', property: 'streetAddress1', type: 'text', visible: true, cssClasses: ['font-medium'] },
    { label: 'Principal', property: 'nameOfPrincipal', type: 'text', visible: true },
    { label: 'Phone', property: 'telephone', type: 'text', visible: true, cssClasses: ['text-secondary', 'font-medium'] },
    { label: 'Status', property: 'status', type: 'text', visible: true }
  ];

  selection = new SelectionModel<any>(true, []);
  totalCount:number=0;pageNumber:number;pageSize:number;
  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icSearch = icSearch;
  icFilterList = icFilterList;
  fapluscircle = "fa-plus-circle";
  tenant = "";
  loading:Boolean;
  getAllSchool: GetAllSchoolModel = new GetAllSchoolModel();
  SchoolModelList: MatTableDataSource<AllSchoolListModel>;
  
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator; 
  @ViewChild(MatSort) sort:MatSort

  constructor(private schoolService: SchoolService,
    private snackbar: MatSnackBar,
    private router: Router,
    private loaderService:LoaderService
    ) 
    { 
     this.loaderService.isLoading.subscribe((val) => {
        this.loading = val;
      });
      this.callAllSchool();
  }

  ngOnInit(): void {
  }
  ngAfterViewInit() {
    this.getAllSchool=new GetAllSchoolModel();
    this.sort.sortChange.subscribe((res) => {
      this.getAllSchool.pageNumber=this.pageNumber
      this.getAllSchool.pageSize=this.pageSize;
      this.getAllSchool.sortingModel.sortColumn=res.active;
      if(res.direction==""){
       this.getAllSchool.sortingModel=null;
      this.callAllSchool();
      this.getAllSchool=new GetAllSchoolModel();
      this.getAllSchool.sortingModel=null;
      }else{
      this.getAllSchool.sortingModel.sortDirection=res.direction;
    this.callAllSchool();
      }
    });
  }

  goToAdd(){
    this.schoolService.setSchoolId(null)
    this.router.navigate(["school/schoolinfo/add-school"]);
  }  

  getPageEvent(event){
    if(this.sort.active!=undefined && this.sort.direction!=""){
      this.getAllSchool.sortingModel.sortColumn=this.sort.active;
      this.getAllSchool.sortingModel.sortDirection=this.sort.direction;
    }
    this.getAllSchool.pageNumber=event.pageIndex+1;
    this.getAllSchool.pageSize=event.pageSize;
    this.callAllSchool();
  }
  
  viewGeneralInfo(id:number){    
    this.schoolService.setSchoolId(id)
    this.router.navigate(["school/schoolinfo/add-school/"]); 
  }
  callAllSchool(){
    if(this.getAllSchool.sortingModel?.sortColumn==""){
      this.getAllSchool.sortingModel=null
    }
    this.schoolService.GetAllSchoolList(this.getAllSchool).subscribe(data => {
      if(data._failure){
        this.snackbar.open('School information failed. '+ data._message, 'LOL THANKS', {
        duration: 10000
        });
      }else{
        this.totalCount=data.totalCount;
        this.pageNumber = data.pageNumber;
        this.pageSize = data._pageSize;
        this.SchoolModelList = new MatTableDataSource(data.getSchoolForView);
        this.getAllSchool=new GetAllSchoolModel();
      }
    });
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

  onFilterChange(value: string) {
    if (!this.SchoolModelList) {
      return;
    }
    value = value.trim();
    value = value.toLowerCase();
    this.SchoolModelList.filter = value;
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }
}
