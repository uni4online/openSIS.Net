import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { CalendarAddViewModel, CalendarListModel } from '../models/calendarModel';
import { CalendarEventAddViewModel, CalendarEventListViewModel } from '../models/calendarEventModel';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class CalendarEventService {
    private eventSource = new BehaviorSubject(false);
    currentEvent = this.eventSource.asObservable();

    apiUrl: string = environment.apiURL;
    constructor(private http: HttpClient) { }

    addCalendarEvent(calendarEvent: CalendarEventAddViewModel) {
        let apiurl = this.apiUrl + calendarEvent._tenantName + "/CalendarEvent/addCalendarEvent";
        return this.http.post<CalendarEventAddViewModel>(apiurl, calendarEvent)
    }
    viewCalendarEvent(calendarEvent: CalendarEventAddViewModel) {
        let apiurl = this.apiUrl + calendarEvent._tenantName + "/CalendarEvent/viewCalendarEvent";
        return this.http.post<CalendarEventAddViewModel>(apiurl, calendarEvent)
    }

    updateCalendarEvent(calendarEvent: CalendarEventAddViewModel) {
        let apiurl = this.apiUrl + calendarEvent._tenantName + "/CalendarEvent/updateCalendarEvent";
        return this.http.put<CalendarEventAddViewModel>(apiurl, calendarEvent)
    }

    deleteCalendarEvent(calendarEvent: CalendarEventAddViewModel) {
        let apiurl = this.apiUrl + calendarEvent._tenantName + "/CalendarEvent/deleteCalendarEvent";
        return this.http.post<CalendarEventAddViewModel>(apiurl, calendarEvent)
    }

    getAllCalendarEvent(calendarEvent: CalendarEventListViewModel) {
        let apiurl = this.apiUrl + calendarEvent._tenantName + "/CalendarEvent/getAllCalendarEvent";
        return this.http.post<CalendarEventListViewModel>(apiurl, calendarEvent)
    }
    changeEvent(message: boolean) {
        this.eventSource.next(message)
    }
}
