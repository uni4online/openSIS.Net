import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewstudentEnrollmentinfoComponent } from './viewstudent-enrollmentinfo.component';

describe('ViewstudentEnrollmentinfoComponent', () => {
  let component: ViewstudentEnrollmentinfoComponent;
  let fixture: ComponentFixture<ViewstudentEnrollmentinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewstudentEnrollmentinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewstudentEnrollmentinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
