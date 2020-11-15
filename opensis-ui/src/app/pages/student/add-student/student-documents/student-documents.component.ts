import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import { Router} from '@angular/router';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icAdd from '@iconify/icons-ic/baseline-add';
import icComment from '@iconify/icons-ic/twotone-comment';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import icUpload from '@iconify/icons-ic/publish';

@Component({
  selector: 'vex-student-documents',
  templateUrl: './student-documents.component.html',
  styleUrls: ['./student-documents.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class StudentDocumentsComponent implements OnInit {
  @Input()
  columns = [
    { label: 'File', property: 'file', type: 'text', visible: true },
    { label: 'Uploaded By', property: 'uploaded_by', type: 'number', visible: true },
    { label: 'Uploaded On', property: 'uploaded_on', type: 'text', visible: true },
    { label: 'Action', property: 'action', type: 'text', visible: true }
  ];

  StudentDocumentsList;

  icEdit = icEdit;
  icDelete = icDelete;
  icAdd = icAdd;
  icComment = icComment;
  icMoreVert = icMoreVert;
  icSearch = icSearch;
  icFilterList = icFilterList;
  icUpload = icUpload;
  isShowDiv = true;
  loading:Boolean;
  files: File[] = [];

  constructor(private router: Router, private fb: FormBuilder, public translateService:TranslateService) {
    translateService.use('en');
    this.StudentDocumentsList = [
      {file: 'certificate.pdf', uploaded_by: 'Super Administrator', uploaded_on: 'Mar 10, 2020'},
      {file: 'grade9_marksheet.pdf', uploaded_by: 'Super Administrator', uploaded_on: 'Mar 10, 2020'},
      {file: 'birth_certificate.pdf', uploaded_by: 'Super Administrator', uploaded_on: 'Mar 11, 2020'},
      {file: 'certificate_2.pdf', uploaded_by: 'Super Administrator', uploaded_on: 'Mar 10, 2020'}
    ]
   }


  ngOnInit(): void {
  }
 
  onSelect(event) {
    console.log(event);
    this.files.push(...event.addedFiles);
  }
  
  onRemove(event) {
    console.log(event);
    this.files.splice(this.files.indexOf(event), 1);
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

  toggleDisplayDiv() {
    this.isShowDiv = !this.isShowDiv;
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

}
