import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AccountService } from '../account.service';
import { MockAccountService } from './MockAccountService';
import { MockUser } from './MockUser';
import { Badge } from './IBadge';

import { ProfilePageComponent } from './profile-page.component';
import { MockBadges } from './MockBadges';

describe('ProfilePageComponent', () => {
  let component: ProfilePageComponent;
  let fixture: ComponentFixture<ProfilePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports:[HttpClientModule],
      declarations: [ ProfilePageComponent ],
      providers:
      [
        {
          provide: AccountService, use: MockAccountService
        }
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfilePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  //testing subscription
  it('should return a User oject with accessible properties', () => {
    
  })
  //testing ViewJohto()
  it('should modify an array of type Badge by evaluating property values of a User object', () =>{
    component.ViewJohto(MockUser,MockBadges)
    expect()
  })
  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
