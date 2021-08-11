import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LeaderboardStatsService } from '../service/leaderboards/leaderboard-stats.service';

import { LeaderboardsComponent } from './leaderboards.component';
import { MockCoinBalanceObservable } from './leaderboards_mocks/MockCoinBalanceData';
import { MockLeaderBoardService } from './leaderboards_mocks/MockLeaderboardService';

describe('LeaderboardsComponent', () => {
  let component: LeaderboardsComponent;
  let fixture: ComponentFixture<LeaderboardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeaderboardsComponent ],
      imports: [
        HttpClientModule,
        HttpClientTestingModule
      ],
      providers:[{
        provide: LeaderboardStatsService, use: MockLeaderBoardService
      },
    ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaderboardsComponent);
    component = fixture.componentInstance;
    //const testcoin:MockCoinBalanceObservable=new MockCoinBalanceObservable(); <- find correct way
    fixture.detectChanges();
  });

  //create a suite of tests - wrap this around the unit tests
  //describe('Leaderboard component tests',()=>{});


  //testing pagination properties
  it('should create an array of pages based on received data',()=>{
    expect(component.pageOfItems[1]).toBe(5);
  });
  //testing pagination methods

  //testing dropdown arrays
  it('should be an array of strings',()=>{
    expect(component.allStatsOptions[0]).toBe('topcoinbalance');
    expect(component.allStatsOptions[1]).toBe('topearnedcoins');
    expect(component.allStatsOptions[2]).toBe('topcoinsspent');
  });

  it('should be an array of numbers',()=>{
    expect(component.allNumbersOptions[0]).toBe(10)
    expect(component.allNumbersOptions[1]).toBe(25);
    expect(component.allNumbersOptions[4]).toBe(100);
  });

  // Method 1
  it('should set "chosenStat" property equal to a string input',()=>{
    component.chosenStat = 'assignment';
    expect(component.chosenStat).toBe('assignment');
  });

  //Method 2
  it('should set "chosenNumber" property equal to a number input',()=>{
    component.chosenNumber = 420;
    expect(component.chosenNumber).toBe(420);
  });
  //Testing Method 3
  it('should create two arrays from 1 observable stream',()=>{
    //Arange
    let testcoin:MockCoinBalanceObservable=new MockCoinBalanceObservable();

    //Act

    //Assert
  });
  //Testing Method 4
  it('should assign a class property value (based off a different class property) by iterating through an array',()=>{
    let testcoin:MockCoinBalanceObservable = new MockCoinBalanceObservable();
    component.observableData = testcoin.MockCoinDataArray;

  });
  //Testing Method 5
  it('should invoke a service method, insert observable stream to class property, and invoke another method',()=>{

  });

  //Method 6:
  it('should set arrays filled with data to empty arrays',()=>{
    //Arrange

    //Act

    //Assert
    expect(component.observableData.length).toBe(0);
    expect(component.observableData.entries).toEqual(null);
    expect(component.headersArray.length).toBe(0);
    expect(component.headersArray.entries).toEqual(null);
    expect(component.columnsArray.length).toBe(0);
    expect(component.columnsArray.entries).toEqual(null);
  });

  it('should create ', () => {
    expect(component).toBeTruthy();
  });
});
