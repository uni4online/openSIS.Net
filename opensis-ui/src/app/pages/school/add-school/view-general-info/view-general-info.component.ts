import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SchoolCreate } from '../../../../enums/school-create.enum';
import { SchoolAddViewModel } from '../../../../models/schoolMasterModel';

@Component({
  selector: 'vex-view-general-info',
  templateUrl: './view-general-info.component.html',
  styleUrls: ['./view-general-info.component.scss']
})

export class ViewGeneralInfoComponent implements OnInit {
  @Input() schoolCreateMode: SchoolCreate;
  @Input() categoryId;
  @Input() schoolViewDetails: SchoolAddViewModel;
  module = "School";
  status: string;
  mapUrl:string;
  mailText:string;
  constructor(private snackbar: MatSnackBar) {
  }

  ngOnInit(): void {
    if (this.schoolViewDetails.schoolMaster.schoolDetail[0].status != null) {
      this.status = this.schoolViewDetails.schoolMaster.schoolDetail[0].status ? 'Active' : 'Inactive';
    }
  }

  showOnGoogleMap(){
    let longitude=this.schoolViewDetails.schoolMaster.longitude;
    let latitude=this.schoolViewDetails.schoolMaster.latitude;
    if(longitude && latitude){
      this.mapUrl=`https://maps.google.com/?q=${latitude},${longitude}`
      window.open(this.mapUrl,'_blank');
    }else{
      this.snackbar.open('Invalid Longitude, Latitude.', 'Ok', {
        duration: 5000
      });
    }
  }
  goToWebsite(){
    window.open(this.schoolViewDetails?.schoolMaster.schoolDetail[0].website, "_blank");
  }
  goToTwitter(){
    window.open(this.schoolViewDetails?.schoolMaster.schoolDetail[0].twitter, "_blank");
    
  }
  goToFacebook(){
    window.open(this.schoolViewDetails?.schoolMaster.schoolDetail[0].facebook, "_blank");
   
  }

  goToInstagram(){
    window.open(this.schoolViewDetails?.schoolMaster.schoolDetail[0].instagram, "_blank");
    
  }
  goToYoutube(){
    window.open(this.schoolViewDetails?.schoolMaster.schoolDetail[0].youtube, "_blank");
   
  }
  goToLinkedin(){
    window.open(this.schoolViewDetails?.schoolMaster.schoolDetail[0].linkedIn, "_blank");
   
  }
  goToEmail(){
    this.mailText = "mailto:"+this.schoolViewDetails?.schoolMaster.schoolDetail[0].email;
    window.open(this.mailText, "_blank");
    
  }

}
