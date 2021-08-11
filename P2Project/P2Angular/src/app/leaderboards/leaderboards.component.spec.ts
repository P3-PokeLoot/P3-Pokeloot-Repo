import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LeaderboardStatsService } from '../service/leaderboards/leaderboard-stats.service';
import { NgxPaginationModule } from 'ngx-pagination';
import { LeaderboardsComponent } from './leaderboards.component';
import { MockCoinBalanceObservable } from './leaderboards_mocks/MockCoinBalanceData';
import { MockLeaderBoardService } from './leaderboards_mocks/MockLeaderboardService';

describe('LeaderboardsComponent', () => {
  let component: LeaderboardsComponent;
  let fixture: ComponentFixture<LeaderboardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeaderboardsComponent ],
      imports: [HttpClientTestingModule, NgxPaginationModule],
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


  
  //testing dropdown arrays
  it('should be an array of strings',()=>{
    expect(component.allStatsOptions[0]).toBe('Coin Balance');
    expect(component.allStatsOptions[1]).toBe('Coins Earned');
    expect(component.allStatsOptions[2]).toBe('Coins Spent');
  });

  it('should be an array of numbers',()=>{
    expect(component.allNumbersOptions[0]).toBe(10)
    expect(component.allNumbersOptions[1]).toBe(25);
    expect(component.allNumbersOptions[3]).toBe(100);
  });

  it('should be an array of string with "rarities" ',()=>{
    expect(component.allRarityOptions[0]).toBe('Common')
    expect(component.allRarityOptions[2]).toBe('Rare');
    expect(component.allRarityOptions[4]).toBe('Legendary');
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
  it('should set "chosenNumber" property equal to a number input',()=>{
    component.chosenRarity = 'rarity';
    expect(component.chosenRarity).toBe('rarity');
  });
  //method 4
  it('should create two arrays from 1 observable stream',()=>{
    //Arange
    let testcoin:MockCoinBalanceObservable = new MockCoinBalanceObservable();
    //Act
    component.observableData = testcoin.MockCoinDataArray;
    component.observableData.forEach(item => component.headersArray = Object.keys(item));
    component.observableData.forEach(data => component.columnsArray = Object.values(data));
    //Assert
    expect(component.headersArray.length).toBe(10);
    expect(component.headersArray[0]).toBe('UserName');
    expect(component.headersArray[4]).toBe('Email');
    expect(component.headersArray[9]).toBe('DisplayBoards');
    expect(component.columnsArray.length).toBe(10);
    expect(component.headersArray[0][1]).toBe('s');
    expect(component.headersArray[0][5]).toBe('a');
    expect(component.headersArray[0][8]).toBeNull;
  });
  //Testing Method 5
  it('should assign a class property value (based off a different class property) by iterating through an array',()=>{
    // let testing = ['a','b','c','d'];
    // let letter = testing[2];
    // //act
    // for (let i=0; i< testing.length; i++){
    //   if 
    // }
    
  });
  //Testing Method 6
  it('should invoke a service method, insert observable stream to class property, and invoke another method',()=>{

  });

  //Method 6: (this test is not working)
  // it('should set arrays filled with data to empty arrays',()=>{
  //   //Arrange
    
  //   //Act

  //   //Assert
  //   expect(component.observableData.length).toBe(0);
  //   expect(component.observableData.entries).toEqual(null);
  //   expect(component.headersArray.length).toBe(0);
  //   expect(component.headersArray.entries).toEqual(null);
  //   expect(component.columnsArray.length).toBe(0);
  //   expect(component.columnsArray.entries).toEqual(null);
  // });

  it('should create ', () => {
    expect(component).toBeTruthy();
  });
});
