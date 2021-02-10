import { Component, OnInit,Input,ViewChild } from '@angular/core';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/twotone-search';
import icAdd from '@iconify/icons-ic/twotone-add';
import icFilterList from '@iconify/icons-ic/twotone-filter-list';
import { TranslateService } from '@ngx-translate/core';
import { MatTableDataSource } from '@angular/material/table';
import { LoaderService } from '../../../../services/loader.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icComment from '@iconify/icons-ic/comment';
import icUpload from '@iconify/icons-ic/baseline-cloud-upload';
import {GetAllStudentDocumentsList} from '../../../../models/studentModel';
import {StudentDocumentAddModel} from '../../../../models/studentModel';
import {StudentService} from '../../../../services/student.service';
import {ConfirmDialogComponent } from '../../../shared-module/confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import * as _moment from 'moment';
import { default as _rollupMoment } from 'moment';
const moment =  _rollupMoment || _moment;
import { SchoolCreate } from 'src/app/enums/school-create.enum';

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
  StudentCreate=SchoolCreate;
  @Input() studentCreateMode:SchoolCreate;
  @Input() studentDetailsForViewAndEdit;
  @Input()
  columns = [
    { label: 'File', property: 'fileUploaded', type: 'text', visible: true },
    { label: 'Uploaded By', property: 'uploadedBy', type: 'number', visible: true },
    { label: 'Uploaded On', property: 'uploadedOn', type: 'text', visible: true },
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
  base64;
  base64Arr=[];
  filesName=[];
  filesType=[];
  uploadSuccessfull = false;
  totalCount:Number;pageNumber:Number;pageSize:Number;
  studentDocument=[];
  getAllStudentDocumentsList: GetAllStudentDocumentsList = new GetAllStudentDocumentsList();   
  StudentDocumentModelList: MatTableDataSource<any>;
  studentDocumentAddModel: StudentDocumentAddModel = new StudentDocumentAddModel();

  @ViewChild(MatSort) sort: MatSort;
  constructor(   
    public translateService:TranslateService, 
    private loaderService:LoaderService,
    private studentService:StudentService,
    private snackbar: MatSnackBar,
    private dialog: MatDialog,
  ) {
    translateService.use('en');    
    this.loaderService.isLoading.subscribe((val) => {
       this.loading = val;
     });
    
  }
  ngOnInit(): void {  
    this.getAllDocumentsList();
  }  
  //Split base 64 data from its datatype then push to base64 array
  HandleReaderLoaded(e) {   
    let str = e.substr(e.indexOf(",") + 1);
    this.base64Arr.push(str);  
  }
  onSelect(event) {  
    this.files.push(...event.addedFiles);
    let count = this.files.length;
    let prevCount = count-1;   
    for(let i=0; i<this.files.length;i++){
      this.filesName.push(this.files[i].name)
      this.filesType.push(this.files[i].type)
      const reader = new FileReader();
      reader.readAsDataURL(this.files[i]);
      reader.onload=()=>{
        this.HandleReaderLoaded(reader.result)
      }
    }  
  }

  
  onRemove(event) {    
    this.files.splice(this.files.indexOf(event), 1);
    this.filesName.splice(this.files.indexOf(event), 1);
    this.filesType.splice(this.files.indexOf(event), 1);
    this.base64Arr.splice(this.files.indexOf(event), 1);
    
  }

  confirmDelete(deleteDetails)
  { 
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Are you sure?",
          message: "You are about to delete File "+deleteDetails.filename+"."}
    });
    
    dialogRef.afterClosed().subscribe(dialogResult => {      
      if(dialogResult){
        this.deleteFile(deleteDetails);
      }
    });
  }
  deleteFile(deleteDetails){
    let studentDocument = [];
    var obj = {};   
    obj = {     
      tenantId: deleteDetails.tenantId,
      schoolId: deleteDetails.schoolId,
      studentId: deleteDetails.studentId,
      documentId:deleteDetails.documentId,
      fileUploaded:deleteDetails.fileUploaded,
      uploadedOn:deleteDetails.uploadedOn,
      uploadedBy:deleteDetails.uploadedBy,
      studentMaster: null
    }   
    studentDocument.push(obj);
    this.studentDocumentAddModel.studentDocuments=studentDocument
    this.studentService.DeleteStudentDocument(this.studentDocumentAddModel).subscribe(data => {
      if (typeof (data) == 'undefined') {
        this.snackbar.open('File Deletion failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (data._failure) {
          this.snackbar.open('File Deletion failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
          this.snackbar.open('File Deletion Successful.', '', {
            duration: 10000
          }).afterOpened().subscribe(data => {
            this.getAllDocumentsList();
          });
        }
      }
  
    })
  }

  uploadFile(){
    
    this.base64Arr.forEach((value, index) => {
        var obj = {};
          obj = {     
            tenantId: sessionStorage.getItem("tenantId"),
            schoolId: +sessionStorage.getItem("selectedSchoolId") ,
            studentId: this.studentDetailsForViewAndEdit.studentMaster.studentId,
            documentId: 0,
            filename: this.filesName[index],
            filetype: this.filesType[index],
            fileUploaded:value,          
            uploadedBy:sessionStorage.getItem("email"),
            studentMaster: null
          }   
          this.studentDocument.push(obj);    
      }); 
     
      
     if(this.studentDocument.length > 0){
       
        this.studentDocumentAddModel.studentDocuments=this.studentDocument;
        this.studentService.AddStudentDocument(this.studentDocumentAddModel).subscribe(data => {
          if (typeof (data) == 'undefined') {
            this.snackbar.open('Student Document Upload failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else {
            if (data._failure) {
              this.snackbar.open('Student Document Upload failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            } else {          
              this.snackbar.open('Student Document Upload Successful.', '', {
                duration: 10000
              }).afterOpened().subscribe(data => {
                this.base64Arr=[];
                this.studentDocument = [];
                this.filesName=[];
                this.filesType=[];
                this.uploadSuccessfull = true;
                this.isShowDiv=true;
                this.getAllDocumentsList();
                this.files = [];
                
              });                  
            }
          }
        });
      } else{
        this.snackbar.open('Please Select File', 'LOL THANKS', {
          duration: 1000
        });
      }
      
  }

  downloadFile(name,type,content){
    let fileType = "data:"+type+";base64," + content;   
    var element = document.createElement('a');
    element.setAttribute('href', fileType);
    element.setAttribute('download', name);
    element.style.display = 'none';
    document.body.appendChild(element);
    element.click();
    document.body.removeChild(element);
}
  getAllDocumentsList(){
    this.getAllStudentDocumentsList.studentId = this.studentDetailsForViewAndEdit.studentMaster.studentId;
    this.studentService.GetAllStudentDocumentsList(this.getAllStudentDocumentsList).subscribe(data => {
      if(data._failure){
        if(data._message==="NO RECORD FOUND"){
          if(data.studentDocumentsList==null){
            this.StudentDocumentModelList=new MatTableDataSource([]) ;
        this.StudentDocumentModelList.sort=this.sort;     
          }
        } else{
          this.snackbar.open('Student Document failed. ' + data._message, 'LOL THANKS', {
            duration: 10000
          });
        }
      }else{   
        
        this.StudentDocumentModelList = new MatTableDataSource(data.studentDocumentsList);
       
        this.StudentDocumentModelList.sort=this.sort;      
      }
    });
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  toggleDisplayDiv() {
    this.isShowDiv = !this.isShowDiv;
    this.uploadSuccessfull = false;
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

}
