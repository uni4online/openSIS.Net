import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewstudentMedicalinfoComponent } from './viewstudent-medicalinfo.component';

describe('ViewstudentMedicalinfoComponent', () => {
  let component: ViewstudentMedicalinfoComponent;
  let fixture: ComponentFixture<ViewstudentMedicalinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewstudentMedicalinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewstudentMedicalinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
