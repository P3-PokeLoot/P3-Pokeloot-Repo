import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CardCollectComponent } from './cardcollect.component';
import{ HttpClientTestingModule, HttpTestingController} from '@angular/common/http/testing';
import { CardServiceService } from '../card-service.service';

describe('CardCollectComponent', () => {
  let component: CardCollectComponent;
  let fixture: ComponentFixture<CardCollectComponent>;
 
  /*Make a class to mock the service that will be used in a component
  class MockCardService {
    GetCardsList()
    GetCardTest():Observable<Card[]>{
     returnof (MockCardCollection);
    }
    AddsCardsTest
  }*/ 
  beforeEach(async () => {
    await TestBed.configureTestingModule({
       imports: [HttpClientTestingModule], 
       providers: [
         {provide: CardServiceService}], 
      declarations: [ CardCollectComponent]
    })
    .compileComponents();
    /*    
    fixture = TestBed.createComponent(CardCollectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    Dealing w/ async returns on it */
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CardCollectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  /* it statemetn: does something & return something
  it('should use ngOnInit to get the usercollection', () => {
    expect(component.userCollection.length).toBe(3);
  });  
  it('should use ngOnInit to get the usercollection value', () => {
      expect(componet.userCollection[1].pokeid).toBe('1');
  */ 

  /* Functions within component classes:
  it('CardCollectionFunction1 should do some functionality', () => {
    component.CardCollectionFunction1(parameter);
    expect(component.a_related_value_or_property).toContain('some_value');
  }


  it('Addplayer should add the correct player to the userCollection array', () =>{
    let{
    pokeid:100,
    pokename:'Arceus',
    rarity:5,
    quanNorm:3,
    spriteNorm:"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/132.png",
    quanShiny:0,
    spriteShiny:"spriteshiny",
    },
    expect(component.userCollection.find(x => x.pokename === 'Arceus').toBeFalsy;
    component.AddCard(p1);
    expect(component.userCollection.find(x => x.pokename === 'Arceus').toBeTruthy;
  })

  Testing a method nested in a method - spying
  it('shall return a 5 from BigFunction', () =>{
    spyOnProperty(component, "nestedfunciton").and.returnValue();
    let result = component.BigFunction();
    expect(result).toBe(_a_value);
  });
  */
  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
