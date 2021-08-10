import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NbgdTablePaginationComponent } from './nbgd-table-pagination.component';

describe('NbgdTablePaginationComponent', () => {
  let component: NbgdTablePaginationComponent;
  let fixture: ComponentFixture<NbgdTablePaginationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NbgdTablePaginationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NbgdTablePaginationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
