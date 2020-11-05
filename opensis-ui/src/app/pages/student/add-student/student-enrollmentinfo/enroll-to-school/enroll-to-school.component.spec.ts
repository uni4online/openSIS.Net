import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrollToSchoolComponent } from './enroll-to-school.component';

describe('EnrollToSchoolComponent', () => {
  let component: EnrollToSchoolComponent;
  let fixture: ComponentFixture<EnrollToSchoolComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrollToSchoolComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrollToSchoolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
