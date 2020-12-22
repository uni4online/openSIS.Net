import { Component, OnInit, ChangeDetectorRef,Input } from '@angular/core';
import { FormBuilder,NgForm } from '@angular/forms';
import { fadeInUp400ms } from '../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../@vex/animations/stagger.animation';
import { fadeInRight400ms } from '../../../../../@vex/animations/fade-in-right.animation';
import { TranslateService } from '@ngx-translate/core';
import icAdd from '@iconify/icons-ic/baseline-add';
import icClear from '@iconify/icons-ic/baseline-clear';
import icVisibility from '@iconify/icons-ic/twotone-visibility';
import icVisibilityOff from '@iconify/icons-ic/twotone-visibility-off';
import { salutation,suffix ,relationShip,userProfile} from '../../../../enums/studentAdd.enum';
import { AddParentInfoModel,ParentInfoList,RemoveAssociateParent } from '../../../../models/parentInfoModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ParentInfoService } from '../../../../services/parent-info.service';
import { StudentService } from '../../../../services/student.service';
import icEdit from '@iconify/icons-ic/edit';
import icDelete from '@iconify/icons-ic/delete';
import icRemove from '@iconify/icons-ic/remove-circle';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AddSiblingComponent } from '../../../student/add-student/student-familyinfo/add-sibling/add-sibling.component';
import {ViewSiblingComponent} from '../../../student/add-student/student-familyinfo/view-sibling/view-sibling.component';
import { ConfirmDialogComponent } from '../../../shared-module/confirm-dialog/confirm-dialog.component';
import {StudentSiblingAssociation} from '../../../../models/studentModel';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
@Component({
  selector: 'vex-editparent-generalinfo',
  templateUrl: './editparent-generalinfo.component.html',
  styleUrls: ['./editparent-generalinfo.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class EditparentGeneralinfoComponent implements OnInit {
  @Input() parentDetailsForViewAndEdit;
  
  icAdd = icAdd;
  icClear = icClear;
  icVisibility = icVisibility;
  icVisibilityOff = icVisibilityOff;  
  icEdit = icEdit;
  icDelete = icDelete;
  icRemove = icRemove;
  inputType = 'password';
  visible = false;
  salutationEnum=Object.keys(salutation);
  suffixEnum = Object.keys(suffix);
  relationShipEnum = Object.keys(relationShip); 
  userProfileEnum = Object.keys(userProfile);
  f: NgForm;
  isPortalUser=false;
  parentDetails;
  mode="view";
  associateStudentMode="";
  addParentInfoModel: AddParentInfoModel = new AddParentInfoModel(); 
  parentInfoList:ParentInfoList=new ParentInfoList();
  studentSiblingAssociation:StudentSiblingAssociation=new StudentSiblingAssociation();
  removeAssociateParent:RemoveAssociateParent=new RemoveAssociateParent();
  destroySubject$: Subject<void> = new Subject();
  parentInfo;
  studentInfo;
  
  constructor(
    private fb: FormBuilder, 
    public translateService:TranslateService, 
    private cd: ChangeDetectorRef,  
    private parentInfoService:ParentInfoService,
    private snackbar: MatSnackBar,  
    private router:Router,
    private dialog: MatDialog,
    private studentService:StudentService
    ) {
    translateService.use('en');
    
  }

  ngOnInit(): void {
    this.parentInfo = {};
    if(this.parentDetailsForViewAndEdit.parentInfo.hasOwnProperty('firstname')){
      this.addParentInfoModel = this.parentDetailsForViewAndEdit;     
      this.parentInfo = this.addParentInfoModel.parentInfo;
      this.studentInfo = this.addParentInfoModel.getStudentForView;    
    }else{    
      this.parentInfoService.getParentDetailsForGeneral.subscribe((res: AddParentInfoModel) => {
        this.addParentInfoModel = res;
        this.parentInfo = this.addParentInfoModel.parentInfo;
        this.studentInfo = this.addParentInfoModel.getStudentForView;     
      })
    } 
    if(this.parentInfo.middlename === null){
      this.parentInfo.middlename = " ";      
      }
      if(this.parentInfo.salutation === null){
      this.parentInfo.salutation = " ";      
      }
      if(this.studentInfo !== undefined){
        this.studentInfo.forEach(element => {
          if(element.middleName === null){
            element.middleName="";
          } 
        }); 
      }
         
      if(this.parentInfo.isPortalUser === true){
      this.isPortalUser = true;
      this.addParentInfoModel.parentInfo.isPortalUser = true; 
      }else{
      this.isPortalUser = false;
      this.addParentInfoModel.parentInfo.isPortalUser = false; 
      }
      if(this.parentInfo){
      this.mode = "view";     
      }
  }

  portalUserCheck(event){
    if(event.checked === true){
      this.isPortalUser = true;
      this.addParentInfoModel.parentInfo.isPortalUser = true; 
    }else{
      this.isPortalUser = false;
      this.addParentInfoModel.parentInfo.isPortalUser = false; 
    }
  }

  editGeneralInfo(){
    this.mode = "add";   
    this.addParentInfoModel.parentInfo = this.parentInfo;
  }
  
  submit()
  {  
    this.parentInfoService.updateParentInfo(this.addParentInfoModel).subscribe(data => {
      if (typeof (data) == 'undefined') 
      {
        this.snackbar.open('Parent Information Updation failed. ' + sessionStorage.getItem("httpError"), '', {
        duration: 10000
        });
      }
      else 
      {
        if (data._failure) {
          this.snackbar.open('Parent Information Updation failed. ' + data._message, 'LOL THANKS', {
          duration: 10000
          });
        }
        else 
        {
          this.snackbar.open('Parent Information Updation Successful.', '', {
          duration: 10000
          });
          this.router.navigateByUrl('/school/parents');        
        }
      }
    })     
  }

  openViewDetails(studentDetails) {
    this.dialog.open(ViewSiblingComponent, {
      data:{     
        siblingDetails:studentDetails,
        flag:"Parent"
      },
       width: '600px'
     })
  }
  
  associateStudent(){
    this.associateStudentMode="search";
    this.dialog.open(AddSiblingComponent, {
     data:{      
      data:this.parentDetails
     },
      width: '600px'
    }).afterClosed().subscribe(data => {
      if(data){
        this.router.navigateByUrl('/school/parents');  
      }
                 
    });
  }

  confirmDelete(deleteDetails){     
    // call our modal window
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Are you sure?",
          message: "You are about to delete "+deleteDetails.firstGivenName+" "+deleteDetails.lastFamilyName+"."}
    });
    // listen to response
    dialogRef.afterClosed().subscribe(dialogResult => {
      // if user pressed yes dialogResult will be true, 
      // if user pressed no - it will be false
      if(dialogResult){
        this.deleteParentInfo(deleteDetails.studentId);
      }
   });
  }
  deleteParentInfo(studentId){  
    this.removeAssociateParent.studentId=studentId;
    this.removeAssociateParent.parentInfo.studentId=studentId;
    this.removeAssociateParent.parentInfo.parentId=this.parentInfo.parentId;
    this.parentInfoService.removeAssociatedParent(this.removeAssociateParent).subscribe(
      data => { 
        if(typeof(data)=='undefined'){
          this.snackbar.open('Student Information failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (data._failure) {  
            
            this.snackbar.open('Student Information failed. ' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          } 
          else {       
            this.snackbar.open('Student Deletion Successful.', '', {
              duration: 10000
            }).afterOpened().subscribe(data => {
              this.router.navigateByUrl('/school/parents');   
            });
            
          }
        }
      })
  }


  
  toggleVisibility() {
    if (this.visible) {
    this.inputType = 'password';
    this.visible = false;
    this.cd.markForCheck();
    } else {
    this.inputType = 'text';
    this.visible = true;
    this.cd.markForCheck();
    }
  }

}