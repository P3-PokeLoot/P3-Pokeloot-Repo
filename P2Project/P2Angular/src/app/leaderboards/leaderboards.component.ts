import { Component, OnInit } from '@angular/core';
//import { resourceLimits } from 'worker_threads';
import { LeaderboardStatsService } from '../service/leaderboards/leaderboard-stats.service';

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
    chosenNumber:number;
    chosenStat:string='';
    chosenService:number;
    
    //store observable streams into here so that it can be iterated on & displayed in html
    observableData:any[]=[];
    headersArray:any[]=[];
    columnsArray:any[]=[];

    

    //array of all options that will reflect onto the "categorical dropdown list"
    allStatsOptions:string[]=[
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
    this.chosenNumber=1;
    this.chosenService=1;
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

//Method 3: This is a generic method that makes arrays to loop over for html display
CreateArraysFromObservable(){
  this.observableData.forEach(element => {//these 2 lines of code allows us to create table columns
    this.headersArray=Object.keys(element);
  });
  this.observableData.forEach(element => {//these 2 lines of code allows us to create table cells and fill them
    this.columnsArray.push(Object.values(element));
  });
}
 //METHOD 4: Looks at the chosenStat variable and returns it's index value to be used as a 'case value'
  AssignChosenService() {
    console.log('this is the start AssignChosenService');
    console.log(this.chosenService);

      for(let i = 0; i < this.allStatsOptions.length; i++){
        //when we "match", stop the loop
        if(this.chosenStat == this.allStatsOptions[i]){
          this.chosenService=i;
          console.log('the new chosenservice value is'+this.chosenService);
          break;
        }
      }
      console.log(this.chosenService);
      console.log('this is the end AssignChosenService');
  }  

  //Method 5: Runs method 4 to get a 'case value', then executes different cases based on the returned 'case value'
  SelectChosenService(){
    //step 1: get case number based off string
    this.AssignChosenService();
    //step 2: run a case, based off return value from step 1
    console.log('this is the start selectChosenService');
    console.log(this.chosenService);
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
      case 3:
        this._leaderboardservice.GetTopSpentCoinsList(this.chosenNumber).subscribe(
          result => {
            this.observableData = result; 
         });
         this.CreateArraysFromObservable();
        console.log('this is case'+this.chosenService);
        break;
    }
  }

  //Method 6: Clean up table by "resetting" the observable stream arrays 
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
