import { Component, OnInit } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import { HttpClient } from 'selenium-webdriver/http';
import { SchoolService } from '../../../../services/school.service';
import { SchoolViewModel, SchoolListViewModel } from 'src/app/models/schoolModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router} from '@angular/router';
@Component({
  selector: 'vex-school-details',
  templateUrl: './school-details.component.html',
  styleUrls: ['./school-details.component.scss']
})
export class SchoolDetailsComponent implements OnInit {
  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icSearch = icSearch;
  icFilterList = icFilterList;
  fapluscircle = "fa-plus-circle";
  tenant = "";
  SchoolModel: SchoolViewModel = new SchoolViewModel();
  SchoolModelList: SchoolListViewModel = new SchoolListViewModel();
  constructor(private schoolService: SchoolService,
    private snackbar: MatSnackBar,
    private router: Router
    ) 
    { 

     this.getSchooldetails();

  }

  ngOnInit(): void {
  }
  goToAdd(){
    this.router.navigate(["school/schoolinfo/add-school"]);
  }  
  getSchooldetails()
  {
    this.SchoolModel._tenantName=sessionStorage.getItem("tenant");
    this.SchoolModel._token=sessionStorage.getItem("token");
    this.schoolService.GetSchool(this.SchoolModel).subscribe(data => {
      if(data._failure){
        this.snackbar.open('School information failed. '+ data._message, 'LOL THANKS', {
        duration: 10000
        });
        // this.notifier.notify( 'error', 'Login failed. '+ data._message );
      }else{
        console.log(data);
        this.SchoolModelList=data;
      }
    })

  }
}
