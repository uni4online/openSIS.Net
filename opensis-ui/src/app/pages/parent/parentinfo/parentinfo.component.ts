import { Component, OnInit, Input } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import icImpersonate from '@iconify/icons-ic/twotone-account-circle';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router} from '@angular/router';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'vex-parentinfo',
  templateUrl: './parentinfo.component.html',
  styleUrls: ['./parentinfo.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class ParentinfoComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Parent’s Name', property: 'name', type: 'text', visible: true },
    { label: 'Profile', property: 'profile', type: 'text', visible: true },
    { label: 'Email Address', property: 'email_address', type: 'text', visible: true },
    { label: 'Mobile Phone', property: 'mobile_phone', type: 'number', visible: true },
    { label: 'Associated Students', property: 'students', type: 'text', visible: true },
    { label: 'action', property: 'action', type: 'text', visible: true }
  ];

  ParentFieldsModelList;

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;
  icImpersonate = icImpersonate;
  loading:Boolean;

  constructor(private router: Router,private dialog: MatDialog,public translateService:TranslateService) {
    translateService.use('en');
    this.ParentFieldsModelList = [
      {name: 'Danielle Boucher', profile: 'Parent', email_address: 'danielle.boucher@example.com', mobile_phone: '1234567980', students: [{'student_name' : 'William Boucher', 'student_id' : '1'}]},
      {name: 'Andrew Brown', profile: 'Parent', email_address: 'andrew_brown@example.com', mobile_phone: '1234567980', students: [{'student_name' : 'Natalie Brown', 'student_id' : '1'},{'student_name' : 'Gabriel Brown', 'student_id' : '1'}]},
      {name: 'Ella Brown', profile: 'Parent', email_address: 'ella_brown@example.com', mobile_phone: '1234567980', students: [{'student_name' : 'Lilly Brown', 'student_id' : '1'}]},
      {name: 'Lian Fang', profile: 'Parent', email_address: 'lian_fang@example.com', mobile_phone: '1234567980', students: [{'student_name' : 'Liliana Fang', 'student_id' : '1'}]},
      {name: 'Adriana Garcia', profile: 'Parent', email_address: 'adriana.garcia@example.com', mobile_phone: '1234567980', students: [{'student_name' : 'Melissa Garcia', 'student_id' : '1'},{'student_name' : 'Delaney Garcia', 'student_id' : '1'}]},
      {name: 'Olivia Jones', profile: 'Parent', email_address: 'olivia.jones@example.com', mobile_phone: '1234567980', students: [{'student_name' : 'Erick Jones', 'student_id' : '1'}]},
      {name: 'Amare Keita', profile: 'Parent', email_address: 'amare_keita@example.com', mobile_phone: '1234567980', students: [{'student_name' : 'Braylen Keita', 'student_id' : '1'}]},
      {name: 'Amber Keita', profile: 'Parent', email_address: 'amber_keita@example.com', mobile_phone: '1234567980', students: [{'student_name' : 'amberkeita', 'student_id' : '1'}]},
      {name: 'Alyssa Kimathi', profile: 'Parent', email_address: 'alyssa_kimathi@example.com', mobile_phone: '1234567980', students: [{'student_name' : 'Athena Kimathi', 'student_id' : '1'}]},
      {name: 'Robert Miller', profile: 'Parent', email_address: 'robert_miller@example.com', mobile_phone: '1234567980', students: [{'student_name' : 'Patrick Miller', 'student_id' : '1'}]}
    ]
  }

  ngOnInit(): void {
  }

  getPageEvent(event){    
    // this.getAllSchool.pageNumber=event.pageIndex+1;
    // this.getAllSchool.pageSize=event.pageSize;
    // this.callAllSchool(this.getAllSchool);
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

}
