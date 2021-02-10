import { ChangeDetectorRef, Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import icGroup from '@iconify/icons-ic/twotone-group';
import icPageView from '@iconify/icons-ic/twotone-pageview';
import icCloudOff from '@iconify/icons-ic/twotone-cloud-off';
import icTimer from '@iconify/icons-ic/twotone-timer';
import icSchool from '@iconify/icons-ic/twotone-account-balance';
import icStudent from '@iconify/icons-ic/twotone-school';
import icStaff from '@iconify/icons-ic/twotone-people';
import icParent from '@iconify/icons-ic/twotone-escalator-warning';
import icMissingAttendance from '@iconify/icons-ic/twotone-alarm-off';
import { defaultChartOptions } from '../../../../@vex/utils/default-chart-options';
import { Order, tableSalesData } from '../../../../static-data/table-sales-data';
import { TableColumn } from '../../../../@vex/interfaces/table-column.interface';
import icMoreVert from '@iconify/icons-ic/twotone-more-vert';
import icPreview from '@iconify/icons-ic/round-preview';
import icPeople from '@iconify/icons-ic/twotone-people';
import { LayoutService } from 'src/@vex/services/layout.service';
import { DashboardViewModel } from '../../../models/dashboardModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DasboardService } from '../../../services/dasboard.service';
import { CalendarDateFormatter, CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarMonthViewBeforeRenderEvent, CalendarMonthViewDay, CalendarView, DAYS_OF_WEEK } from 'angular-calendar';
import { CalendarEventModel } from '../../../models/calendarEventModel';
import { Observable, Subject } from 'rxjs';
import { CalendarModel } from 'src/app/models/calendarModel';
import { map, takeUntil, tap } from 'rxjs/operators';
import { CustomDateFormatter } from '../../shared-module/user-defined-directives/custom-date-formatter.provider';


@Component({
  selector: 'vex-dashboard-analytics',
  templateUrl: './dashboard-analytics.component.html',
  styleUrls: ['./dashboard-analytics.component.scss'],
  styles: [
    `
     .cal-month-view .bg-aqua,
      .cal-week-view .cal-day-columns .bg-aqua,
      .cal-day-view .bg-aqua {
        background-color: #ffdee4 !important;
      }
    `,
  ],
  providers: [
    {
      provide: CalendarDateFormatter,
      useClass: CustomDateFormatter,
    },
  ],
})
export class DashboardAnalyticsComponent implements OnInit,OnDestroy {

  view: CalendarView = CalendarView.Month;
  viewDate: Date = new Date();
  events: CalendarEvent[] = [];
  events$: Observable<CalendarEvent<{ calendar: CalendarEventModel }>[]>;
  calendars: CalendarModel;
  activeDayIsOpen = true;
  weekendDays: number[];
  filterDays = [];


  tableColumns: TableColumn<Order>[] = [
    {
      label: '',
      property: 'status',
      type: 'badge'
    },
    {
      label: 'PRODUCT',
      property: 'name',
      type: 'text'
    },
    {
      label: '$ PRICE',
      property: 'price',
      type: 'text',
      cssClasses: ['font-medium']
    },
    {
      label: 'DATE',
      property: 'timestamp',
      type: 'text',
      cssClasses: ['text-secondary']
    }
  ];
  tableData = tableSalesData;

  series: ApexAxisChartSeries = [{
    name: 'Subscribers',
    data: [28, 40, 36, 0, 52, 38, 60, 55, 67, 33, 89, 44]
  }];

  userSessionsSeries: ApexAxisChartSeries = [
    {
      name: 'Attendance',
      data: [38480, 40203, 36890, 41520, 38005, 34060, 23150, 35002, 29161, 38054, 40250, 36492]
    }
  ];

  salesSeries: ApexAxisChartSeries = [{
    name: 'Sales',
    data: [28, 40, 36, 0, 52, 38, 60, 55, 99, 54, 38, 87]
  }];

  pageViewsSeries: ApexAxisChartSeries = [{
    name: 'Page Views',
    data: [405, 800, 200, 600, 105, 788, 600, 204]
  }];

  uniqueUsersSeries: ApexAxisChartSeries = [{
    name: 'Unique Users',
    data: [356, 806, 600, 754, 432, 854, 555, 1004]
  }];

  uniqueUsersOptions = defaultChartOptions({
    chart: {
      type: 'area',
      height: 100
    },
    colors: ['#ff9800']
  });

  icGroup = icGroup;
  icPageView = icPageView;
  icCloudOff = icCloudOff;
  icTimer = icTimer;
  icMoreVert = icMoreVert;
  icSchool = icSchool;
  icStudent = icStudent;
  icStaff = icStaff;
  icParent = icParent;
  icPreview = icPreview;
  icPeople = icPeople;
  icMissingAttendance = icMissingAttendance;
  scheduleType = '1';
  durationType = '1';
  addCalendarDay = 0;
  studentCount: number;
  parentCount: number;
  staffCount: number;
  dashboardViewModel: DashboardViewModel = new DashboardViewModel();
  destroySubject$: Subject<void> = new Subject();
  noticeTitle: string;
  calendarTitle: string;
  noticeBody: string;
  refresh: Subject<any> = new Subject();
  cssClass: string;
  showCalendarView: boolean = false;
  noticeHide: boolean = true;

  constructor(private cd: ChangeDetectorRef, private layoutService: LayoutService,
    private snackbar: MatSnackBar,
    private dasboardService: DasboardService,) {
    if (localStorage.getItem("collapseValue") !== null) {
      if (localStorage.getItem("collapseValue") === "false") {
        this.layoutService.expandSidenav();
      } else {
        this.layoutService.collapseSidenav();
      }
    } else {
      this.layoutService.expandSidenav();
    }
    this.dasboardService.getPageLoadEvent().pipe(takeUntil(this.destroySubject$)).subscribe((message) => {
      if (message === true) {
        this.getDashboardView();
      }
    });

  }
 
  ngOnDestroy(): void {
    this.destroySubject$.next();
    this.destroySubject$.complete();
  }

  ngOnInit() {
    this.getDashboardView();
    setTimeout(() => {
      const temp = [
        {
          name: 'Subscribers',
          data: [55, 213, 55, 0, 213, 55, 33, 55]
        },
        {
          name: ''
        }
      ];
    }, 3000);
  }


  //for rendar weekends
  beforeMonthViewRender(renderEvent: CalendarMonthViewBeforeRenderEvent): void {
    renderEvent.body.forEach((day) => {
      const dayOfMonth = day.date.getDay();
      if (this.filterDays.includes(dayOfMonth)) {
        day.cssClass = this.cssClass;
      }
    });
  }

  getDashboardView() {
    this.dashboardViewModel.schoolId = +sessionStorage.getItem("selectedSchoolId");
    this.events$ = this.dasboardService.getDashboardView(this.dashboardViewModel).pipe(tap((res)=> {
      if (typeof (res) == 'undefined') {
        this.snackbar.open('Custom Field list failed. ' + sessionStorage.getItem("httpError"), '', {
          duration: 10000
        });
      }
      else {
        if (res._failure) {
          this.snackbar.open('Custom Field list failed. ' + res._message, 'LOL THANKS', {
            duration: 10000
          });
        }
        else {
          this.studentCount = res.totalStudent !== null ? res.totalStudent : 0;
          this.staffCount = res.totalStaff !== null ? res.totalStaff : 0;
          this.parentCount = res.totalParent !== null ? res.totalParent : 0;
          var checkCalendarEvent= res.calendarEventList;
          if(res.noticeTitle !==null){
            this.noticeTitle = res.noticeTitle;
          }
          else{
            this.noticeTitle = "No notice found!"
            this.noticeHide = false;
          }
         
          this.noticeBody = res.noticeBody;
          this.calendars = res.schoolCalendar;
          this.showCalendarView = false;
          if (this.calendars !== null) {
            this.calendarTitle = res.schoolCalendar.title;
            this.showCalendarView = true;
            this.getDays(this.calendars.days);
          }
        }
      }
    
    }),
      map(({ calendarEventList }: { calendarEventList: CalendarEventModel[] }) => {
        if(calendarEventList !==null ){
          return calendarEventList.map((calendar: CalendarEventModel) => {

            return {
              id: calendar.eventId,
              title: calendar.title,
              start: new Date(calendar.startDate),
              end: new Date(calendar.endDate),
              allDay: true,
              meta: {
                calendar,
              },
              draggable: true
            };
          });
        }
        
      })
    );

   
  }

  getDays(days: string) {
    const calendarDays = days;
    var allDays = [0, 1, 2, 3, 4, 5, 6];
    var splitDays = calendarDays.split('').map(x => +x);
    this.filterDays = allDays.filter(f => !splitDays.includes(f));
    this.weekendDays = this.filterDays;
    this.cssClass = 'bg-aqua';
    this.refresh.next();
  }

}
