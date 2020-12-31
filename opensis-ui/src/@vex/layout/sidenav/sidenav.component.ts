import { Component, Input, OnInit } from '@angular/core';
import { trackByRoute } from '../../utils/track-by';
import { NavigationService } from '../../services/navigation.service';
// import icRadioButtonChecked from '@iconify/icons-ic/twotone-radio-button-checked';
// import icRadioButtonUnchecked from '@iconify/icons-ic/twotone-radio-button-unchecked';
import icCollapseSidebar from '@iconify/icons-ic/twotone-switch-left';
import icExpandSidebar from '@iconify/icons-ic/twotone-switch-right';
import icArrowDropDown from '@iconify/icons-ic/arrow-drop-down';
import { LayoutService } from '../../services/layout.service';
import { ConfigService } from '../../services/config.service';
import { map } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'vex-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {

  @Input() collapsed: boolean;
  globalCollapseValue;
  collapsedOpen$ = this.layoutService.sidenavCollapsedOpen$;
  title$ = this.configService.config$.pipe(map(config => config.sidenav.title));
  imageUrl$ = this.configService.config$.pipe(map(config => config.sidenav.imageUrl));
  showCollapsePin$ = this.configService.config$.pipe(map(config => config.sidenav.showCollapsePin));

  items = this.navigationService.items;
  trackByRoute = trackByRoute;
  // icRadioButtonChecked = icRadioButtonChecked;
  // icRadioButtonUnchecked = icRadioButtonUnchecked;
  icCollapseSidebar = icCollapseSidebar;
  icExpandSidebar = icExpandSidebar;
  icArrowDropDown = icArrowDropDown;

  constructor(private navigationService: NavigationService,
              private layoutService: LayoutService,
              private configService: ConfigService,
              private router:Router) { }

  ngOnInit() {
    
  }

  onMouseEnter() {
    this.layoutService.collapseOpenSidenav();
  }

  onMouseLeave() {
    this.layoutService.collapseCloseSidenav();
  }

  toggleCollapse() { 
    if(this.collapsed){
      localStorage.setItem("collapseValue","false");
      this.layoutService.expandSidenav()
    }else{
      this.layoutService.collapseSidenav();
      localStorage.setItem("collapseValue","true");
    }   
    
  }
  logOut(){
    localStorage.removeItem("collapseValue");
    sessionStorage.removeItem("token");    
    this.router.navigate(["/"]);
  }
}
