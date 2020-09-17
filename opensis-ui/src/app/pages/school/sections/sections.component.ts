import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/twotone-search';
import icAdd from '@iconify/icons-ic/twotone-add';
import icFilterList from '@iconify/icons-ic/twotone-filter-list';
import { EditSectionComponent } from '../sections/edit-section/edit-section.component';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'vex-sections',
  templateUrl: './sections.component.html',
  styleUrls: ['./sections.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class SectionsComponent implements OnInit {

  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icAdd = icAdd;
  icFilterList = icFilterList;

  constructor(private dialog: MatDialog, public translateService:TranslateService) { translateService.use('en'); }

  ngOnInit(): void {
  }

  openAddNew() {
    this.dialog.open(EditSectionComponent, {
      data: null,
      width: '600px'
    });
  }

}
