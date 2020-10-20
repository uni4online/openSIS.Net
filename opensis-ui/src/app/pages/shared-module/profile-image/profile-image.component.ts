import { Component, OnInit, ViewChild, TemplateRef, Input } from '@angular/core';
import {ImageCroppedEvent,base64ToFile} from 'ngx-image-cropper';
import { MatDialog,MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import {fadeInRight400ms} from '../../../../@vex/animations/fade-in-right.animation';
import icCrop from '@iconify/icons-ic/crop';
import { ImageCropperService } from '../../../services/image-cropper.service';
import { SchoolService } from '../../../services/school.service';

@Component({
  selector: 'vex-profile-image',
  templateUrl: './profile-image.component.html',
  styleUrls: ['./profile-image.component.scss'],
   animations: [
    fadeInRight400ms
  ]
})
export class ProfileImageComponent implements OnInit {
  @ViewChild('mytemplate') mytemplate: TemplateRef<any>;
  @ViewChild('modalClickButton') modalClickButton: TemplateRef<any>;

  icCrop=icCrop;
  preview:String='';originalFileName:String;
  imageChangedEvent: any = '';
  croppedImage: any = '';
  showCropTool:Boolean=false;
  showCropperandButton:Boolean;
  // afterConvertingBase64toFile;
  fileUploader:any;
  hideCropperToolButton:Boolean=true;
  enableUpload:boolean;
  inputType:string="file";
  @Input() enableCropTool=true;
  @Input() responseImage; 
  @Input() mode; 

  constructor(private dialog:MatDialog,
    private _ImageCropperService:ImageCropperService,
    private snackbar: MatSnackBar,
    private schoolService:SchoolService) {
     }

     ngOnInit(): void {
      if(this.mode){
        this._ImageCropperService.sharedMessage.subscribe((message) => {
          this.enableUpload = message
          let id;
          
          id=this.schoolService.getSchoolId();
          
          if(this.enableUpload){
            this.inputType ="none";
            if(id!=null){
              this.inputType ="none";
            }
            if(id==null && this.enableUpload){
              this.inputType="file"
            }
          }else if(!this.enableUpload){
            this.inputType="file"
          }
        });
      }
     
    }
     
  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
    let base64ImageSplit=this.croppedImage.split(',')
    let sendCropImage=(croppedImage)=>{
      this._ImageCropperService.sendCroppedEvent(croppedImage);
      } 
    sendCropImage(base64ImageSplit);
  }

  setImage(){
    this.showCropperandButton=true;
  }
  unsetImage(){
    this.hideCropperToolButton=!this.hideCropperToolButton;
  }

  imageLoaded() {
    // console.log("Image is loaded!");
  }
  cropperReady() {
    // cropper ready
  }
  loadImageFailed() {
    // console.log("Failed to Load!!")
  }

  uploadFile(event,fileUpload){
  this.fileUploader=fileUpload;
  this.responseImage=null;
  this.hideCropperToolButton=true;
    if(event.target.files[0]?.size>307200){
      this.snackbar.open('Warning: File must be less than 300kb', '', {duration: 10000});
    }else if((event.target.files[0]?.type=="image/jpeg") || 
    (event.target.files[0]?.type=="image/jpg") || 
    (event.target.files[0]?.type=="image/png")){
      this.originalFileName = event.target.files[0].name;
      this.showCropTool = false;
      var files = event.target.files;
      var _URL = window.URL || window.webkitURL;
      for (var i = 0; i < files.length; i++) {
        var img = new Image();
        img.onload = () => {
          if (this.enableCropTool) {
            this.CallCropper(event);
          } else {
            this.CallUncropper(event);
          }
        }
        img.src = _URL.createObjectURL(files[i]);
      }
    }else{
      if(event.target.files[0]?.size>0){
      this.snackbar.open('Warning: Only jpg/jpeg/png will support', '', {duration: 10000});
      }
    }
  }

  CallCropper(event){
    this.croppedImage
    this.imageChangedEvent = event;
    this.showCropTool = true;
    this.openModal();
    return false;
  }

  CallUncropper(event){
    const file = (event.target as HTMLInputElement).files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = this.HandleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
    }
    const reader = new FileReader();
    reader.onload = () => {
      this.preview = reader.result as string;
    }
    reader.readAsDataURL(file)
  }

  HandleReaderLoaded(e) {
    this.croppedImage='';
    let sendImageData2 =(e)=>{
      this._ImageCropperService.sendUncroppedEvent(e);
      this.fileUploader.value=null;
      }
      sendImageData2(e);
  }

  openModal() {
    let dialogRef = this.dialog.open(this.mytemplate, {
        width: '700px',
    });

    dialogRef.afterClosed().subscribe(result => {
        // Do Something after Dialog Closed
    });
}

onClose(){
  this.hideCropperToolButton=false;
  this.fileUploader.value=null;
  // this.showCropperandButton=true;
  this.dialog.closeAll();
}

}
