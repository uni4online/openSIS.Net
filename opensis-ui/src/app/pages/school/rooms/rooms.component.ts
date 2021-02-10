import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { stagger60ms } from '../../../../@vex/animations/stagger.animation';
import icEdit from '@iconify/icons-ic/twotone-edit';
import icDelete from '@iconify/icons-ic/twotone-delete';
import icSearch from '@iconify/icons-ic/twotone-search';
import icAdd from '@iconify/icons-ic/twotone-add';
import icFilterList from '@iconify/icons-ic/twotone-filter-list';
import { EditRoomComponent } from '../rooms/edit-room/edit-room.component';
import { RoomDetailsComponent } from '../rooms/room-details/room-details.component';

import { RoomAddView,RoomListViewModel } from '../../../models/roomModel'
import { RoomService } from '../../../services/room.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { TranslateService } from '@ngx-translate/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmDialogComponent } from '../../shared-module/confirm-dialog/confirm-dialog.component';
import { LoaderService } from '../../../services/loader.service';
import { LayoutService } from 'src/@vex/services/layout.service';
@Component({
  selector: 'vex-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.scss'],
  animations: [
    stagger60ms,
    fadeInUp400ms
  ]
})
export class RoomsComponent implements OnInit {

  icEdit = icEdit;
  icDelete = icDelete;
  icSearch = icSearch;
  icAdd = icAdd;
  icFilterList = icFilterList;
  roomaddviewmodel:RoomAddView= new RoomAddView();
  roomListViewModel:RoomListViewModel = new RoomListViewModel()
  roomDetails: any;
  loading:boolean;
  searchKey:string;
  constructor(private dialog: MatDialog,
    private roomService:RoomService,
    private snackbar: MatSnackBar,
    private translateService : TranslateService,
    private loaderService:LoaderService,private layoutService: LayoutService) {
      translateService.use('en');
      if(localStorage.getItem("collapseValue") !== null){
        if( localStorage.getItem("collapseValue") === "false"){
          this.layoutService.expandSidenav();
        }else{
          this.layoutService.collapseSidenav();
        } 
      }else{
        this.layoutService.expandSidenav();
      }
      this.loaderService.isLoading.subscribe((val) => {
        this.loading = val;
      }); 
     }
  roomModelList: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator:MatPaginator
  @ViewChild(MatSort) sort: MatSort;
  columns = [
    { label: 'Title', property: 'title', type: 'text', visible: true },
    { label: 'Capacity', property: 'capacity', type: 'text', visible: true, cssClasses: ['font-medium'] },
    { label: 'Description', property: 'description', type: 'text', visible: true },
    { label: 'Sort Order', property: 'sortOrder', type: 'text', visible: true, cssClasses: ['text-secondary', 'font-medium'] },
    { label: 'isActive', property: 'isActive', type: 'text', visible: true, cssClasses: ['text-secondary', 'font-medium'] },
    { label: 'Action', property: 'action', type: 'text', visible: true },
  ];

  ngOnInit(): void {
    this.roomListViewModel.schoolId=+sessionStorage.getItem("selectedSchoolId")
    this.getAllRooms()
  }
  getAllRooms(){
    this.roomService.getAllRoom(this.roomListViewModel).subscribe(
      (res:RoomListViewModel)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open('Room list failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (res._failure) {     
            if (res._message === "NO RECORD FOUND") {
              if (res.tableroomList == null) {
                this.roomModelList=new MatTableDataSource([]) ;
                this.roomModelList.sort=this.sort;  
              }
              else {
                this.roomModelList=new MatTableDataSource(res.tableroomList) ;
                this.roomModelList.sort=this.sort;  
              }
  
            } else {
              this.snackbar.open('Room List failed. ' + res._message, '', {
                duration: 10000
              });
            }
          } 
          else { 
            this.roomModelList=new MatTableDataSource(res.tableroomList) ;
            this.roomModelList.sort=this.sort;      
          }
        }
      })
  }

  openAddNew() {
    this.dialog.open(EditRoomComponent, {
      data: null,
      width: '600px'
    }).afterClosed().subscribe(data => {
        if(data==='submited'){
          this.getAllRooms();
        }
      });
  }

  openViewDetails(element) {
    this.dialog.open(RoomDetailsComponent, {
      data: {info:element},
      width: '600px'
    });
  }

  onSearchClear(){
    this.searchKey="";
    this.applyFilter();
  }

  applyFilter(){
    this.roomModelList.filter = this.searchKey.trim().toLowerCase()
  }
  
  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  openEditdata(element){
    this.dialog.open(EditRoomComponent, {
      data: element,
        width: '600px'
    }).afterClosed().subscribe((data)=>{
      if(data==='submited'){
        this.getAllRooms()
      }
    })
  }
  deleteRoomdata(element){
    this.roomaddviewmodel.tableRoom=element
    this.roomService.deleteRoom(this.roomaddviewmodel).subscribe(
      (res:RoomAddView)=>{
        if(typeof(res)=='undefined'){
          this.snackbar.open('Room Delete failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else{
          if (res._failure) {
            this.snackbar.open('Room Delete failed. ' + res._message, '', {
              duration: 10000
            });
          } 
          else { 
            this.getAllRooms()
          }
        }
      }
    )
  }
  confirmDelete(element){
      const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        maxWidth: "400px",
        data: {
            title: "Are you sure?",
            message: "You are about to delete "+element.title+"."}
      });
      dialogRef.afterClosed().subscribe(dialogResult => {
        if(dialogResult){
          this.deleteRoomdata(element);
        }
     });
  }
}
