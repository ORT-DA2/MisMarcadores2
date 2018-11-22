import { Component, OnInit, OnDestroy } from '@angular/core';
import { NavBarItem } from './NavBarItem';
import { Router, ActivatedRoute } from '@angular/router';
import { SesionService } from '../servicios/sesion.service';
import { UsuarioService } from '../servicios/usuario.service';

@Component({
  selector: 'app-navegacion',
  templateUrl: './navegacion.component.html',
  styleUrls: ['./navegacion.component.css']
})

export class NavegacionComponent implements OnInit {
  public uri = '';
  public nombreUsuario = '';

  public items: NavBarItem[] = [
    { name: 'Inicio', uri: 'home' },
    { name: 'Encuentros', uri: 'encuentros' },
    { name: 'Posiciones', uri: 'posiciones' }
  ];

  constructor(
    private router: Router,
    private a: ActivatedRoute,
    private auth: SesionService,
    private self: UsuarioService
  ) { }

  ngOnInit() {
    this.router.events.subscribe(event => {
      this.uri = location.pathname.split('/')[1];
    });
    this.self.obtenerUsuarioActual().subscribe(
      data => {
        this.nombreUsuario = data.nombreUsuario;
        if  (data.administrador)  {
          this.loadAdminMenu();
        }
      }
    );
  }

  private loadAdminMenu(): void {
    this.items.push({ name: 'Usuarios', uri: 'usuarios' });
    this.items.push({ name: 'Deportes', uri: 'deportes' });
    this.items.push({ name: 'Participantes', uri: 'participantes' });
    this.items.push({ name: 'Fixtures', uri: 'fixture' });
    this.items.push({ name: 'Reporte Deporte', uri: 'reporteDeporte' });
    this.items.push({ name: 'Reporte Participante', uri: 'reporteParticipante' });
  }

  public logout(): void {
    this.auth.logOff();
    this.router.navigate(['login']);
  }
}
