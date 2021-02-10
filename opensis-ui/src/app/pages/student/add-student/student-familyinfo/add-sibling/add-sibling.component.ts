import { Component, OnInit,Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder,NgForm,FormGroup, Validators } from '@angular/forms';
import icClose from '@iconify/icons-ic/twotone-close';
import icBack from '@iconify/icons-ic/baseline-arrow-back';
import { fadeInUp400ms } from '../../../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../../../@vex/animations/stagger.animation';
import { MatDialog } from '@angular/material/dialog';
import { fadeInRight400ms } from 'src/@vex/animations/fade-in-right.animation';
import { StudentSiblingAssociation, StudentSiblingSearch } from '../../../../../models/studentModel';
import { StudentService } from '../../../../../services/student.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AssociateStudent,AddParentInfoModel } from '../../../../../models/parentInfoModel';
import { relationShip} from '../../../../../enums/studentAdd.enum';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { ParentInfoService } from '../../../../../services/parent-info.service';
import { GetAllGradeLevelsModel } from '../../../../../models/gradeLevelModel';
import { GradeLevelService } from '../../../../../services/grade-level.service';
import { LovList } from '../../../../../models/lovModel';
import { CommonService } from '../../../../../services/common.service';
@Component({
  selector: 'vex-add-sibling',
  templateUrl: './add-sibling.component.html',
  styleUrls: ['./add-sibling.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms,
    fadeInRight400ms
  ]
})
export class AddSiblingComponent implements OnInit {
  icClose = icClose;
  icBack = icBack;
  form: FormGroup;
  associateStudents: NgForm;
  studentSiblingSearch:StudentSiblingSearch=new StudentSiblingSearch();
  getAllGradeLevelsModel:GetAllGradeLevelsModel=new GetAllGradeLevelsModel();
  studentSiblingAssociation:StudentSiblingAssociation = new StudentSiblingAssociation();
  hideSearchBoxAfterSearch=true;
  associatStudent:AssociateStudent=new AssociateStudent();
  associateMultipleStudents: NgForm;
  relationShipList = [];
  addParentInfoModel: AddParentInfoModel = new AddParentInfoModel();
  parentData;
  mode;
  gradeLevelArr;
  lovList: LovList = new LovList();
  constructor(private dialogRef: MatDialogRef<AddSiblingComponent>,
    private fb: FormBuilder,
    private studentService:StudentService,
    private snackbar: MatSnackBar,
    private parentInfoService:ParentInfoService,
    private gradeLevelService:GradeLevelService,
    private commonService:CommonService,
    @Inject(MAT_DIALOG_DATA) public val) { }

