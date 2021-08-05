//Make a class to mock the service that will be used in a component
import { Observable, of } from "rxjs";
import { HttpClientModule } from "@angular/common/http";
import { MockServiceData } from "../MockServiceData";

//Each Method is broken into pairs of Mock method + Mock return type
   
export class MockLeaderBoardService {
//constructor for mock class
    constructor(private _http:HttpClientModule){}

    //t1
    MockGetTopCurrentBalanceList():Observable<[]>{
    return MockServiceData.MockCoinBalanceObservable;
    }

    //t2
    MockGetTopEarnedCoinsList():Observable<[]>{
    return MockServiceData.MockTopEarnedObservable;
    }
    //t3
    MockGetTopSpentCoinsList():Observable<[]>{
    return MockServiceData.MockSpentObservable;
    }

    //t4
    MockGetMostShiningList():Observable<[]>{
    return MockServiceData.MockShinyObservable;
    }


}