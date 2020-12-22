import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icAdd from '@iconify/icons-ic/baseline-add';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/search';
import icFilterList from '@iconify/icons-ic/filter-list';
import icImpersonate from '@iconify/icons-ic/twotone-account-circle';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger40ms } from '../../../../@vex/animations/stagger.animation';
import { TranslateService } from '@ngx-translate/core';
import { StaffService } from 'src/app/services/staff.service';
import { LoaderService } from '../../../services/loader.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { GetAllStaffModel,StaffListModel } from '../../../models/staffModel';
@Component({
  selector: 'vex-staffinfo',
  templateUrl: './staffinfo.component.html',
  styleUrls: ['./staffinfo.component.scss'],
  animations: [
    fadeInUp400ms,
    stagger40ms
  ]
})
export class StaffinfoComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort

  getAllStaff: GetAllStaffModel = new GetAllStaffModel();
  staffList: MatTableDataSource<StaffListModel>;

  columns = [
    { label: 'Name', property: 'lastFamilyName', type: 'text', visible: true },
    { label: 'Staff ID', property: 'staffInternalId', type: 'text', visible: true },
    { label: 'openSIS Profile', property: 'profile', type: 'text', visible: true },
    { label: 'Job Title', property: 'jobTitle', type: 'text', visible: true },
    { label: 'School Email', property: 'schoolEmail', type: 'text', visible: true },
    { label: 'Mobile Phone', property: 'mobilePhone', type: 'number', visible: true },
    { label: 'Actions', property: 'actions', type: 'text', visible: true }
  ];

  icMoreVert = icMoreVert;
  icAdd = icAdd;
  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icImpersonate = icImpersonate;
  icFilterList = icFilterList;

  loading: boolean;
  totalCount: number = 0;
  pageNumber: number;
  pageSize: number;
  searchCtrl: FormControl;

  constructor(private snackbar: MatSnackBar,
    private router: Router,
    private loaderService: LoaderService,
    public translateService: TranslateService,
    private staffService: StaffService) {
    translateService.use('en');
    this.getAllStaff.filterParams = null;
    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    });
    this.callStaffList();
  }

  ngOnInit(): void {
    this.searchCtrl = new FormControl();
  }

  ngAfterViewInit() {
    //  Sorting
    this.getAllStaff = new GetAllStaffModel();
    this.sort.sortChange.subscribe((res) => {
      this.getAllStaff.pageNumber = this.pageNumber
      this.getAllStaff.pageSize = this.pageSize;
      this.getAllStaff.sortingModel.sortColumn = res.active;
      if (this.searchCtrl.value != null && this.searchCtrl.value != "") {
        let filterParams = [
          {
            columnName: null,
            filterValue: this.searchCtrl.value,
            filterOption: 4
          }
        ]
        Object.assign(this.getAllStaff, { filterParams: filterParams });
      }
      if (res.direction == "") {
        this.getAllStaff.sortingModel = null;
        this.callStaffList();
        this.getAllStaff = new GetAllStaffModel();
        this.getAllStaff.sortingModel = null;
      } else {
        this.getAllStaff.sortingModel.sortDirection = res.direction;
        this.callStaffList();
      }
    });

    //  Searching
    this.searchCtrl.valueChanges.pipe(debounceTime(500), distinctUntilChanged()).subscribe((term) => {
      if (term != '') {
        this.callWithFilterValue(term);
      } else {
        this.callWithoutFilterValue()
      }
    });
  }

  callWithFilterValue(term) {
    let filterParams = [
      {
        columnName: null,
        filterValue: term,
        filterOption: 4
      }
    ]
    if (this.sort.active != undefined && this.sort.direction != "") {
      this.getAllStaff.sortingModel.sortColumn = this.sort.active;
      this.getAllStaff.sortingModel.sortDirection = this.sort.direction;
    }
    Object.assign(this.getAllStaff, { filterParams: filterParams });
    this.getAllStaff.pageNumber = 1;
    this.paginator.pageIndex = 0;
    this.getAllStaff.pageSize = this.pageSize;
    this.callStaffList();
  }

  callWithoutFilterValue() {
    Object.assign(this.getAllStaff, { filterParams: null });
    this.getAllStaff.pageNumber = this.paginator.pageIndex + 1;
    this.getAllStaff.pageSize = this.pageSize;
    if (this.sort.active != undefined && this.sort.direction != "") {
      this.getAllStaff.sortingModel.sortColumn = this.sort.active;
      this.getAllStaff.sortingModel.sortDirection = this.sort.direction;
    }
    this.callStaffList();
  }

  getPageEvent(event) {
    if (this.sort.active != undefined && this.sort.direction != "") {
      this.getAllStaff.sortingModel.sortColumn = this.sort.active;
      this.getAllStaff.sortingModel.sortDirection = this.sort.direction;
    }
    if (this.searchCtrl.value != null && this.searchCtrl.value != "") {
      let filterParams = [
        {
          columnName: null,
          filterValue: this.searchCtrl.value,
          filterOption: 3
        }
      ]
      Object.assign(this.getAllStaff, { filterParams: filterParams });
    }
    this.getAllStaff.pageNumber = event.pageIndex + 1;
    this.getAllStaff.pageSize = event.pageSize;
    this.callStaffList();
  }

  viewStaffDetails(id) {
    this.staffService.setStaffId(id);
    this.router.navigate(["school/staff/add-staff"]); 
  }

  goToAdd() {
    this.staffService.setStaffId(null);
    this.router.navigate(["school/staff/add-staff"]);
  }

  callStaffList() {
    if (this.getAllStaff.sortingModel?.sortColumn == "") {
      this.getAllStaff.sortingModel = null
    }
    this.staffService.getAllStaffList(this.getAllStaff).subscribe(res => {
      if (res._failure) {
        this.snackbar.open('Staff information failed. ' + res._message, 'LOL THANKS', {
          duration: 10000
        });
      } else {
        this.totalCount = res.totalCount;
        this.pageNumber = res.pageNumber;
        this.pageSize = res._pageSize;
        this.staffList = new MatTableDataSource(res.getStaffListForView);
        this.getAllStaff = new GetAllStaffModel();
      }
    });
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

}
