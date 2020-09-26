import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { ReplaySubject, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { YEAR, YEARS, PART, PARTS } from './year-data';
import { SchoolService } from '../../../../app/services/school.service';
import { AllSchoolListModel,OnlySchoolListModel } from 'src/app/models/getAllSchoolModel';

@Component({
  selector: 'vex-select-bar',
  templateUrl: './select-bar.component.html',
  styleUrls: ['./select-bar.component.scss']
})
export class SelectBarComponent implements OnInit {
  getSchoolList:OnlySchoolListModel = new OnlySchoolListModel();
  schools;
  // List of Years
  protected years: YEAR[] = YEARS;
  // List of Parts
  protected parts: PART[] = PARTS;
  schoolCtrl: FormControl;
  /** control for the selected Year */
  public yearCtrl: FormControl = new FormControl();

  /** control for the selected Part */
  public partCtrl: FormControl = new FormControl();
  schoolFilterCtrl: FormControl;

  /** control for the MatSelect filter keyword */
  public yearFilterCtrl: FormControl = new FormControl();

  /** control for the MatSelect filter keyword */
  public partFilterCtrl: FormControl = new FormControl();

  /** list of schools filtered by search keyword */
  public filteredSchools: ReplaySubject<AllSchoolListModel[]> = new ReplaySubject<AllSchoolListModel[]>(1);

  /** list of years filtered by search keyword */
  public filteredYears: ReplaySubject<YEAR[]> = new ReplaySubject<YEAR[]>(1);

  /** list of parts filtered by search keyword */
  public filteredParts: ReplaySubject<PART[]> = new ReplaySubject<PART[]>(1);

  @ViewChild('singleSelect') singleSelect: MatSelect;
  @ViewChild('singleSelectYear') singleSelectYear: MatSelect;
  @ViewChild('singleSelectPart') singleSelectPart: MatSelect;

  /** Subject that emits when the component has been destroyed. */
  protected _onDestroy = new Subject<void>();

  constructor(private _schoolService: SchoolService) {
    this.getSchoolList.tenantId = sessionStorage.getItem("tenantId");
    this.getSchoolList._tenantName = sessionStorage.getItem("tenant");
    this.getSchoolList._token = sessionStorage.getItem("token");
   
      this._schoolService.GetAllSchoolList(this.getSchoolList).subscribe((data) => {
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
      })
  }

  changeSchool(name,id){
    localStorage.setItem("selectedId",id);
  }
  ngOnInit() {

    // set initial selection
    this.yearCtrl.setValue(this.years[0]);

    // set initial selection
    this.partCtrl.setValue(this.parts[0]);

    // load the initial year list
    this.filteredYears.next(this.years.slice());

    // load the initial part list
    this.filteredParts.next(this.parts.slice());

    // listen for search field value changes
    this.yearFilterCtrl.valueChanges
      .pipe(takeUntil(this._onDestroy))
      .subscribe(() => {
        this.filterYears();
      });

    // listen for search field value changes
    this.partFilterCtrl.valueChanges
      .pipe(takeUntil(this._onDestroy))
      .subscribe(() => {
        this.filterParts();
      });
  }

  ngAfterViewInit() {
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

  protected setInitialYearValue() {
    this.filteredYears
      .pipe(take(1), takeUntil(this._onDestroy))
      .subscribe(() => {
        // setting the compareWith property to a comparison function
        // triggers initializing the selection according to the initial value of
        // the form control (i.e. _initializeSelection())
        // this needs to be done after the filteredYears are loaded initially
        // and after the mat-option elements are available
        this.singleSelectYear.compareWith = (a: YEAR, b: YEAR) => a && b && a.id === b.id;
      });
  }

  protected setInitialPartValue() {
    this.filteredParts
      .pipe(take(1), takeUntil(this._onDestroy))
      .subscribe(() => {
        // setting the compareWith property to a comparison function
        // triggers initializing the selection according to the initial value of
        // the form control (i.e. _initializeSelection())
        // this needs to be done after the filteredPart are loaded initially
        // and after the mat-option elements are available
        this.singleSelectPart.compareWith = (a: PART, b: PART) => a && b && a.id === b.id;
      });
  }

  protected filterYears() {
    if (!this.years) {
      return;
    }
    // get the search keyword
    let search = this.yearFilterCtrl.value;
    if (!search) {
      this.filteredYears.next(this.years.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the Year
    this.filteredYears.next(
      this.years.filter(year => year.name.toLowerCase().indexOf(search) > -1)
    );
  }

  protected filterParts() {
    if (!this.parts) {
      return;
    }
    // get the search keyword
    let search = this.partFilterCtrl.value;
    if (!search) {
      this.filteredParts.next(this.parts.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the parts
    this.filteredParts.next(
      this.parts.filter(part => part.name.toLowerCase().indexOf(search) > -1)
    );
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
