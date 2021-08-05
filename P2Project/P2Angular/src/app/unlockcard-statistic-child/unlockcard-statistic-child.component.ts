import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile-statistic-child',
  templateUrl: './profile-statistic-child.component.html',
  styleUrls: ['./profile-statistic-child.component.css']
})
export class UnlockCardStatisticChildComponent implements OnInit {
//This is the child component for the unlock card component. It will display aggregate data related to unlock card, such as:
// "percent of people who have unlocked this card" and anything else appropriate
  constructor() { }

  ngOnInit(): void {
  }

}
