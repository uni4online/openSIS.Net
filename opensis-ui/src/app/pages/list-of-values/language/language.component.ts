import { Component, OnInit, Input ,ViewChild} from '@angular/core';
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
import { EditLanguageComponent } from './edit-language/edit-language.component';
import {LanguageModel} from '../../../models/languageModel';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { LoaderService } from '../../../services/loader.service';
import { CommonService } from '../../../services/common.service';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'vex-language',
  templateUrl: './language.component.html',
  styleUrls: ['./language.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class LanguageComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Title', property: 'locale', type: 'text', visible: true },
    { label: 'Short Code', property: 'languageCode', type: 'text', visible: true },
    { label: 'Created By', property: 'createdBy', type: 'text', visible: true },
    { label: 'Create Date', property: 'createdOn', type: 'text', visible: true },
    { label: 'Updated By', property: 'updatedBy', type: 'text', visible: true },
    { label: 'Update Date', property: 'updatedOn', type: 'text', visible: true },
    { label: 'Actions', property: 'actions', type: 'text', visible: true }
  ];

  SchoolClassificationModelList;

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icImpersonate = icImpersonate;
  icFilterList = icFilterList; 
  loading;  
  totalCount:Number;pageNumber:Number;pageSize:Number;
  languageModel: LanguageModel = new LanguageModel();  
  languageModelList: MatTableDataSource<any>;
  searchKey:string;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private router: Router,
    private dialog: MatDialog,
    public translateService:TranslateService,
    public loaderService:LoaderService,
    public commonService:CommonService,
    public snackbar:MatSnackBar
    ) {
    translateService.use('en');
    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    });
    this.getLanguageList();
  }

  ngOnInit(): void {
    
  }
 
  getLanguageList(){
    this.languageModel._tenantName = sessionStorage.getItem("tenant");
    this.commonService.GetAllLanguage(this.languageModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Language list failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }else{
        if (data._failure) {
          if (data._message === "NO RECORD FOUND") {
            this.languageModelList = new MatTableDataSource(data.tableLanguage);
            this.languageModelList.sort=this.sort;
            this.languageModelList.paginator = this.paginator;       
          } else {
            this.snackbar.open('Language list failed.' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          }
        }else{ 
          this.languageModelList = new MatTableDataSource(data.tableLanguage);
          this.languageModelList.sort=this.sort; 
          this.languageModelList.paginator = this.paginator;         
        } 
      }
    });
  }

  goToAdd(){
    this.dialog.open(EditLanguageComponent, {
      data: {
        mode:"add"       
      },  
      width: '500px'
    }).afterClosed().subscribe((data) => {
      if(data){
        this.getLanguageList();
      }            
    });   
  }

  goToEdit(editDetails){
    this.dialog.open(EditLanguageComponent, {
      data: {
        mode:"edit",
        editDetails:editDetails
      },  
      width: '500px'
    }).afterClosed().subscribe((data) => {
      if(data){
        this.getLanguageList();
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

  onSearchClear(){
    this.searchKey="";
    this.applyFilter();
  }

  applyFilter(){
    this.languageModelList.filter = this.searchKey.trim().toLowerCase()
  }

}
