import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewstudentAddressandcontactsComponent } from './viewstudent-addressandcontacts.component';

describe('ViewstudentAddressandcontactsComponent', () => {
  let component: ViewstudentAddressandcontactsComponent;
  let fixture: ComponentFixture<ViewstudentAddressandcontactsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewstudentAddressandcontactsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewstudentAddressandcontactsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
