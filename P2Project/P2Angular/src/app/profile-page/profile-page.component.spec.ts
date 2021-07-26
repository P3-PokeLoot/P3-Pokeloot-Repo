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
    expect(component.currentProfile.AccountLevel).toBe(18);
  })
  //testing ViewJohto()
  it('1should modify an array of type Badge by evaluating property values of a User object', () =>{
    component.ViewJohto(MockUser,MockBadges);
    expect(MockBadges[0].Completed).toBe(true);
    expect(MockBadges[1].Completed).toBe(true);
    expect(MockBadges[2].Completed).toBe(true);
  })
  it('2should modify an array of type Badge by evaluating property values of a User object', () =>{
    component.ViewJohto(MockUser,MockBadges);
    expect(MockBadges[3].Completed).toBe(true);
  })
  it('3should modify an array of type Badge by evaluating property values of a User object', () =>{
    component.ViewJohto(MockUser,MockBadges);
    expect(MockBadges[4].Completed).toBe(false);
  })
  it('4should modify an array of type Badge by evaluating property values of a User object', () =>{
    component.ViewJohto(MockUser,MockBadges);
    expect(MockBadges[7].Completed).toBe(false);
  })
  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
