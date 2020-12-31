import { Component, OnInit, Input,ViewChild } from '@angular/core';
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
import { CountryModel } from '../../../models/countryModel';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { LoaderService } from '../../../services/loader.service';
import { CommonService } from '../../../services/common.service';
import { EditCountryComponent } from './edit-country/edit-country.component';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'vex-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class CountriesComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Title', property: 'name', type: 'text', visible: true },
    { label: 'Short Name', property: 'countryCode', type: 'text', visible: true },
    { label: 'Created By', property: 'createdBy', type: 'text', visible: true },
    { label: 'Create Date', property: 'createdOn', type: 'text', visible: true },
    { label: 'Updated By', property: 'updatedBy', type: 'text', visible: true },
    { label: 'Update Date', property: 'updatedOn', type: 'text', visible: true },
    { label: 'Actions', property: 'actions', type: 'text', visible: true }
  ];  
  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icImpersonate = icImpersonate;
  icFilterList = icFilterList; 
  loading;  
  totalCount:Number;pageNumber:Number;pageSize:Number;
  getCountryModel: CountryModel = new CountryModel();  
  CountryModelList: MatTableDataSource<any>;
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
    this.getCountryList();
  }

  ngOnInit(): void {}

  getCountryList(){    
    this.commonService.GetAllCountry(this.getCountryModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('Country list failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }else{
        if (data._failure) {
          if (data._message === "NO RECORD FOUND") {
            this.CountryModelList = new MatTableDataSource(data.tableCountry);
            this.CountryModelList.sort=this.sort;      
            this.CountryModelList.paginator = this.paginator;   
          } else {
            this.snackbar.open('Country list failed.' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          }
        }else{
          this.CountryModelList = new MatTableDataSource(data.tableCountry);
          this.CountryModelList.sort=this.sort;      
          this.CountryModelList.paginator = this.paginator;  
        }
      }
    });
  }

  goToAdd(){
    this.dialog.open(EditCountryComponent, {
      data: {
        mode:"add"       
      },  
      width: '500px'
    }).afterClosed().subscribe((data) => {
      if(data){
        this.getCountryList();
      }            
    });   
  }

  goToEdit(editDetails){
    this.dialog.open(EditCountryComponent, {
      data: {
        mode:"edit",
        editDetails:editDetails
      },  
      width: '500px'
    }).afterClosed().subscribe((data) => {
      if(data){
        this.getCountryList();
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
    this.CountryModelList.filter = this.searchKey.trim().toLowerCase()
  }

}
