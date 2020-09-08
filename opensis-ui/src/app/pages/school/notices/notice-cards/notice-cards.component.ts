import { Component, Input, OnInit } from '@angular/core';
import icPreview from '@iconify/icons-ic/round-preview';
import icPeople from '@iconify/icons-ic/twotone-people';
import icMoreVert from '@iconify/icons-ic/more-vert';

@Component({
  selector: 'vex-notice-cards',
  templateUrl: './notice-cards.component.html',
  styleUrls: ['./notice-cards.component.scss']
})
export class NoticeCardsComponent implements OnInit {

  icPreview = icPreview;
  icPeople = icPeople;
  icMoreVert = icMoreVert;

  @Input() title: string;
  @Input() imageUrl: string;
  @Input() visibleFrom: string;
  @Input() visibleTo: number;

  constructor() { }

  ngOnInit(): void {
  }

}
