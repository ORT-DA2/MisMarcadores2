import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

const tokenKey = 'currentToken';

@Injectable()
export class ConfigService {

    data: any;

    constructor(private http: HttpClient) {
        this.getData();
    }

    getData() {
        this.http
            .get('../assets/my_json_file.json').subscribe(data => {
                console.log(data);
                this.data = data;
            });
    }
}

