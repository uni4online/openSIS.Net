import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMarkingPeriodComponent } from './edit-marking-period.component';

describe('EditMarkingPeriodComponent', () => {
  let component: EditMarkingPeriodComponent;
  let fixture: ComponentFixture<EditMarkingPeriodComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditMarkingPeriodComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditMarkingPeriodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
