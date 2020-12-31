import { Component, OnInit, Input } from '@angular/core';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import { Router} from '@angular/router';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import { MatDialog } from '@angular/material/dialog';
import { EditEffortItemComponent } from './edit-effort-item/edit-effort-item.component';
import { EditCategoryComponent } from './edit-category/edit-category.component';

@Component({
  selector: 'vex-effort-grade-library',
  templateUrl: './effort-grade-library.component.html',
  styleUrls: ['./effort-grade-library.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class EffortGradeLibraryComponent implements OnInit {

  @Input()
  columns = [
    { label: 'ID', property: 'id', type: 'number', visible: true },
    { label: 'Title', property: 'title', type: 'text', visible: true },
    { label: 'Action', property: 'action', type: 'text', visible: true }
  ];

  PeriodsModelList;

  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;
  loading:Boolean;

  constructor(private router: Router,private dialog: MatDialog,public translateService:TranslateService) {
    translateService.use('en');
    this.PeriodsModelList = [
      {id: 1, title: 'Listens for information'},
      {id: 2, title: 'Listens attentively while literature is being read'},
      {id: 3, title: 'Can carry out multiple directions'}
    ]
  }

  ngOnInit(): void {
  }

  getPageEvent(event){    
    // this.getAllSchool.pageNumber=event.pageIndex+1;
    // this.getAllSchool.pageSize=event.pageSize;
    // this.callAllSchool(this.getAllSchool);
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  goToAddEffortItem() {
    this.dialog.open(EditEffortItemComponent, {
      width: '500px'
    });
  }

  goToAddCategory() {
    this.dialog.open(EditCategoryComponent, {
      width: '500px'
    });
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

}
