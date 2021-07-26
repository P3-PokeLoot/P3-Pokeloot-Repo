  //Make a class to mock the service that will be used in a component
 import { Observable, of } from "rxjs";
import { User } from "./IUser";
import { MockUser } from "./MockUser";
 
 
  export class MockAccountService {
    GetUserProfile():Observable<User>{
      return of(MockUser);
    }
  }