  ngOnInit(): void {
    this.getRelationship();
    this.getGradeLevel();
    this.form = this.fb.group(
      {
        firstGivenName: [null,Validators.required],
        lastFamilyName: [null,Validators.required],
        gradeLevel: [null],
        studentId:[null],
        dob:[null],
        searchAllSchool: [false]
      });
      
      
  }
  cancel(){
    this.dialogRef.close(false);
  }
  getRelationship(){
    
      this.lovList.lovName="Relationship";
      this.commonService.getAllDropdownValues(this.lovList).subscribe(
        (res:LovList)=>{      
          this.relationShipList = res.dropdownList; 
                
        }
      );
   
  }
  getGradeLevel(){
    this.getAllGradeLevelsModel.schoolId=+sessionStorage.getItem("selectedSchoolId");
    this.getAllGradeLevelsModel._tenantName=sessionStorage.getItem("tenant");
    this.getAllGradeLevelsModel._token=sessionStorage.getItem("token");
    this.gradeLevelService.getAllGradeLevels(this.getAllGradeLevelsModel).subscribe((res)=>{
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Grade Level Information failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (res._failure) {
          this.snackbar.open('Grade Level Information failed. ' + res._message, 'LOL THANKS', {
            duration: 10000
          });
        } else {
         
          this.gradeLevelArr = res.tableGradelevelList
          
        }
      }
    })
  }

  search(){
    this.form.markAllAsTouched();
    if(this.form.valid){
      if(this.form.value.searchAllSchool){
        this.studentSiblingSearch.schoolId=null
      }else{
        this.studentSiblingSearch.schoolId=+sessionStorage.getItem("selectedSchoolId")
      }
      if(this.studentSiblingSearch.studentInternalId==""){
        this.studentSiblingSearch.studentInternalId=null;
      }
      this.studentService.siblingSearch(this.studentSiblingSearch).subscribe((res)=>{
        if (typeof (res) == 'undefined') {
          this.snackbar.open('Student Search failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else {
          if (res._failure) {
            this.snackbar.open('Student Search failed. ' + res._message, 'LOL THANKS', {
              duration: 10000
            });
          } else {
            if(this.val !== null){
              if(this.val.data){
                this.mode="Parent";
              }
            }
            
            this.hideSearchBoxAfterSearch=false;
            this.studentSiblingSearch.getStudentForView=res.getStudentForView;
            
          }
        }
      })
    }
  }
  
  associateMultipleStudentsToParent(){
    var isCustodian=this.associatStudent.isCustodian;
    var contactRelationship=this.associatStudent.contactRelationship;  
    if(contactRelationship === undefined){
     contactRelationship = "";
    }   
    if(isCustodian === undefined){
     isCustodian = false
    }
    let obj = {
     'isCustodian':isCustodian,
     'relationship':contactRelationship,
     "tenantId": sessionStorage.getItem("tenantId"),
     "schoolId": +sessionStorage.getItem("selectedSchoolId"),
     "parentId": this.val.data.parentId
     
     }   
    return obj;
  }
  associateStudent(studentDetails){
   if(this.val !== null){   
    if(this.val.data !== null ){
      if(this.studentSiblingSearch.getStudentForView?.length>1){      
        let obj = this.associateMultipleStudentsToParent();   
        if(obj.relationship === ""){
          this.snackbar.open('Please provide Relationship','', {
            duration: 10000
          });
        }else
        {       
         
          this.addParentInfoModel.parentAssociationship.studentId = studentDetails.studentId;
          this.addParentInfoModel.parentInfo.parentAddress[0].studentId = studentDetails.studentId;
          this.addParentInfoModel.parentAssociationship.parentId = this.val.data.parentInfo.parentId;
          this.addParentInfoModel.parentInfo.parentAddress[0].parentId = this.val.data.parentInfo.parentId;
          this.addParentInfoModel.parentInfo.parentId = this.val.data.parentInfo.parentId;
          this.addParentInfoModel.parentAssociationship.relationship= obj.relationship; 
          this.addParentInfoModel.parentAssociationship.isCustodian = obj.isCustodian;            
          this.parentInfoService.addParentForStudent(this.addParentInfoModel).subscribe(data => {
            if (typeof (data) == 'undefined') 
            {
              this.snackbar.open('Parent Information Submission failed. ' + sessionStorage.getItem("httpError"), '', {
                duration: 10000
              });
            }
            else 
            {
              if (data._failure) {
                this.snackbar.open('Parent Information Submission failed. ' + data._message, 'LOL THANKS', {
                  duration: 10000
                });
              }
              else 
              {
                this.snackbar.open('Parent Information Submission Successful.', '', {
                duration: 10000
                });
                
                this.dialogRef.close(true); 
                
                           
              }
            }
          })     
        }
      }else{
        let isCustodian=this.associatStudent.isCustodian;
        let contactRelationship=this.associatStudent.contactRelationship;
        if(contactRelationship === undefined){
          contactRelationship = "";
         }   
         if(isCustodian === undefined){
          isCustodian = false
         }
         
         
         this.addParentInfoModel.parentAssociationship.relationship= contactRelationship; 
         this.addParentInfoModel.parentAssociationship.isCustodian = isCustodian;
          this.addParentInfoModel.parentAssociationship.studentId = studentDetails.studentId;
          this.addParentInfoModel.parentInfo.parentAddress[0].studentId = studentDetails.studentId;
          this.addParentInfoModel.parentAssociationship.parentId = this.val.data.parentInfo.parentId;
          this.addParentInfoModel.parentInfo.parentAddress[0].parentId = this.val.data.parentInfo.parentId;
          this.addParentInfoModel.parentInfo.parentId = this.val.data.parentInfo.parentId;
       
         this.parentInfoService.addParentForStudent(this.addParentInfoModel).subscribe(data => {
          if (typeof (data) == 'undefined') 
          {
            this.snackbar.open('Parent Information Submission failed. ' + sessionStorage.getItem("httpError"), '', {
              duration: 10000
            });
          }
          else 
          {
            if (data._failure) {
              this.snackbar.open('Parent Information Submission failed. ' + data._message, 'LOL THANKS', {
                duration: 10000
              });
            }
            else 
            {
              this.snackbar.open('Parent Information Submission Successful.', '', {
              duration: 10000
              });
              
              this.dialogRef.close(true); 
              
                         
            }
          }
        })    
      }
    } 
  }else{
      this.studentSiblingAssociation.studentMaster.studentId=studentDetails.studentId;
      this.studentSiblingAssociation.studentMaster.schoolId=studentDetails.schoolId;
      this.studentSiblingAssociation.studentId=this.studentService.getStudentId();
      this.studentService.associationSibling(this.studentSiblingAssociation).subscribe((res)=>{
        if (typeof (res) == 'undefined') {
          this.snackbar.open('Association failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else {
          if (res._failure) {
            this.snackbar.open('Association failed. ' + res._message, 'LOL THANKS', {
              duration: 10000
            });
          } else {  
            this.dialogRef.close(true);  
            this.snackbar.open('Sibling has been associated','Thanks', {
              duration: 10000
            });
          }
        }
      })
    }   
    
  }

  backToSearch(){
    if(this.hideSearchBoxAfterSearch){
      this.dialogRef.close();
    }else{
      this.hideSearchBoxAfterSearch=true;
      this.studentSiblingSearch.getStudentForView=null;
    }
  }
}
