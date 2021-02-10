import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/twotone-search';
import icAdd from '@iconify/icons-ic/twotone-add';
import icFilterList from '@iconify/icons-ic/twotone-filter-list';
import { EditGradeLevelsComponent } from '../grade-levels/edit-grade-levels/edit-grade-levels.component';
import { TranslateService } from '@ngx-translate/core';
import { AddGradeLevelModel, GelAllGradeEquivalencyModel, GetAllGradeLevelsModel } from '../../../models/gradeLevelModel';
import { MatTableDataSource } from '@angular/material/table';
import { GradeLevelService } from '../../../services/grade-level.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { LoaderService } from '../../../services/loader.service';
import { ConfirmDialogComponent } from '../../shared-module/confirm-dialog/confirm-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'vex-grade-levels',
  templateUrl: './grade-levels.component.html',
  styleUrls: ['./grade-levels.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class GradeLevelsComponent implements OnInit {
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort

  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icAdd = icAdd;
  icFilterList = icFilterList;
  getAllGradeLevels: GetAllGradeLevelsModel = new GetAllGradeLevelsModel();
  gradeLevelList: MatTableDataSource<GetAllGradeLevelsModel>;
  getGradeEquivalencyList: GelAllGradeEquivalencyModel = new GelAllGradeEquivalencyModel();
  sendGradeLevelsToDialog: [];
  editMode: boolean;
  sendDetailsToEditComponent: [];
  loading: boolean = false;
  searchKey: string;
  deleteGradeLevelData: AddGradeLevelModel = new AddGradeLevelModel();
  columns = [
    { label: 'Title', property: 'title', type: 'text', visible: true },
    { label: 'Short Name', property: 'shortName', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sortOrder', type: 'number', visible: true },
    { label: 'Grade Level Equivalency', property: 'gradeDescription', type: 'text', visible: true },
    // { label: 'Age Range', property: 'ageRange', type: 'text', visible: false},
    // { label: 'Educational Stage', property: 'educationalStage', type: 'text', visible: false},
    { label: 'Next Grade', property: 'nextGrade', type: 'text', visible: true },
    { label: 'Action', property: 'action', type: 'text', visible: true }
  ];
  constructor(private dialog: MatDialog,
    public translateService: TranslateService,
    private gradeLevelService: GradeLevelService,
    private loaderService: LoaderService,
    private snackbar: MatSnackBar) {
    translateService.use('en');
    this.loaderService.isLoading.subscribe((val) => {
      this.loading = val;
    });
  }
  ngOnInit(): void {
    this.getGradeEquivalency()
    this.getAllGradeLevel();
  }

  getGradeEquivalency() {
    this.gradeLevelService.getAllGradeEquivalency(this.getGradeEquivalencyList).subscribe((res) => {
      this.getGradeEquivalencyList = res;
    })
  }

  openAddNew() {
    this.editMode = false;
    this.dialog.open(EditGradeLevelsComponent, {
      data: {
        editMode: this.editMode,
        gradeLevels: this.sendGradeLevelsToDialog,
        gradeLevelEquivalencyList: this.getGradeEquivalencyList
      },
      width: '600px'
    }).afterClosed().subscribe((res) => {
      if (res) {
        this.getAllGradeLevel();
      }
    });
  }

  openEdit(editDetails) {
    this.editMode = true;
    this.dialog.open(EditGradeLevelsComponent, {
      data: {
        editMode: this.editMode,
        editDetails: editDetails,
        gradeLevels: this.sendGradeLevelsToDialog,
        gradeLevelEquivalencyList: this.getGradeEquivalencyList
      },
      width: '600px'
    }).afterClosed().subscribe((res) => {
      if (res) {
        this.getAllGradeLevel();
      }
    });
  }


  getAllGradeLevel() {
    this.getAllGradeLevels.schoolId = +sessionStorage.getItem("selectedSchoolId");
    this.getAllGradeLevels._tenantName = sessionStorage.getItem("tenant");
    this.getAllGradeLevels._token = sessionStorage.getItem("token");
    this.gradeLevelService.getAllGradeLevels(this.getAllGradeLevels).subscribe((res) => {
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Grade Level List failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (res._failure) {
          if (res._message === "NO RECORD FOUND") {
            if (res.tableGradelevelList == null) {
              this.gradeLevelList = new MatTableDataSource([]);
              this.gradeLevelList.sort = this.sort;
              this.sendGradeLevelsToDialog = [];
            }

          } else {
            this.snackbar.open('Grade Level List failed. ' + res._message, '', {
              duration: 10000
            });
          }

        }
        else {
          this.gradeLevelList = new MatTableDataSource(res.tableGradelevelList);
          this.gradeLevelList.sort = this.sort;
          this.sendGradeLevelsToDialog = res.tableGradelevelList;
        }
      }
    })
  }

  onSearchClear() {
    this.searchKey = "";
    this.applyFilter();
  }

  applyFilter() {
    this.gradeLevelList.filter = this.searchKey.trim().toLowerCase()
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  confirmDelete(deleteDetails) {
    // call our modal window
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
        title: "Are you sure?",
        message: "You are about to delete " + deleteDetails.title + "."
      }
    });
    // listen to response
    dialogRef.afterClosed().subscribe(dialogResult => {
      // if user pressed yes dialogResult will be true, 
      // if user pressed no - it will be false
      if (dialogResult) {
        this.deleteGradeLevel(deleteDetails);
      }
    });
  }

  deleteGradeLevel(deleteDetails) {
    this.deleteGradeLevelData.tblGradelevel.schoolId = deleteDetails.schoolId;
    this.deleteGradeLevelData.tblGradelevel.gradeId = deleteDetails.gradeId;
    this.deleteGradeLevelData._tenantName = sessionStorage.getItem("tenant");
    this.deleteGradeLevelData._token = sessionStorage.getItem("token");
    this.gradeLevelService.deleteGradelevel(this.deleteGradeLevelData).subscribe((res) => {
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Grade Level Deletion failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      } else if (res._failure) {
        this.snackbar.open(res._message, '', {
          duration: 10000
        });
      } else {
        this.snackbar.open('Grade Level Deleted Successfully.', '', {
          duration: 10000
        });
        this.getAllGradeLevel();
      }
    })
  }
}
