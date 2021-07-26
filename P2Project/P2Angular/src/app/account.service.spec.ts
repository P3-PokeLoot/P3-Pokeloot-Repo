import { TestBed } from '@angular/core/testing';
import{ HttpClientTestingModule, HttpTestingController} from '@angular/common/http/testing';
import { AccountService } from './account.service';
import { MockUser } from './profile-page/MockUser';

describe('AccountService', () => {
  let service: AccountService;
  let HttpTestingController:HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientTestingModule],
      providers:[AccountService],
    });
    service = TestBed.inject(AccountService);
  });

  it('should make a GET request',()=>{
    //Setup a mock 
    const User ={
      result: [],
      next_page:true,
    };
    service.GetUserProfile().subscribe(x=>{
      expect(x).toEqual(MockUser);
    })
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
