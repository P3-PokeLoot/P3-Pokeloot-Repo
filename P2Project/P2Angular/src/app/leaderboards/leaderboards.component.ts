import { Component, OnInit } from '@angular/core';
//import { resourceLimits } from 'worker_threads';
import { LeaderboardStatsService } from '../leaderboard-stats.service';

@Component({
  selector: 'app-leaderboards',
  templateUrl: './leaderboards.component.html',
  styleUrls: ['./leaderboards.component.css']
})
export class LeaderboardsComponent implements OnInit {

  //Don't forget: change the connection string in the P3 Main API from p2 connection string to the correct p3 string. I was using P2 string since that database actually had data.
  //Change back app module & app routing module. Change connection string. 

  /*This had been the general design approach for this component:
  1) Component loads with only dropdown lists and pages on screen
  2) User selects a statistic they want to view from one dropdown, and they choose a number of results they want returned from another dropdown
  3) When User hits "Confirm", a method in the component runs that will take in the chosenStat and chosenNumber as parameters 
  and invoke service method based off those parameters, and then map the resulting data to arrays
  4) A table will be created automatically, and then a user can reset data with a button to view new statistics
  */

  //============================CLASS PROPERTIES SECTION=========================================
    //pagination properties
    pageOfItems:any[];
    currentIndex : number = 0;
    currentPage: number = 1;
    lastpage: number=1;


    //component property that will be updated everytime user selects a dropdown option and click button
    chosenNumber:number=20;
    chosenStat:string='topcoinbalance';
    chosenService:number=2;

    //store observable streams into here so that it can be iterated on & displayed in html
    observableData:any[]=[];
    headersArray:any[]=[];
    columnsArray:any[]=[];

    

    //array of all options that will reflect onto the "categorical dropdown list"
    allStatsOptions:string[]=[
      '---',
      'topcoinbalance',
      'topearnedcoins',
      'topcoinsspent',
    ];
    //array of all options that will reflect onto the "numerical dropdown list"
    allNumbersOptions:number[]=[
      10,
      25,
      50,
      100,
    ];

    

  //===============================CLASS CONSTRUCTOR SECTION===============================
    //DI
  constructor(private _leaderboardservice:LeaderboardStatsService) {
    this.observableData=[];
    this.pageOfItems=[];
   }

  ngOnInit(): void {
  }

  //==========================================CLASS METHOD SECTION ===============================
  //METHOD 1: FUNCTION THAT TAKES IN STRING FROM THE FRONTEND CATEGORY DROPDOWN to choose a service method

  AssignChosenStatFromDropdown(option:string){ //function takes in a string value from "categorical dropdown list"
    this.chosenStat = option;
    console.log('the user chose the following stat:' + this.chosenStat);
  }
//METHOD 2: TAKES IN NUMBER FROM THE FRONTEND NUMBER DROPDOWN
  AssignChosenNumberFromDropdown(int:number){
    this.chosenNumber = int;
    console.log('the number of items to return is:' + this.chosenNumber);
  }

  //METHOD 3: METHOD TO MAP DATA FROM OBSERVABLE ARRAY OF OBJECTS TO USABLE ARRAYS FOR HTML
  /*attempts:
  1) creating an array with the names and paranthese of service methods and appending subscribe - fail
  2) creating a string array and simply changing the strings of the service method name - fail
  
  */
 //METHOD 3: INVOKES SERVICE METHOD AND MAKES HEADERS ARRAY AND COLUMNS ARRAY WITH THE RECEIVED DATA
  AssignChosenService(option:string): number {
    this.chosenStat = option;
    for(let i = 0; i < this.allStatsOptions.length; i++){
      if(this.chosenStat = this.allStatsOptions[i]){
        this.chosenService=i;
        break;
      }
    }
    return this.chosenService;
  }  

  SelectChosenService(){
    switch(this.chosenService){
    case 1:
      this._leaderboardservice.GetTopCurrentBalanceList(this.chosenNumber).subscribe(
        result => {
          this.observableData = result; 
       });
       this.CreateArraysFromObservable();
       console.log('this is case'+this.chosenService);
        break;
    case 2:
      this._leaderboardservice.GetTopEarnedCoinsList(this.chosenNumber).subscribe(
        result => {
          this.observableData = result; 
      });
      this.CreateArraysFromObservable();
      console.log('this is case'+this.chosenService);
      break;
      // case 3:
      //   this._leaderboardservice.GetTopSpentCoinsList(this.chosenNumber).subscribe(
      //     result => {
      //       this.observableData = result; 
      //    });
      //    this.CreateArraysFromObservable();
      //   break;
    }
  }

  //METHOD 4: METHOD THAT CREATES HEADERS ARRAY AND COLUMNS ARRAY WITH THE RECEIVED DATA 
  //I separated this method into method 5 & method 6
  CreatingTableData(){
    this._leaderboardservice.GetTopCurrentBalanceList(this.chosenNumber).subscribe(
      result => {
        this.observableData = result; 
        result.forEach(element => {//these 2 lines of code allows us to create table columns
          this.headersArray=Object.keys(element);
        });
          result.forEach(element => {//these 2 lines of code allows us to create table cells and fill them
            this.columnsArray.push(Object.values(element));
          });
     }
   );
 }

 //METHOD 5: This is a generic method that intakes any service method and maps the observable to a class property
  IntakeObservableData(){
  this._leaderboardservice.GetTopCurrentBalanceList(this.chosenNumber).subscribe(
    result => {
      this.observableData = result; 
   });
}

 //Method 6: This is a generic method that makes arrays to loop over for html display
 CreateArraysFromObservable(){
  this.observableData.forEach(element => {//these 2 lines of code allows us to create table columns
    this.headersArray=Object.keys(element);
  });
  this.observableData.forEach(element => {//these 2 lines of code allows us to create table cells and fill them
    this.columnsArray.push(Object.values(element));
  });
}
  //Method 5: Clean up table and "reset" the observable stream arrays 
  ResetDataArrays(){
    this.observableData=[];
    this.headersArray=[];
    this.columnsArray=[];
  }

  //============================================PAGINATION SECTION======================================
  onChangePageNext() {
    // update current page of items
    this.currentIndex += 10;
    if(this.currentIndex >= this.observableData.length - 10){
      this.currentIndex = this.observableData.length - 10;
      this.currentPage = this.lastpage - 1;
    }
    this.currentPage++;
    console.log(this.currentIndex);
    console.log("collection length = " + this.observableData.length);
    this.pageOfItems = this.observableData.slice(this.currentIndex, this.currentIndex + 10);
    //this.pageOfItems = pageOfItems;
}
  onChangePagePrev() {
    // update current page of items
    this.currentIndex -= 10;
    this.currentPage--;
    if(this.currentIndex <= 0){
      this.currentIndex = 0;
      this.currentPage = 1;
    }
    console.log(this.currentIndex);
    this.pageOfItems = this.observableData.slice(this.currentIndex, this.currentIndex + 10);
    //this.pageOfItems = pageOfItems;
  }
}
