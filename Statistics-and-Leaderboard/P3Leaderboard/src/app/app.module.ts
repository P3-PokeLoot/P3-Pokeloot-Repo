import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { DisplaystatisticsComponent } from './displaystatistics/displaystatistics.component';

@NgModule({
  declarations: [
    AppComponent,
    DisplaystatisticsComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
