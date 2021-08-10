import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { FriendServiceService } from './friend-service.service';

describe('FriendServiceService', () => {
  let service: FriendServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, RouterTestingModule],
    });
    service = TestBed.inject(FriendServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
