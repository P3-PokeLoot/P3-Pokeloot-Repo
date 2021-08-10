import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { LeaderboardStatsService } from './leaderboard-stats.service';

describe('LeaderboardStatsService', () => {
  let service: LeaderboardStatsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, RouterTestingModule],
    });
    service = TestBed.inject(LeaderboardStatsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
