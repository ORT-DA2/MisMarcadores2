import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { TokenGuard } from './token-guard';
import icons from 'glyphicons';

import { RouterModule, Routes } from '@angular/router';

import { MenuBarComponent } from './menu-bar/menu-bar.component';
import { NavegacionComponent } from './navegacion/navegacion.component';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { UsuariosListComponent } from './usuarios/usuarios-list.component';
import { DeporteComponent } from './deportes/deporte.component';
import { DeportesListComponent } from './deportes/deportes-list.component';
import { ParticipantesComponent } from './participantes/participantes.component';
import { FixtureComponent } from './fixtures/fixtures.component';
import { ParticipantesListComponent } from './participantes/participantes-list.component';
import { EncuentrosComponent } from './encuentros/encuentros.component';
import { PosicionesComponent } from './posiciones/posiciones-list.component';
import { EncuentrosListComponent } from './encuentros/encuentros-list.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { SesionService } from './servicios/sesion.service';
import { HomeModule } from './home/home.component';
import { YesNoPipe } from './pipes/yesNo.pipe';
import { UsuarioService } from './servicios/usuario.service';
import { DeporteService } from './servicios/deporte.service';
import { ParticipanteService } from './servicios/participante.service';
import { EncuentroService } from './servicios/encuentro.service';
import { BaseApiService } from './servicios/base-api.service';
import { CoreModule } from './core/core.module';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'usuarios', component: UsuariosListComponent, canActivate: [TokenGuard] },
  { path: 'usuarios/agregar', component: UsuariosComponent, canActivate: [TokenGuard] },
  { path: 'usuarios/:nombreUsuario', component: UsuariosComponent, canActivate: [TokenGuard] },
  { path: 'deportes', component: DeportesListComponent, canActivate: [TokenGuard] },
  { path: 'deportes/agregar', component: DeporteComponent, canActivate: [TokenGuard] },
  { path: 'deportes/:nombre', component: DeporteComponent, canActivate: [TokenGuard] },
  { path: 'participantes', component: ParticipantesListComponent, canActivate: [TokenGuard] },
  { path: 'participantes/agregar', component: ParticipantesComponent, canActivate: [TokenGuard] },
  { path: 'participantes/:idParticipante', component: ParticipantesComponent, canActivate: [TokenGuard] },
  { path: 'encuentros', component: EncuentrosListComponent, canActivate: [TokenGuard] },
  { path: 'encuentros/agregar', component: EncuentrosComponent, canActivate: [TokenGuard] },
  { path: 'encuentros/:idEncuentro', component: EncuentrosComponent, canActivate: [TokenGuard] },
  { path: 'posiciones', component: PosicionesComponent, canActivate: [TokenGuard] },
  { path: 'fixture', component: FixtureComponent, canActivate: [TokenGuard] },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DeporteComponent,
    PageNotFoundComponent,
    MenuBarComponent,
    NavegacionComponent,
    UsuariosListComponent,
    UsuariosComponent,
    DeportesListComponent,
    DeporteComponent,
    ParticipantesComponent,
    ParticipantesListComponent,
    PosicionesComponent,
    EncuentrosComponent,
    EncuentrosListComponent,
    FixtureComponent,
    YesNoPipe
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false } // <-- debugging purposes only
    ),
    BrowserModule,
    FormsModule,
    HttpClientModule,
    CoreModule,
    HomeModule
  ],
  providers: [
    TokenGuard,
    SesionService,
    UsuarioService,
    DeporteService,
    ParticipanteService,
    EncuentroService,
    BaseApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
