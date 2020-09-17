import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageCropperService {
  private cropEventSubject = new Subject<any>();
  private unCropEventSubject = new Subject<any>();
  constructor() { }

  sendCroppedEvent(event) {
    this.cropEventSubject.next(event);
  }
  getCroppedEvent(): Observable<any> {
    return this.cropEventSubject.asObservable();
  }

  sendUncroppedEvent(event) {
    this.unCropEventSubject.next(event);
  }
  getUncroppedEvent(): Observable<any> {
    return this.unCropEventSubject.asObservable();
  }
}
