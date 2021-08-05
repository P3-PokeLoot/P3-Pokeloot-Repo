import { TestBed } from '@angular/core/testing';

import { LeaderboardStatsService } from './leaderboard-stats.service';

describe('LeaderboardStatsService', () => {
  let service: LeaderboardStatsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LeaderboardStatsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
