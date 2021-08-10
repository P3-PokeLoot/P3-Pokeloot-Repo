import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nbgd-table-pagination',
  templateUrl: './nbgd-table-pagination.component.html',
  styleUrls: ['./nbgd-table-pagination.component.css']
})
export class NbgdTablePaginationComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

//   page = 1;
//   pageSize = 5;
//   collectionSize = observableData.length;

// refreshEntries() {
//     this.pageOfItems = observableData
//     .map((entry, i) => ({id: i + 1, ...entry}))
//     .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
// }
}
