import { Component, OnInit, Input,AfterViewInit } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import icImpersonate from '@iconify/icons-ic/twotone-account-circle';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router} from '@angular/router';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import {ParentInfoService} from '../../../services/parent-info.service';
import { GetAllParentModel } from "../../../models/parentInfoModel";
import { LoaderService } from '../../../services/loader.service';
import { FormControl } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'vex-parentinfo',
  templateUrl: './parentinfo.component.html',
  styleUrls: ['./parentinfo.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class ParentinfoComponent implements OnInit,AfterViewInit {
  @Input()
  columns = [
    { label: 'Parent�s Name', property: 'name', type: 'text', visible: true },
    { label: 'Profile', property: 'profile', type: 'text', visible: true },
    { label: 'Email Address', property: 'email_address', type: 'text', visible: true },
    { label: 'Mobile Phone', property: 'mobile_phone', type: 'number', visible: true },
    { label: 'Associated Students', property: 'students', type: 'text', visible: true },
    { label: 'action', property: 'action', type: 'text', visible: true }
  ];
  
  totalCount:number;
  pageNumber:number;
  pageSize:number;
  searchCtrl: FormControl;

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;
  icImpersonate = icImpersonate;
  loading:Boolean;
  getAllParentModel:GetAllParentModel= new GetAllParentModel()
  ParentFieldsModelList: MatTableDataSource<any>;

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private parentInfoService:ParentInfoService,
    private snackbar: MatSnackBar,
    private loaderService:LoaderService,
    public translateService:TranslateService
    ) {
    translateService.use('en');
    this.getAllParentModel.filterParams=null;
    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    });
    this.getAllparentList();
    
  }

  ngOnInit(): void {
    this.searchCtrl = new FormControl();
  }

  getPageEvent(event){
    if(this.searchCtrl.value!=null){
      let filterParams=[
        {
         columnName:null,
         filterValue:this.searchCtrl.value,
         filterOption:1
        }
      ]
     Object.assign(this.getAllParentModel,{filterParams: filterParams});
    }
    this.getAllParentModel.pageNumber=event.pageIndex+1;
    this.getAllParentModel._pageSize=event.pageSize;
    this.getAllparentList();
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }
  getAllparentList(){
    this.parentInfoService.getAllParentInfo(this.getAllParentModel).subscribe(
      (res:GetAllParentModel)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open('Parent list failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (res._failure) {     
            this.snackbar.open('Parent list failed. ' + res._message, 'LOL THANKS', {
              duration: 10000
            });
          } 
          else { 
            this.totalCount=res.totalCount;

            this.pageNumber = res.pageNumber;
            this.pageSize = res._pageSize;
            this.ParentFieldsModelList=new MatTableDataSource(res.parentInfoForView);   
          }
        }
      }
    )
  }
    ngAfterViewInit() {
    
      //  Searching
      this.searchCtrl.valueChanges.pipe(debounceTime(500),distinctUntilChanged()).subscribe(
        
        (term)=>{
          if(term!=''){
            
            let filterParams=[
              {
                columnName:null,
                filterValue:term,
                filterOption:1
              }
            ]
            Object.assign(this.getAllParentModel,{filterParams: filterParams});
            this.getAllParentModel.pageNumber=this.pageNumber;
            this.getAllParentModel._pageSize=this.pageSize;
            this.getAllparentList();
          }
          else{
            Object.assign(this.getAllParentModel,{filterParams: null});
            this.getAllParentModel.pageNumber=this.pageNumber;
            this.getAllParentModel._pageSize=this.pageSize; 
            
            this.getAllparentList()
          }
        }
      )
    }

}
