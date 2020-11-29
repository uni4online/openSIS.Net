import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewstaffSchoolinfoComponent } from './viewstaff-schoolinfo.component';

describe('ViewstaffSchoolinfoComponent', () => {
  let component: ViewstaffSchoolinfoComponent;
  let fixture: ComponentFixture<ViewstaffSchoolinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewstaffSchoolinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewstaffSchoolinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
