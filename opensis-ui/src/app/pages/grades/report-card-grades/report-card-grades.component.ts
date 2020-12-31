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
import { EditGradeScaleComponent } from './edit-grade-scale/edit-grade-scale.component';
import { EditReportCardGradeComponent } from './edit-report-card-grade/edit-report-card-grade.component';

@Component({
  selector: 'vex-report-card-grades',
  templateUrl: './report-card-grades.component.html',
  styleUrls: ['./report-card-grades.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class ReportCardGradesComponent implements OnInit {

  @Input()
  columns = [
    { label: 'ID', property: 'id', type: 'number', visible: true },
    { label: 'Title', property: 'title', type: 'text', visible: true },
    { label: 'Breakoff', property: 'breakoff', type: 'text', visible: true },
    { label: 'Weighted GP Value', property: 'weighted_gp_value', type: 'text', visible: true },
    { label: 'unweighted GP Value', property: 'unweighted_gp_value', type: 'text', visible: true },
    { label: 'Comment', property: 'comment', type: 'text', visible: true },
    { label: 'action', property: 'action', type: 'text', visible: true }
  ];

  ReportCardGradesModelList;

  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;
  loading:Boolean;

  constructor(private router: Router,private dialog: MatDialog,public translateService:TranslateService) {
    translateService.use('en');
    this.ReportCardGradesModelList = [
      {id: 1, title: 'A', breakoff: '90', weighted_gp_value: '0.00', unweighted_gp_value: '4.00', comment: ''},
      {id: 1, title: 'B', breakoff: '80', weighted_gp_value: '0.00', unweighted_gp_value: '3.00', comment: ''},
      {id: 1, title: 'C', breakoff: '70', weighted_gp_value: '0.00', unweighted_gp_value: '2.00', comment: ''},
      {id: 1, title: 'D', breakoff: '60', weighted_gp_value: '0.00', unweighted_gp_value: '1.00', comment: ''},
      {id: 1, title: 'F', breakoff: '0', weighted_gp_value: '0.00', unweighted_gp_value: '0.00', comment: ''},
    ]
  }

  ngOnInit(): void {
  }

  getPageEvent(event){    
    // this.getAllSchool.pageNumber=event.pageIndex+1;
    // this.getAllSchool.pageSize=event.pageSize;
    // this.callAllSchool(this.getAllSchool);
  }

  goToAddGrade() {
    this.dialog.open(EditReportCardGradeComponent, {
      width: '500px'
    });
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  goToAddBlock() {
    this.dialog.open(EditGradeScaleComponent, {
      width: '500px'
    });
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

}
