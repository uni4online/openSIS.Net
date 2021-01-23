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
import { EditHonorRollComponent } from './edit-honor-roll/edit-honor-roll.component';

@Component({
  selector: 'vex-honor-roll-setup',
  templateUrl: './honor-roll-setup.component.html',
  styleUrls: ['./honor-roll-setup.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class HonorRollSetupComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Honor Roll', property: 'honor_roll', type: 'text', visible: true },
    { label: 'Break Off', property: 'break_off', type: 'text', visible: true },
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
      {honor_roll: 'Bronze', break_off: '80'},
      {honor_roll: 'Silver', break_off: '85'},
      {honor_roll: 'Gold', break_off: '90'},
      {honor_roll: 'Platinum', break_off: '96'}
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
    this.dialog.open(EditHonorRollComponent, {
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
