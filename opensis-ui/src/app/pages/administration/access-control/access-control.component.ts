import { Component, OnInit, ViewChild } from '@angular/core';
import { LayoutService } from 'src/@vex/services/layout.service';
import icExpandAll from '@iconify/icons-ic/unfold-more';
import icCollapseAll from '@iconify/icons-ic/unfold-less';
import icExpand from '@iconify/icons-ic/expand-more';
import icCollapse from '@iconify/icons-ic/expand-less';
import { MatAccordion } from '@angular/material/expansion';

@Component({
  selector: 'vex-access-control',
  templateUrl: './access-control.component.html',
  styleUrls: ['./access-control.component.scss']
})
export class AccessControlComponent implements OnInit {
  @ViewChild(MatAccordion) accordion: MatAccordion;

  icExpandAll = icExpandAll;
  icExpand = icExpand;
  icCollapseAll = icCollapseAll;
  icCollapse = icCollapse;

  constructor(private layoutService: LayoutService) {
    this.layoutService.collapseSidenav();

   }

  ngOnInit(): void {
  }

}
