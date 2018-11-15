import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { SessionService } from './servicios/session.service';
import { HomeComponent } from './home/home.component';
import { DeporteComponent } from './deportes/deporte.component';
import { UserService } from './servicios/usuario.service';
import { BaseApiService } from './servicios/base-api.service';
import { CoreModule } from './core/core.module';

const appRoutes: Routes = [
  {
    path: '', component: HomeComponent,
    children: [
      { path: 'home', component: HomeComponent }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'deportes', component: DeporteComponent },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    DeporteComponent,
    PageNotFoundComponent,
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
  ],
  providers: [
    SessionService,
    UserService,
    BaseApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
