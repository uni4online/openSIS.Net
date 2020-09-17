import { Component, Input, OnInit, ViewChild } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import { HttpClient } from 'selenium-webdriver/http';
import { SchoolService } from '../../../../services/school.service';
import { SchoolViewModel, SchoolListViewModel } from 'src/app/models/schoolModel';
import { GetAllSchoolModel,AllSchoolListModel } from 'src/app/models/getAllSchoolModel';

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
    { label: 'Checkbox', property: 'checkbox', type: 'checkbox', visible: true },
    { label: 'Name', property: 'school_Name', type: 'text', visible: true },
    { label: 'Address', property: 'school_Address', type: 'text', visible: true, cssClasses: ['font-medium'] },
    { label: 'Principle', property: 'principle', type: 'text', visible: true },
    { label: 'Phone', property: 'phone', type: 'text', visible: true, cssClasses: ['text-secondary', 'font-medium'] },
    { label: 'Status', property: 'status', type: 'text', visible: true }
  ];

  selection = new SelectionModel<any>(true, []);
  totalCount:Number;pageNumber:Number;pageSize:Number;
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

  constructor(private schoolService: SchoolService,
    private snackbar: MatSnackBar,
    private router: Router,
    private loaderService:LoaderService
    ) 
    { 
     this.loaderService.isLoading.subscribe((val) => {
        this.loading = val;
      });
     this.getSchooldetails();
  }

  ngOnInit(): void {
  }
  goToAdd(){
    this.router.navigate(["school/schoolinfo/add-school"]);
  }  
  getSchooldetails()
  {
    this.getAllSchool._tenantName=sessionStorage.getItem("tenant");
    this.getAllSchool._token=sessionStorage.getItem("token");
    this.callAllSchool(this.getAllSchool);
  }

  getPageEvent(event){
    
    this.getAllSchool.pageNumber=event.pageIndex+1;
    this.getAllSchool.pageSize=event.pageSize;
    this.callAllSchool(this.getAllSchool);
  }

  callAllSchool(getAllSchool){
    this.schoolService.GetSchool(this.getAllSchool).subscribe(data => {
      if(data._failure){
        this.snackbar.open('School information failed. '+ data._message, 'LOL THANKS', {
        duration: 10000
        });
      }else{
        this.totalCount=data.totalCount;
        this.pageNumber = data.pageNumber;
        this.pageSize = data._pageSize;
        this.SchoolModelList = new MatTableDataSource(data.getSchoolForView);
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

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.SchoolModelList.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.SchoolModelList.data.forEach(row => this.selection.select(row));
  }
}
