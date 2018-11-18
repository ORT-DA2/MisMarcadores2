import { Component, OnInit } from '@angular/core';
import { SesionService } from '../servicios/sesion.service';

@Component({
  selector: 'app-menu-bar',
  templateUrl: './menu-bar.component.html',
  styleUrls: ['./menu-bar.component.css']
})
export class MenuBarComponent implements OnInit {

  constructor(private auth: SesionService) { }

  ngOnInit() {
  }

  public showNavBar(): boolean {
    return this.auth.getToken() !== 'no-token';
  }
}
