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


}
