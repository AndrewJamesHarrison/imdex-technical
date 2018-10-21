import { Component, Input} from '@angular/core';

@Component({
  selector: 'drill-hole',
  templateUrl: './drillhole.component.html',
})
export class DrillHoleComponent {
  @Input() drillhole: DrillHole;
}
