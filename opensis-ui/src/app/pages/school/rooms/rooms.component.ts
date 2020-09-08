import { Component, OnInit } from '@angular/core';
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

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openAddNew() {
    this.dialog.open(EditRoomComponent, {
      data: null,
      width: '800px'
    });
  }

  openViewDetails() {
    this.dialog.open(RoomDetailsComponent, {
      data: null,
      width: '600px'
    });
  }

}
