import { Component, OnInit, Input } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
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
    { label: 'Parent Name', property: 'name', type: 'text', visible: true },
    { label: 'Email Address', property: 'email_address', type: 'text', visible: true },
    { label: 'Mobile Phone', property: 'mobile_phone', type: 'number', visible: true },
    { label: 'Username', property: 'username', type: 'text', visible: true },
    { label: 'action', property: 'action', type: 'text', visible: true }
  ];

  ParentFieldsModelList;

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icFilterList = icFilterList;
  loading:Boolean;

  constructor(private router: Router,private dialog: MatDialog,public translateService:TranslateService) {
    translateService.use('en');
    this.ParentFieldsModelList = [
      {name: 'Danielle Boucher', email_address: 'danielle.boucher@example.com', mobile_phone: '1234567980', username: 'danielleb'},
      {name: 'Andrew Brown', email_address: 'andrew_brown@example.com', mobile_phone: '1234567980', username: 'andrewbrown'},
      {name: 'Ella Brown', email_address: 'ella_brown@example.com', mobile_phone: '1234567980', username: 'ellabrown'},
      {name: 'Lian Fang', email_address: 'lian_fang@example.com', mobile_phone: '1234567980', username: 'lianfang'},
      {name: 'Adriana Garcia', email_address: 'adriana.garcia@example.com', mobile_phone: '1234567980', username: 'adrianagarcia'},
      {name: 'Olivia Jones', email_address: 'olivia.jones@example.com', mobile_phone: '1234567980', username: 'oliviajones'},
      {name: 'Amare Keita', email_address: 'amare_keita@example.com', mobile_phone: '1234567980', username: 'amarekeita'},
      {name: 'Amber Keita', email_address: 'amber_keita@example.com', mobile_phone: '1234567980', username: 'amberkeita'},
      {name: 'Alyssa Kimathi', email_address: 'alyssa_kimathi@example.com', mobile_phone: '1234567980', username: 'alyssakimathi'},
      {name: 'Robert Miller', email_address: 'robert_miller@example.com', mobile_phone: '1234567980', username: 'robertmiller'}
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
