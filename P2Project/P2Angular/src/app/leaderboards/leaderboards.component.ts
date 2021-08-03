import { Component, OnInit } from '@angular/core';
import { LeaderboardStatsService } from '../leaderboard-stats.service';
import { UserCoinData } from './UserCoinData';

@Component({
  selector: 'app-leaderboards',
  templateUrl: './leaderboards.component.html',
  styleUrls: ['./leaderboards.component.css']
})
export class LeaderboardsComponent implements OnInit {

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


    //store observable streams into here so that it can be iterated on & displayed in html
    observableData:any[]=[];
    headersArray:any[]=[];
    columnsArray:any[]=[];
    serviceArray:any[]=[
      this._leaderboardservice.GetTopCurrentBalanceList(),
      this._leaderboardservice.GetTopEarnedCoinsList(),
      this._leaderboardservice.GetTopSpentCoinsList(),
    ];
    

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

    //component property that will be updated everytime user selects a dropdown option and click button
    chosenNumber:number=10;
    chosenStat:string='';
    

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

  ChooseServiceMethod(option:string){ //function takes in a string value from "categorical dropdown list"
    this.chosenStat = option;
    console.log('the user chose the following stat:' + this.chosenStat);
  }
//METHOD 2: TAKES IN NUMBER FROM THE FRONTEND NUMBER DROPDOWN
  ChooseNumberOfRows(int:number){
    this.chosenNumber = int;
    console.log('the number of items to return is:' + this.chosenNumber);
  }

  //METHOD 3: METHOD TO MAP DATA FROM OBSERVABLE ARRAY OF OBJECTS TO USABLE ARRAYS FOR HTML
  /*attempts:
  1) creating an array with the names and paranthese of service methods and appending subscribe - fail
  2) creating a string array and simply changing the strings of the service method name - fail
  
  */
 //METHOD 3: INVOKES SERVICE METHOD AND MAKES HEADERS ARRAY AND COLUMNS ARRAY WITH THE RECEIVED DATA
  CreatingTableData(chosenStat:string,chosenNumber:number){
     this._leaderboardservice.GetTopCurrentBalanceList().subscribe(
       result => {
         this.observableData = result; 
         result.forEach(element => {
           this.headersArray=Object.keys(element);
         });
           result.forEach(element => {
             this.columnsArray.push(Object.values(element));
           });
      }
    );
  }

  //Method 4: Clean up table and "reset" the observable stream arrays 
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
