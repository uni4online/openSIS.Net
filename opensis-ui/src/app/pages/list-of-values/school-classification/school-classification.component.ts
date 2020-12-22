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
import { EditSchoolClassificationComponent } from './edit-school-classification/edit-school-classification.component';

@Component({
  selector: 'vex-school-classification',
  templateUrl: './school-classification.component.html',
  styleUrls: ['./school-classification.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class SchoolClassificationComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Title', property: 'title', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sort_order', type: 'number', visible: true },
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
  loading:Boolean;

  constructor(private router: Router,private dialog: MatDialog,public translateService:TranslateService) {
    translateService.use('en');
    this.SchoolClassificationModelList = [
      {title: 'Public School', sort_order: 1},
      {title: 'Private School', sort_order: 2},
      {title: 'Charter School', sort_order: 3},
      {title: 'Magnet School', sort_order: 4},
      {title: 'Trade School', sort_order: 5},
      {title: 'Home School', sort_order: 6},
      {title: 'Religious School', sort_order: 7},
      {title: 'Military School', sort_order: 8},
      {title: 'Pre-K/Montessori', sort_order: 9},
      {title: 'College', sort_order: 10},
      {title: 'University', sort_order: 11},
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
    this.dialog.open(EditSchoolClassificationComponent, {
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
