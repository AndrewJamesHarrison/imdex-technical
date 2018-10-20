import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatExpansionModule } from '@angular/material/expansion';

import { DrillHoleComponent } from './../components/drillhole.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public drillholes: DrillHole[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<DrillHole[]>(baseUrl + 'api/Data/sample').subscribe(result => {
      this.drillholes = result;
    }, error => console.error(error));
  }
}
