import { Component, Input, OnInit } from '@angular/core';


@Component({
  selector: 'app-nbgd-table-pagination',
  templateUrl: './nbgd-table-pagination.component.html',
  styleUrls: ['./nbgd-table-pagination.component.css']
})
export class NbgdTablePaginationComponent implements OnInit {

  @Input() columnsArray:any[] =[];
  @Input() headersArray:any[] =[];
  @Input() observableData:any[] =[];
  cp=1;
  config:any;
  constructor() {
   }

  ngOnInit(): void {
  }



}
