import { Component, OnInit} from '@angular/core';
import { FormControl } from '@angular/forms';
import {ReplaySubject, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { YEAR, YEARS, PART, PARTS } from './year-data';
import { SchoolService } from '../../../../app/services/school.service';
import { AllSchoolListModel,OnlySchoolListModel } from 'src/app/models/getAllSchoolModel';
import { Router } from '@angular/router';

@Component({
  selector: 'vex-select-bar',
  templateUrl: './select-bar.component.html',
  styleUrls: ['./select-bar.component.scss']
})
export class SelectBarComponent implements OnInit {
  getSchoolList:OnlySchoolListModel = new OnlySchoolListModel();
  schools;
  // List of Years
  public years: YEAR[] = YEARS;
  // List of Parts
  public parts: PART[] = PARTS;
  schoolCtrl: FormControl;
  /** control for the selected Year */
  public yearCtrl: FormControl = new FormControl();
  filteredSchoolsForDrop;
  /** control for the selected Part */
  public partCtrl: FormControl = new FormControl();
  schoolFilterCtrl: FormControl;

  /** list of schools filtered by search keyword */
  public filteredSchools: ReplaySubject<AllSchoolListModel[]> = new ReplaySubject<AllSchoolListModel[]>(1);

  /** Subject that emits when the component has been destroyed. */
  protected _onDestroy = new Subject<void>();

  constructor(private _schoolService: SchoolService,
      private router:Router
    ) {
    this.getSchoolList.tenantId = sessionStorage.getItem("tenantId");
    this.getSchoolList._tenantName = sessionStorage.getItem("tenant");
    this.getSchoolList._token = sessionStorage.getItem("token");
   
      this._schoolService.GetAllSchools(this.getSchoolList).subscribe((data) => {
        this.schools = data.getSchoolForView;
        /** control for the selected School */
        this.schoolCtrl = new FormControl();
        this.schoolFilterCtrl= new FormControl();
        // set initial selection
        this.schoolCtrl.setValue(this.schools[0]);
    // load the initial School list
        this.filteredSchools.next(this.schools.slice());
        /** control for the MatSelect filter keyword */
        this.schoolFilterCtrl.valueChanges
          .pipe(takeUntil(this._onDestroy))
          .subscribe(() => {
            this.filterSchools();
          });
          // Beacause of Reload in Setting, we have to check the existing id to retrieve
          // school name from dropdown.
          if(!sessionStorage.getItem("selectedId")){
            sessionStorage.setItem("selectedId",this.schools[0].school_Id);
          }else{
            let id = parseInt(sessionStorage.getItem("selectedId"));
            let index = this.schools.findIndex((x) => {
              return x.school_Id === id
            });
            if(index!=-1){
              this.schoolCtrl.setValue(this.schools[index]);
            }else{
              this.schoolCtrl.setValue(this.schools[0]);
            }
          }
      })
  }
  changeSchool(name,id){
    sessionStorage.setItem("selectedId",id);
    this.router.navigate(['/school/dashboards']);
  }

  ngOnInit() {
    // set initial selection
    this.yearCtrl.setValue(this.years[0]);

    // set initial selection
    this.partCtrl.setValue(this.parts[0]);
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

  protected filterSchools() {
    if (!this.schools) {
      return;
    }
    // get the search keyword
    let search = this.schoolFilterCtrl.value;
    if (!search) {
      this.filteredSchools.next(this.schools.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the school
    this.filteredSchools.next(
      this.schools.filter(school => school.school_Name.toLowerCase().indexOf(search) > -1)
    );
  }
}

