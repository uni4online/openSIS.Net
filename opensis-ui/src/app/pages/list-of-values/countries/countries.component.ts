import { Component, OnInit, Input } from '@angular/core';
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
import { EditCountryComponent } from './edit-country/edit-country.component';

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
    { label: 'Title', property: 'title', type: 'text', visible: true },
    { label: 'Short Name', property: 'short_name', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sort_order', type: 'number', visible: true },
    { label: 'Actions', property: 'actions', type: 'text', visible: true }
  ];

  CountriesModelList;

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icImpersonate = icImpersonate;
  icFilterList = icFilterList;
  loading:Boolean;

  constructor(private router: Router,private dialog: MatDialog,public translateService:TranslateService) {
    translateService.use('en');
    this.CountriesModelList = [
      {title: 'United States', short_name: 'US', sort_order: 1},
      {title: 'Argentina', short_name: 'AR', sort_order: 2},
      {title: 'Australia', short_name: 'AU', sort_order: 3},
      {title: 'Afghanistan', short_name: 'AF', sort_order: 4},
      {title: 'Bahrain', short_name: 'BH', sort_order: 5},
      {title: 'Bolivia', short_name: 'BO', sort_order: 6},
      {title: 'Brazil', short_name: 'BR', sort_order: 7},
      {title: 'Canada', short_name: 'CA', sort_order: 8},
      {title: 'China', short_name: 'CN', sort_order: 9},
      {title: 'Cuba', short_name: 'CU', sort_order: 10},
      {title: 'Denmark', short_name: 'DK', sort_order: 11},
      {title: 'Dominica', short_name: 'DM', sort_order: 12},
      {title: 'Dominican Republic', short_name: 'DO', sort_order: 13},
      {title: 'Ecuador', short_name: 'EC', sort_order: 14},
      {title: 'Egypt', short_name: 'EG', sort_order: 15},
      {title: 'France', short_name: 'FR', sort_order: 16},
      {title: 'Georgia', short_name: 'GE', sort_order: 17},
      {title: 'Germany', short_name: 'DE', sort_order: 18},
      {title: 'Guyana', short_name: 'GY', sort_order: 19},
      {title: 'India', short_name: 'IN', sort_order: 20},
      {title: 'Japan', short_name: 'JP', sort_order: 21},
      {title: 'Kenya', short_name: 'KE', sort_order: 22},
      {title: 'Korea (North)', short_name: 'KP', sort_order: 23},
      {title: 'Korea (South)', short_name: 'KR', sort_order: 24},
    ]
  }

  ngOnInit(): void {
  }

  getPageEvent(event){    
    // this.getAllSchool.pageNumber=event.pageIndex+1;
    // this.getAllSchool.pageSize=event.pageSize;
    // this.callAllSchool(this.getAllSchool);
  }

  goToAdd(){
    this.dialog.open(EditCountryComponent, {
      width: '500px'
    })
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
