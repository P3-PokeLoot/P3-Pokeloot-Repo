//Make a class to mock the service that will be used in a component
import { Observable, of } from "rxjs";
import { User } from "../Models/User";
import { HttpClientModule } from "@angular/common/http";
import { MVPShiny } from "./IMVPShiny";
import { MockServiceData } from "./MockServiceData";

//Each Method is broken into pairs of Mock method + Mock return type
   
export class MockLeaderBoardService {
//constructor for mock class
    constructor(private _http:HttpClientModule){}

    //t1
MockGetTopCurrentBalanceList():Observable<User[]>{//doesn't .Take() in the backend method work like TOP in SQL?
    return MockServiceData;
    }

    //t2
    GetTopEarnedCoinsList():Observable<User[]>{//doesn't .Take() work like TOP?
    return MockServiceData;
    }
    //t3
    GetTopSpentCoinsList():Observable<User[]>{//doesn't .Take() work like TOP?
    return MockServiceData;
    }

    //t4
    GetMostShiningList():Observable<MVPShiny[]>{//doesn't .Take() work like TOP?
    return MockServiceData;
    }

}