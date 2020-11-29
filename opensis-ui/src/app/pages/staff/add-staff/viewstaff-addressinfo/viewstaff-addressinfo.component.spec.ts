import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewstaffAddressinfoComponent } from './viewstaff-addressinfo.component';

describe('ViewstaffAddressinfoComponent', () => {
  let component: ViewstaffAddressinfoComponent;
  let fixture: ComponentFixture<ViewstaffAddressinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewstaffAddressinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewstaffAddressinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
