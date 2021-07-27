import { Component, OnInit } from '@angular/core';
import { MockShinyData } from '../MockModels/IMockShiny';
import { MockShinyStatisticData } from '../MockModels/MockShinyStatistic';

@Component({
  selector: 'app-displaystatistics',
  templateUrl: './displaystatistics.component.html',
  styleUrls: ['./displaystatistics.component.css']
})
export class DisplaystatisticsComponent implements OnInit {

  //for now we will simply assign an observable into a property of the Display class - will refactor after service creation
  mockShinyDataArray:MockShinyData[] = MockShinyStatisticData;
  mockCoinDataArry;

  constructor( _statisticservice) { }

  ngOnInit(): void {
  }

  ViewShinyLeaderboard(){

  }
}
