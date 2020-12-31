import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import icSettings from '@iconify/icons-ic/twotone-settings';


@Component({
  selector: 'vex-course-manager',
  templateUrl: './course-manager.component.html',
  styleUrls: ['./course-manager.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class CourseManagerComponent implements OnInit {

  selectedOption = '1';
  icSettings = icSettings;

  constructor(public translateService:TranslateService) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

}
