import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html',
  styleUrls: ['./page-not-found.component.css']
})
export class PageNotFoundComponent implements OnInit {

  message: Observable<string>;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.message = this.route
      .queryParamMap
      .pipe(map(params => params.get('message') || ''));
  }

}
