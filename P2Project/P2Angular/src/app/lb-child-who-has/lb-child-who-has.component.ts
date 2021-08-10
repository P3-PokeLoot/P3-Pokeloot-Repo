import { Component, OnInit } from '@angular/core';
import { LeaderboardStatsService } from '../service/leaderboards/leaderboard-stats.service';

@Component({
  selector: 'app-lb-child-who-has',
  templateUrl: './lb-child-who-has.component.html',
  styleUrls: ['./lb-child-who-has.component.css']
})
export class LbChildWhoHasComponent implements OnInit {

  constructor( private _leaderboardService:LeaderboardStatsService) {}

  ngOnInit(): void {
  }

  
}
