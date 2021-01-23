import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';
import icAdd from '@iconify/icons-ic/baseline-add';
import icSuperadmin from '@iconify/icons-ic/baseline-admin-panel-settings';
import icAdmin from '@iconify/icons-ic/baseline-account-box';
import icTeacher from '@iconify/icons-ic/twotone-person';
import icHomeroomTeacher from '@iconify/icons-ic/twotone-account-circle';
import icParent from '@iconify/icons-ic/twotone-escalator-warning';
import icStudent from '@iconify/icons-ic/baseline-face';
import icMoreVert from '@iconify/icons-ic/more-vert';
import { TranslateService } from '@ngx-translate/core';
import { EditCustomProfileComponent } from '../../administration/access-control/edit-custom-profile/edit-custom-profile.component';

@Component({
  selector: 'vex-administration-settings',
  templateUrl: './administration-settings.component.html',
  styleUrls: ['./administration-settings.component.scss'],
  animations: [
    fadeInRight400ms
  ]
})
export class AdministrationSettingsComponent implements OnInit {
  pages=['Profiles and Permissions']
  administrationSettings=true;
  pageTitle = 'Profiles and Permissions';
  pageId: string = '';

  icAdd = icAdd;
  icSuperadmin = icSuperadmin;
  icAdmin = icAdmin;
  icTeacher = icTeacher;
  icHomeroomTeacher = icHomeroomTeacher;
  icParent = icParent;
  icStudent = icStudent;
  icMoreVert = icMoreVert;

  constructor(public translateService:TranslateService, private dialog: MatDialog) {
    translateService.use('en');
  }

  ngOnInit(): void {
    this.pageId = localStorage.getItem("pageId");
  }

  getSelectedPage(pageId){
    this.pageId = pageId;
    localStorage.setItem("pageId", pageId);
  }

  goToAdd() {
    this.dialog.open(EditCustomProfileComponent, {
      width: '500px'
    });
  }

}
