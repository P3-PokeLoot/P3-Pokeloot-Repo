import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewInformationPageComponent } from './view-information-page.component';

describe('ViewInformationPageComponent', () => {
  let component: ViewInformationPageComponent;
  let fixture: ComponentFixture<ViewInformationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewInformationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
