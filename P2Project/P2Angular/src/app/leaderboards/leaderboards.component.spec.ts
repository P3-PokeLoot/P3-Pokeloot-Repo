import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LeaderboardStatsService } from '../leaderboard-stats.service';

import { LeaderboardsComponent } from './leaderboards.component';
import { MockLeaderBoardService } from './leaderboards_mocks/MockLeaderboardService';

describe('LeaderboardsComponent', () => {
  let component: LeaderboardsComponent;
  let fixture: ComponentFixture<LeaderboardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeaderboardsComponent ],
      imports: [HttpClientTestingModule],
      providers:[{
        provide: LeaderboardStatsService, use: MockLeaderBoardService
      }]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaderboardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  //testing pagination properties
  it('should create an array of pages based on received data',()=>{
    
    expect(component.pageOfItems[1]).toBe(5);
  });
  //testing pagination methods
  
  //testing dropdown arrays
  it('should be an array',()=>{
    expect(component.allStatsOptions[0]).toBe('---');
    expect(component.allStatsOptions[1]).toBe('topcoinbalance');
    expect(component.allStatsOptions[4]).toBe('topcoinsspent');
  });

  it('should select a method based on a string input',()=>{
    expect(component.allNumbersOptions[0]).toBe(10)
    expect(component.allNumbersOptions[1]).toBe(25);
    expect(component.allNumbersOptions[4]).toBe(100);
  });
  //testing Method 1
  it('should select a method based on a string input',()=>{
    
    
  });
  //Testing Method 2
  it('should set "currentChosenNumber" property equal to a number input',()=>{
    
    
  });
  //Testing Method 3
  it('should create two arrays from 1 observable stream',()=>{
    
    
  });
  //Testing Method 4
  it('should invoke a service method, insert observable stream to class property, and invoke another method',()=>{

  });
  //Testing Method 5:
  it('should create two arrays from an array of objects',()=>{

  });
  //Method 6:
  it('should should take an observable stream and map it to a class array',()=>{

  });

  //Method 7:
  it('should set arrays filled with data to empty arrays',()=>{
    //Arrange
    
    //Act

    //Asset
    expect(component.observableData.length).toBe(0);
    expect(component.observableData.entries).toBe(null);
    expect(component.headersArray.length).toBe(0);
    expect(component.headersArray.entries).toBe(null);
    expect(component.columnsArray.length).toBe(0);
    expect(component.columnsArray.entries).toBe(null);
  });

  it('should create ', () => {
    expect(component).toBeTruthy();
  });
});
