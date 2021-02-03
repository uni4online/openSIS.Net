import { Component, OnInit, Input } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import icImport from '@iconify/icons-ic/twotone-unarchive';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router} from '@angular/router';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import { EditCommonToiletAccessibilityComponent } from './edit-common-toilet-accessibility/edit-common-toilet-accessibility.component';

@Component({
  selector: 'vex-common-toilet-accessibility',
  templateUrl: './common-toilet-accessibility.component.html',
  styleUrls: ['./common-toilet-accessibility.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class CommonToiletAccessibilityComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Accessibility Name', property: 'accessibility_name', type: 'text', visible: true },
    { label: 'Created By', property: 'created_by', type: 'text', visible: true },
    { label: 'Created Date', property: 'created_date', type: 'text', visible: true },
    { label: 'Updated By', property: 'updated_by', type: 'text', visible: true },
    { label: 'Updated Date', property: 'updated_date', type: 'text', visible: true },
    { label: 'Actions', property: 'actions', type: 'text', visible: true }
  ];

  EffortGradeScaleModelList;

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icImport = icImport;
  icFilterList = icFilterList;
  loading:Boolean;

  constructor(private router: Router,private dialog: MatDialog,public translateService:TranslateService) {
    translateService.use('en');
    this.EffortGradeScaleModelList = [
      {accessibility_name: 'Wheelchair height toilet', created_by: 'John Doe', created_date: 'Jan 08, 2021 | 10:03 AM', updated_by: 'John Doe', updated_date: 'Jan 08, 2021 | 10:03 AM'},
      {accessibility_name: 'Bathroom emergency pullstring', created_by: 'John Doe', created_date: 'Jan 08, 2021 | 10:03 AM', updated_by: 'John Doe', updated_date: 'Jan 08, 2021 | 10:03 AM'},
      {accessibility_name: 'Wheelchair height sink and hand dryer', created_by: 'John Doe', created_date: 'Jan 08, 2021 | 10:03 AM', updated_by: 'John Doe', updated_date: 'Jan 08, 2021 | 10:03 AM'},
      {accessibility_name: 'wheelchair width door', created_by: 'John Doe', created_date: 'Jan 08, 2021 | 10:03 AM', updated_by: 'John Doe', updated_date: 'Jan 08, 2021 | 10:03 AM'},
      {accessibility_name: 'Pit latrines', created_by: 'John Doe', created_date: 'Jan 08, 2021 | 10:03 AM', updated_by: 'John Doe', updated_date: 'Jan 08, 2021 | 10:03 AM'},
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
    this.dialog.open(EditCommonToiletAccessibilityComponent, {
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
