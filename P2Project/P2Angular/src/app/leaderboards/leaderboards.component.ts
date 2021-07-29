import { Component, OnInit } from '@angular/core';
import { LeaderboardStatsService } from '../leaderboard-stats.service';
import { UserCoinData } from './UserCoinData';

@Component({
  selector: 'app-leaderboards',
  templateUrl: './leaderboards.component.html',
  styleUrls: ['./leaderboards.component.css']
})
export class LeaderboardsComponent implements OnInit {

  //============================CLASS PROPERTIES SECTION=========================================
    //pagination properties
    pageOfItems:any[];
    //@Input() changePage = EventEmitter<any>(true);
    currentIndex : number = 0;
    currentPage: number = 1;
    lastpage: number=1;


    //store observable streams into here so that it can be iterated on & displayed in html
    observableDataFromMethods:any[]=[];
    userCoinDataList:UserCoinData[];
    


    //array of all options that will reflect onto the dropdown list - currently not working for some reason? Was working yesterday this morning :(
    allStatsOptions:string[]=[
      '---',
      'topcoinbalance',
      'topearnedcoins',
      'topcoinsspent',
    ];
    allNumbersOptions:number[]=[
      10,
      25,
      50,
      100,
    ];

    //component property that will be updated everytime user selects a dropdown option and click button
    currentChosenStat:string='';
    currentChosenNumber:number=0;
    

  //===============================CLASS CONSTRUCTOR SECTION===============================
    //DI
  constructor(private _leaderboardservice:LeaderboardStatsService) {
    this.userCoinDataList=[];
    this.observableDataFromMethods=[];
    this.pageOfItems=[];
    
   }

  ngOnInit(): void {
    this.currentChosenNumber=10;
    this.currentChosenStat='account level';
  }

  //==========================================CLASS METHOD SECTION ===============================
  //METHOD 1: FUNCTION THAT TAKES IN STRING FROM THE FRONTEND CATEGORY DROPDOWN
  OnClickQueryFunction(chosenDropdownList:string){
     let counter:string = chosenDropdownList;
    switch(counter){
      case 'shinystats': 
    }
  }
//METHOD 2:FUNCTION THAT TAKES IN NUMBER FROM THE FRONTEND NUMBER DROPDOWN
  OnClickNumberFunction(){}

  //METHOD 3: METHOD TO MAP DATA FROM OBSERVABLE TO CLASS PROPERTY ARRAY
  ServiceTest(){
    this._leaderboardservice.GetTopCurrentBalanceList().subscribe(
      result => {
        this.observableDataFromMethods = result; 
            // for (let i = 0; i < this.observableDataFromMethods.length; i++) {
            //   let UserName = this.observableDataFromMethods[i].UserName;
            //   let AccountLevel = this.observableDataFromMethods[i].AccountLevel;
            //   let CoinBalance= this.observableDataFromMethods[i].CoinBalance;
            //   let TotalCoinsEarned= this.observableDataFromMethods[i].TotalCoinsEarned;
            //   let CoinsSpent= this.observableDataFromMethods[i].TotalCoinsEarned-this.observableDataFromMethods[i].CoinBalance;
            //   let coindata:UserCoinData = {UserName,AccountLevel,CoinBalance,TotalCoinsEarned,CoinsSpent};
            //   this.userCoinDataList.push(coindata);
            // };
            // console.log(this.userCoinDataList);
        console.log(this.observableDataFromMethods);
      }
    );
  }

  //============================================PAGINATION SECTION======================================
  onChangePageNext() {
    // update current page of items
    this.currentIndex += 10;
    if(this.currentIndex >= this.observableDataFromMethods.length - 10){
      this.currentIndex = this.observableDataFromMethods.length - 10;
      this.currentPage = this.lastpage - 1;
    }
    this.currentPage++;
    console.log(this.currentIndex);
    console.log("collection length = " + this.observableDataFromMethods.length);
    this.pageOfItems = this.observableDataFromMethods.slice(this.currentIndex, this.currentIndex + 10);
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
    this.pageOfItems = this.observableDataFromMethods.slice(this.currentIndex, this.currentIndex + 10);
    //this.pageOfItems = pageOfItems;
  }
}
