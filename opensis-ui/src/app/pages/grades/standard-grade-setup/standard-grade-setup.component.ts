import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'vex-standard-grade-setup',
  templateUrl: './standard-grade-setup.component.html',
  styleUrls: ['./standard-grade-setup.component.scss']
})
export class StandardGradeSetupComponent implements OnInit {

  activeLink = 'usCommonCoreStandards';

  constructor(public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

  updateActiveLink(activeLink) {
    this.activeLink = activeLink;
  }

}
