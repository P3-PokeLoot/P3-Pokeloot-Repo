import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LbChildWhoHasComponent } from './lb-child-who-has.component';

describe('LbChildWhoHasComponent', () => {
  let component: LbChildWhoHasComponent;
  let fixture: ComponentFixture<LbChildWhoHasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    imports: [
      HttpClientModule,
      HttpClientTestingModule
    ],
      declarations: [ LbChildWhoHasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LbChildWhoHasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
