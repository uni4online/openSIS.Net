import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewstaffGeneralinfoComponent } from './viewstaff-generalinfo.component';

describe('ViewstaffGeneralinfoComponent', () => {
  let component: ViewstaffGeneralinfoComponent;
  let fixture: ComponentFixture<ViewstaffGeneralinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewstaffGeneralinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewstaffGeneralinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
