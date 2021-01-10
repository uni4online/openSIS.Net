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
import { EditEffortGradeScaleComponent } from './edit-effort-grade-scale/edit-effort-grade-scale.component';

@Component({
  selector: 'vex-effort-grade-scale',
  templateUrl: './effort-grade-scale.component.html',
  styleUrls: ['./effort-grade-scale.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class EffortGradeScaleComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Order', property: 'order', type: 'number', visible: true },
    { label: 'Value', property: 'value', type: 'text', visible: true },
    { label: 'Comment', property: 'comment', type: 'text', visible: true },
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
      {order: '1', value: '1', comment: 'Outstanding Effort'},
      {order: '2', value: '2', comment: 'Satisfactory Effort'},
      {order: '3', value: '3', comment: 'Work shows improvement'},
      {order: '4', value: '4', comment: 'Not working ability'},
      {order: '5', value: '5', comment: 'Minimal Effort Shown'},
      {order: '6', value: '6', comment: 'Request for Conferencce'}
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
    this.dialog.open(EditEffortGradeScaleComponent, {
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
