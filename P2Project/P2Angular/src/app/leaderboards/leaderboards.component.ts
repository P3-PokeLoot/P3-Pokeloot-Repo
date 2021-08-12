import { Component, OnInit } from '@angular/core';
//import { resourceLimits } from 'worker_threads';
import { LeaderboardStatsService } from '../service/leaderboards/leaderboard-stats.service';

@Component({
  selector: 'app-leaderboards',
  templateUrl: './leaderboards.component.html',
  styleUrls: ['./leaderboards.component.css']
})
export class LeaderboardsComponent implements OnInit {

  //============================CLASS PROPERTIES SECTION=========================================
    //pagination properties
    pageOfItems:any[];
    currentIndex : number = 0;
    currentPage: number = 1;
    lastpage: number = 1;
    
    //component property that will be updated everytime user selects a dropdown option and click button
    chosenNumber:number;
    chosenStat:string='';
    chosenService:number;
    chosenRarity:string;
    chosenPokename:string;
    
    //store observable streams into here so that it can be iterated on & displayed in html
    observableData:any[]=[];
    headersArray:any[]=[];
    columnsArray:any[]=[];

    

    //array of all options that will reflect onto the "categorical dropdown list"
    tableExists:string;
    allStatsOptions:string[]=[
      'Coin Balance',
      'Coins Earned',
      'Coins Spent',
      'Collection Completion (Percent)',
      'Rarities',
      'Largest Shiny Collection',
      'Card Collection (Unique)',
      'Largest Collection',
    ];
    //array of all options that will reflect onto the "numerical dropdown list"
    allNumbersOptions:number[]=[
      10,
      25,
      50,
      100,
    ];
    allRarityOptions:string[]=[
      'Common',
      'Uncommon',
      'Rare',
      'Mythic',
      'Legendary'
    ];
    

  //===============================CLASS CONSTRUCTOR SECTION===============================
    //DI
  constructor(private _leaderboardservice:LeaderboardStatsService) {
    this.observableData=[];
    this.pageOfItems=[];
    this.chosenNumber=100;
    this.chosenStat='Coin Balance',
    this.chosenService=1;
    this.chosenRarity = 'Common';
    this.chosenPokename='pikachu';
    this.tableExists='false';
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
  //Method 3: rarity dropdown variable assignment
  AssignChosenRarityFromDropdown(rarity:string){
    this.ResetRarity();
    this.chosenRarity = rarity;
    console.log('the number of items to return is:' + this.chosenRarity);
  }
//Method 4: This is a generic method that makes arrays to loop over for html display
CreateArraysFromObservable(){
  this.observableData.forEach(element => {//these 2 lines of code allows us to create table columns
    this.headersArray=Object.keys(element);
  });
  this.observableData.forEach(element => {//these 2 lines of code allows us to create table cells and fill them
    this.columnsArray.push(Object.values(element));
  });
}
 //METHOD 5: Looks at the chosenStat variable and returns it's index value to be used as a 'case value'
  AssignChosenService() {
    console.log('this is the start AssignChosenService');
    console.log(this.chosenService);
      for(let i = 0; i < this.allStatsOptions.length; i++){

        if(this.chosenStat == this.allStatsOptions[i]){
          this.chosenService=i;
          console.log('the new chosenservice value is'+this.chosenService);
          break;
        }
      }
      console.log(this.chosenService);
      console.log('this is the end AssignChosenService');
  }  

  //Method 6: Runs method 4 to get a 'case value', then executes different cases based on the returned 'case value'
  SelectChosenService(){
    //step 1: get case number based off string
    this.tableExists='true';
    this.AssignChosenService();
    //step 2: run a case, based off return value from step 1
    console.log('this is the start selectChosenService');
    console.log(this.chosenService);
    switch(this.chosenService){
    case 0:
      this._leaderboardservice.GetTopCurrentBalanceList(this.chosenNumber).subscribe(
        result => {
          this.observableData = result; 
          this.CreateArraysFromObservable();
       });
       //this.CreateArraysFromObservable();
       console.log('this is case'+this.chosenService);
        break;
    case 1:
      this._leaderboardservice.GetTopEarnedCoinsList(this.chosenNumber).subscribe(
        result => {
          this.observableData = result; 
          this.CreateArraysFromObservable();
      });
      console.log('this is case'+this.chosenService);
      break;
    case 2:
        this._leaderboardservice.GetTopSpentCoinsList(this.chosenNumber).subscribe(
          result => {
            this.observableData = result; 
            this.CreateArraysFromObservable();
         });
        console.log('this is case'+this.chosenService);
        break;
    case 3:
        this._leaderboardservice.GetTopCompletedCollection(this.chosenNumber).subscribe(
          result => {
            this.observableData = result; 
            this.CreateArraysFromObservable();
        });
        console.log('this is case'+this.chosenService);
        break;
    //Rarities
    case 4:
      this._leaderboardservice.GetUserListbyRarity(this.chosenRarity,this.chosenNumber).subscribe(
        result => {
          this.observableData = result; 
          this.CreateArraysFromObservable();
        });
        console.log('this is case'+this.chosenService);
        break;
    case 5:
      this._leaderboardservice.GetMVPShinyUsersList(this.chosenNumber).subscribe(
        result => {
          this.observableData = result; 
          this.CreateArraysFromObservable();
        });
        console.log('this is case'+this.chosenService);
        break;
    case 6:
      this._leaderboardservice.GetUsersTotalCollectionList(this.chosenNumber).subscribe(
        result => {
          this.observableData = result; 
          this.CreateArraysFromObservable();
        });
        console.log('this is case'+this.chosenService);
        break;
    case 7:
      this._leaderboardservice.GetUserTotalAmountList(this.chosenNumber).subscribe(
        result => {
          this.observableData = result; 
          this.CreateArraysFromObservable();
        });
        console.log('this is case'+this.chosenService);
        break;
    }
  }
  CheckTableExists(){
    if(this.tableExists == 'false'){
      this.SelectChosenService();
      return;
    }
    if(this.tableExists=='true'){
      console.log('Leaderboard table already exists');
      return;
    }
  }
  //Method 7: Clean up table by "resetting" the observable stream arrays 
  ResetData(){
    this.observableData=[];
    this.headersArray=[];
    this.columnsArray=[];
    this.tableExists='false';
  }

  ResetNumber(int:number){
    this.chosenNumber=int;
    this.ResetData();
    this.SelectChosenService();
  }
  //
  ResetRarity(){
    this.chosenRarity = '';
  }
  //============================================PAGINATION SECTION======================================

cp=1;
config:any;


}
