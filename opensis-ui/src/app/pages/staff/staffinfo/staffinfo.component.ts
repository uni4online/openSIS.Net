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
  selector: 'vex-staffinfo',
  templateUrl: './staffinfo.component.html',
  styleUrls: ['./staffinfo.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class StaffinfoComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Name', property: 'name', type: 'text', visible: true },
    { label: 'Staff ID', property: 'staff_id', type: 'text', visible: true },
    { label: 'openSIS Profile', property: 'profile', type: 'text', visible: true },
    { label: 'Job Title', property: 'job_title', type: 'text', visible: true },
    { label: 'School Email', property: 'school_email', type: 'text', visible: true },
    { label: 'Mobile Phone', property: 'mobile_phone', type: 'number', visible: true },
    { label: 'Actions', property: 'actions', type: 'text', visible: true }
  ];

  StaffFieldsModelList;

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icImpersonate = icImpersonate;
  icFilterList = icFilterList;
  loading:Boolean;

  constructor(private router: Router,private dialog: MatDialog,public translateService:TranslateService) {
    translateService.use('en');
    this.StaffFieldsModelList = [
      {name: 'Danielle Boucher', staff_id: '1', profile: 'Super Administrator', job_title: 'Head Teacher', school_email: 'danielle.boucher@example.com', mobile_phone: '1234567980', last_login: 'Aug 21, 2020, 03:39 PM'},
      {name: 'Andrew Brown', staff_id: '2', profile: 'Teacher', job_title: 'Asst. Teacher', school_email: 'andrew_brown@example.com', mobile_phone: '1234567980', last_login: 'Aug 21, 2020, 12:51 PM'},
      {name: 'Ella Brown', staff_id: '3', profile: 'Teacher', job_title: 'Asst. Teacher', school_email: 'ella_brown@example.com', mobile_phone: '1234567980', last_login: 'Aug 21, 2020, 04:17 PM'},
      {name: 'Lian Fang', staff_id: '4', profile: 'Teacher', job_title: 'Asst. Teacher', school_email: 'lian_fang@example.com', mobile_phone: '1234567980', last_login: 'Aug 21, 2020, 14:08 PM'},
      {name: 'Adriana Garcia', staff_id: '5', profile: 'Teacher', job_title: 'Asst. Teacher', school_email: 'adriana.garcia@example.com', mobile_phone: '1234567980', last_login: 'Aug 20, 2020, 05:55 PM'},
      {name: 'Olivia Jones', staff_id: '6', profile: 'Teacher', job_title: 'Asst. Teacher', school_email: 'olivia.jones@example.com', mobile_phone: '1234567980', last_login: 'Aug 21, 2020, 3:29 PM'},
      {name: 'Amare Keita', staff_id: '7', profile: 'Teacher', job_title: 'Asst. Teacher', school_email: 'amare_keita@example.com', mobile_phone: '1234567980', last_login: 'Aug 21, 2020, 04:10 PM'},
      {name: 'Amber Keita', staff_id: '8', profile: 'Teacher', job_title: 'Asst. Teacher', school_email: 'amber_keita@example.com', mobile_phone: '1234567980', last_login: 'Aug 21, 2020, 02:17 PM'},
      {name: 'Alyssa Kimathi', staff_id: '9', profile: 'Teacher', job_title: 'Asst. Teacher', school_email: 'alyssa_kimathi@example.com', mobile_phone: '1234567980', last_login: 'Aug 21, 2020, 03:01 PM'},
      {name: 'Robert Miller', staff_id: '10', profile: 'Teacher', job_title: 'Asst. Teacher', school_email: 'robert_miller@example.com', mobile_phone: '1234567980', last_login: 'Aug 21, 2020, 03:39 PM'}
    ]
  }

  ngOnInit(): void {
  }

  getPageEvent(event){    
    // this.getAllSchool.pageNumber=event.pageIndex+1;
    // this.getAllSchool.pageSize=event.pageSize;
    // this.callAllSchool(this.getAllSchool);
  }

  goToAdd(){
    this.router.navigate(["school/staff/add-staff"]);
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
