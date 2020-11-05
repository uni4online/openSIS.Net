import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icAdd from '@iconify/icons-ic/baseline-add';

@Component({
  selector: 'vex-student-familyinfo',
  templateUrl: './student-familyinfo.component.html',
  styleUrls: ['./student-familyinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StudentFamilyinfoComponent implements OnInit {

  icEdit = icEdit;
  icDelete = icDelete;
  icAdd = icAdd;
  pageTitle = 'Contacts';
  pageId = 'Contacts';

  displayContacts = true;
  displaySiblingsInfo = true;
  ContactsFlag = true
  SiblingsInfoFlag = true

  //form: FormGroup;

  constructor(public translateService:TranslateService) { 
    this.showChildPage('Contacts');
  }

  ngOnInit(): void {
    this.pageId = localStorage.getItem("pageId");

  }

  showChildPage(pageId = 'Contacts') {
    console.log(pageId);
    this.pageTitle = pageId;
    if (pageId === 'Contacts') {
      this.displayContacts = true;
      this.ContactsFlag = true;
    } else {
      this.displayContacts = false;
      this.ContactsFlag = false;
    }
    if (pageId === 'Siblings Info') {
      this.displaySiblingsInfo = true;
      this.SiblingsInfoFlag = true;
    } else {
      this.displaySiblingsInfo = false;
      this.SiblingsInfoFlag = false;
  }
  }

}
