import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewstudentGeneralinfoComponent } from './viewstudent-generalinfo.component';

describe('ViewstudentGeneralinfoComponent', () => {
  let component: ViewstudentGeneralinfoComponent;
  let fixture: ComponentFixture<ViewstudentGeneralinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewstudentGeneralinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewstudentGeneralinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
