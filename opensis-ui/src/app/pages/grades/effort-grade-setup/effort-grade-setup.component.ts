import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'vex-effort-grade-setup',
  templateUrl: './effort-grade-setup.component.html',
  styleUrls: ['./effort-grade-setup.component.scss']
})
export class EffortGradeSetupComponent implements OnInit {

  activeLink = 'effortGradeLibrary';

  constructor(public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

  updateActiveLink(activeLink) {
    this.activeLink = activeLink;
  }

}
