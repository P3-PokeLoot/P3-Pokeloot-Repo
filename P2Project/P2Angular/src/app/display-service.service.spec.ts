import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { DisplayServiceService } from './display-service.service';

describe('DisplayServiceService', () => {
  let service: DisplayServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, RouterTestingModule],
    });
    service = TestBed.inject(DisplayServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
