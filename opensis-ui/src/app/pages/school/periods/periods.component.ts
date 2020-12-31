import { Component, OnInit } from '@angular/core';
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
import { EditBlockComponent } from './edit-block/edit-block.component';
import { LayoutService } from 'src/@vex/services/layout.service';
@Component({
  selector: 'vex-periods',
  templateUrl: './periods.component.html',
  styleUrls: ['./periods.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class PeriodsComponent implements OnInit {

  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;

  constructor(public translateService:TranslateService,private dialog: MatDialog,private layoutService:LayoutService) {
    translateService.use('en');
    if(localStorage.getItem("collapseValue") !== null){
      if( localStorage.getItem("collapseValue") === "false"){
        this.layoutService.expandSidenav();
      }else{
        this.layoutService.collapseSidenav();
      } 
    }else{
      this.layoutService.expandSidenav();
    }
  }

  ngOnInit(): void {
  }

  goToAddBlock() {
    this.dialog.open(EditBlockComponent, {
      width: '500px'
    });
  }

}
