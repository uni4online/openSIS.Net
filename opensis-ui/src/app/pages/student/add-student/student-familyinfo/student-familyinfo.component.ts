import { Component, OnInit ,Input} from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icAdd from '@iconify/icons-ic/baseline-add';
import { SchoolCreate } from '../../../../enums/school-create.enum';
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
  @Input() studentCreateMode:SchoolCreate;
  @Input() studentDetailsForViewAndEdit;
  icEdit = icEdit;
  icDelete = icDelete;
  icAdd = icAdd;
  studentDetailsForViewAndEditData;
  constructor(public translateService:TranslateService) { 
  }

  ngOnInit(): void {
    this.studentDetailsForViewAndEditData = this.studentDetailsForViewAndEdit;
  }


}
