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
import { EditSchoolLevelComponent } from './edit-school-level/edit-school-level.component';

@Component({
  selector: 'vex-school-level',
  templateUrl: './school-level.component.html',
  styleUrls: ['./school-level.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class SchoolLevelComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Title', property: 'title', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sort_order', type: 'number', visible: true },
    { label: 'Actions', property: 'actions', type: 'text', visible: true }
  ];

  SchoolLevelModelList;

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
    this.SchoolLevelModelList = [
      {title: 'Nursery', sort_order: 1},
      {title: 'Pre-School', sort_order: 2},
      {title: 'Pre-Kindergarten', sort_order: 3},
      {title: 'Kindergarten', sort_order: 4},
      {title: 'Primary', sort_order: 5},
      {title: 'Elementary', sort_order: 6},
      {title: 'Secondary', sort_order: 7},
      {title: 'Lower Secondary', sort_order: 8},
      {title: 'Upper Secondary', sort_order: 9},
      {title: 'Middle', sort_order: 10},
      {title: 'High', sort_order: 11},
      {title: 'Tertiary', sort_order: 12},
      {title: 'College', sort_order: 13},
      {title: 'University', sort_order: 14},
      {title: 'Graduate', sort_order: 15},
      {title: 'Post Graduate', sort_order: 16},
      {title: 'Doctoral', sort_order: 17},
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
    this.dialog.open(EditSchoolLevelComponent, {
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
