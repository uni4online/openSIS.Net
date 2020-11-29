import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewparentGeneralinfoComponent } from './viewparent-generalinfo.component';

describe('ViewparentGeneralinfoComponent', () => {
  let component: ViewparentGeneralinfoComponent;
  let fixture: ComponentFixture<ViewparentGeneralinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewparentGeneralinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewparentGeneralinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
