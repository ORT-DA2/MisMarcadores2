import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { TokenGuard } from './token-guard';

import { RouterModule, Routes } from '@angular/router';

import { MenuBarComponent } from './menu-bar/menu-bar.component';
import { NavegacionComponent } from './navegacion/navegacion.component';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { SesionService } from './servicios/sesion.service';
import { HomeModule } from './home/home.component';
import { DeporteComponent } from './deportes/deporte.component';
import { UsuarioService } from './servicios/usuario.service';
import { BaseApiService } from './servicios/base-api.service';
import { CoreModule } from './core/core.module';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'deportes', component: DeporteComponent },
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
    BaseApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
