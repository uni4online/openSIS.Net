import { Injectable } from '@angular/core';
import { BehaviorSubject,Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageCropperService {
  private cropEventSubject = new Subject<any>();
  private unCropEventSubject = new Subject<any>();
  private message = new BehaviorSubject(false);
  sharedMessage = this.message.asObservable();
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

  enableUpload(message: boolean) {
    this.message.next(message)
  }
}
