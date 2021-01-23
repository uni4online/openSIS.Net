import { Component, OnInit, Input } from '@angular/core';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import { Router} from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import { EditPeriodComponent } from '../edit-period/edit-period.component';
import { SchoolPeriodService } from 'src/app/services/school-period.service';

@Component({
  selector: 'vex-periods-list',
  templateUrl: './periods-list.component.html',
  styleUrls: ['./periods-list.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class PeriodsListComponent implements OnInit {

  columns = [
    { label: 'ID', property: 'periodId', type: 'number', visible: true },
    { label: 'Title', property: 'periodTitle', type: 'text', visible: true },
    { label: 'Short Name', property: 'periodShortName', type: 'text', visible: true },
    { label: 'Start Time', property: 'periodStartTime', type: 'text', visible: true },
    { label: 'End Time', property: 'periodEndTime', type: 'text', visible: true },
    { label: 'Length', property: 'length', type: 'number', visible: true },
    { label: 'action', property: 'action', type: 'text', visible: true }
  ];

  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;
  loading:Boolean;
  periodsModelList: any;

  constructor(private router: Router,private dialog: MatDialog,
    private schoolPeriodService: SchoolPeriodService,
    public translateService:TranslateService) {
      this.periodsModelList = this.schoolPeriodService.getBlockPeriodList();
      
    translateService.use('en');
    
  }

  ngOnInit(): void {
    this.periodsModelList =this.schoolPeriodService.getBlockPeriodList();
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

  goToAddPeriod() {
    this.dialog.open(EditPeriodComponent, {
      width: '500px'
    });
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

}
