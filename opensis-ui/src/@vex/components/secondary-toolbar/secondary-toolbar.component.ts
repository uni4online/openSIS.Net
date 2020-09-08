import { Component, Input, OnInit } from '@angular/core';
import { ConfigService } from '../../services/config.service';
import { map } from 'rxjs/operators';
import icHelp from '@iconify/icons-ic/help';
import icSearch from '@iconify/icons-ic/search';

@Component({
  selector: 'vex-secondary-toolbar',
  templateUrl: './secondary-toolbar.component.html',
  styleUrls: ['./secondary-toolbar.component.scss']
})
export class SecondaryToolbarComponent implements OnInit {

  icHelp = icHelp;
  icSearch = icSearch;

  @Input() current: string;
  @Input() crumbs: string[];

  fixed$ = this.configService.config$.pipe(
    map(config => config.toolbar.fixed)
  );

  constructor(private configService: ConfigService) { }

  ngOnInit() {
  }
}
