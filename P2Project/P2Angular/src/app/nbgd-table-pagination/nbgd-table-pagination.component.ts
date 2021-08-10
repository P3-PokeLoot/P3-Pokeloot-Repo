import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-nbgd-table-pagination',
  templateUrl: './nbgd-table-pagination.component.html',
  styleUrls: ['./nbgd-table-pagination.component.css']
})
export class NbgdTablePaginationComponent implements OnInit {

  @Input() observableData:any[] =[];
  
  tempdata:any[]=[];
  page = 1;
  chosenPageSize:number;
  collectionSize = this.observableData.length;

  constructor() {
    this.chosenPageSize=5;
   }

  ngOnInit(): void {
  }


  //  refreshEntries() {
  //     this.pageOfItems = observableData
  //     .map((entry, i) => ({id: i + 1, ...entry}))
  //     .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  // }
}
