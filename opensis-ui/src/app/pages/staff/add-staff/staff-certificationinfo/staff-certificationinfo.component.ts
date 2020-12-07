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
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import { AddCertificateComponent } from './add-certificate/add-certificate.component';
import { CertificateDetailsComponent } from './certificate-details/certificate-details.component';

@Component({
  selector: 'vex-staff-certificationinfo',
  templateUrl: './staff-certificationinfo.component.html',
  styleUrls: ['./staff-certificationinfo.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class StaffCertificationinfoComponent implements OnInit {
  @Input()
  columns = [
    { label: 'Certification Name', property: 'certification_name', type: 'text', visible: true },
    { label: 'Short Name', property: 'short_name', type: 'text', visible: true },
    { label: 'Primary Certification Indicator', property: 'primary_certification_indicator', type: 'text', visible: true },
    { label: 'Certification Date', property: 'certification_date', type: 'text', visible: true },
    { label: 'Certification Expiry Date', property: 'certification_expiry_date', type: 'text', visible: true },
    { label: 'Actions', property: 'actions', type: 'text', visible: true }
  ];

  StaffCertificationFieldsModelList;

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
    this.StaffCertificationFieldsModelList = [
      {certification_name: 'Masters of Science', short_name: 'M.Sc.', primary_certification_indicator: 'No', certification_date: 'Mar 10, 2001', certification_expiry_date: '-'},
      {certification_name: 'Bachelor of Science', short_name: 'B.Sc.', primary_certification_indicator: 'Yes', certification_date: 'Mar 01, 2004', certification_expiry_date: '-'},
    ]
  }

  ngOnInit(): void {
  }

  getPageEvent(event){    
    // this.getAllSchool.pageNumber=event.pageIndex+1;
    // this.getAllSchool.pageSize=event.pageSize;
    // this.callAllSchool(this.getAllSchool);
  }

  openAddNew() {
    this.dialog.open(AddCertificateComponent, {
      data: null,
      width: '600px'
    });
  }

  openViewDetails() {
    this.dialog.open(CertificateDetailsComponent, {
      data: null,
      width: '600px'
    });
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
