import { Component, Input, OnInit } from '@angular/core';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import icCheckBoxOutlineBlank from '@iconify/icons-ic/check-box-outline-blank';
import icCheckBox from '@iconify/icons-ic/check-box';
import { StudentAddModel } from '../../../../models/studentModel';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';

@Component({
  selector: 'vex-view-student-addressandcontacts',
  templateUrl: './view-student-addressandcontacts.component.html',
  styleUrls: ['./view-student-addressandcontacts.component.scss'],
  animations: [
    stagger60ms
  ],
})
export class ViewStudentAddressandcontactsComponent implements OnInit {
  icCheckBoxOutlineBlank = icCheckBoxOutlineBlank;
  icCheckBox = icCheckBox;

  @Input() studentCreateMode: SchoolCreate;
  @Input() studentViewDetails: StudentAddModel;
  @Input() nameOfMiscValues;
  constructor() { }

  ngOnInit(): void {
  }

}
