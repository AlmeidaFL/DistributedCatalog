import { Component } from '@angular/core';
import { TopBarComponent } from "../top-bar/top-bar.component";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-top-bar-layout',
  standalone: true,
  imports: [TopBarComponent, TopBarComponent, RouterOutlet],
  templateUrl: './top-bar-layout.component.html',
})
export class TopBarLayoutComponent {

}
