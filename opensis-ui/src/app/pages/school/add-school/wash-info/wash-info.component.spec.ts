import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WashInfoComponent } from './wash-info.component';

describe('WashInfoComponent', () => {
  let component: WashInfoComponent;
  let fixture: ComponentFixture<WashInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WashInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WashInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
