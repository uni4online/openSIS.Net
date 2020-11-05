import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEnrollmentComponent } from './edit-enrollment.component';

describe('EditEnrollmentComponent', () => {
  let component: EditEnrollmentComponent;
  let fixture: ComponentFixture<EditEnrollmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditEnrollmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditEnrollmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
