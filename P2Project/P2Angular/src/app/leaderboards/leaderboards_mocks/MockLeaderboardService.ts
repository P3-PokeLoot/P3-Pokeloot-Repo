//Make a class to mock the service that will be used in a component
import { Observable, of } from "rxjs";
import { HttpClientTestingModule } from "@angular/common/http/testing"; 
import { MockServiceData } from "./MockServiceData";

import { MockCoinBalanceObservable } from "./MockCoinBalanceData";

//Each Method is broken into pairs of Mock method + Mock return type
   
export class MockLeaderBoardService {
//constructor for mock class
    constructor(private _http:HttpClientTestingModule){}

    //t1
    MockGetTopCurrentBalanceList(){
    return MockCoinBalanceObservable;
    }

    MockGeTopCompletedCollection(){
    return TopPercentModel_Coin;
    }

    MockGetMVPShinyUsersList(){
    return MVPShinyModel_Shiny;
    }


}