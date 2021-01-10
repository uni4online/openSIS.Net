import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import icSettings from '@iconify/icons-ic/twotone-settings';
import icAdd from '@iconify/icons-ic/baseline-add';
import icMoreVertical from '@iconify/icons-ic/baseline-more-vert';
import icRemoveCircle from '@iconify/icons-ic/remove-circle';
import icBack from '@iconify/icons-ic/baseline-arrow-back';
import icInfo from '@iconify/icons-ic/info';
import icCheckboxChecked from '@iconify/icons-ic/check-box';
import icCheckboxUnchecked from '@iconify/icons-ic/check-box-outline-blank';
import { MatDialog } from '@angular/material/dialog';
import { ManageSubjectsComponent } from './manage-subjects/manage-subjects.component';
import { ManageProgramsComponent } from './manage-programs/manage-programs.component';
import { EditCourseComponent } from './edit-course/edit-course.component';
import { EditCourseSectionComponent } from './edit-course-section/edit-course-section.component';


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
  icAdd = icAdd;
  icMoreVertical = icMoreVertical;
  icRemoveCircle = icRemoveCircle;
  icInfo = icInfo;
  icBack = icBack;
  icCheckboxChecked = icCheckboxChecked;
  icCheckboxUnchecked = icCheckboxUnchecked;
  showCourses = false;

  constructor(public translateService:TranslateService, private dialog: MatDialog) {
    translateService.use('en');
  }

  ngOnInit(): void {
  }

  backToCourse() {
    this.showCourses = true;
  }

  courseSections() {
    this.showCourses = false;
  }

  openModalManageSubjects() {
    this.dialog.open(ManageSubjectsComponent, {
      width: '500px'
    });
  }

  openModalManagePrograms() {
    this.dialog.open(ManageProgramsComponent, {
      width: '500px'
    });
  }

  openModalEditCourse() {
    this.dialog.open(EditCourseComponent, {
      width: '800px'
    });
  }

  openModalEditCourseSection() {
    this.dialog.open(EditCourseSectionComponent, {
      width: '900px'
    });
  }

}
