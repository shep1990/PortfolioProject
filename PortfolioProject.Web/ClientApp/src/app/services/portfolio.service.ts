import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CV } from '../models/CV';
import { Portfolio } from '../models/portfolio';

@Injectable({
  providedIn: 'root'
})
export class PortfolioService {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) { }

  getAllPortfolioItems(): Observable<Portfolio[]> {
    return this.http.get<Portfolio[]>(this.baseUrl + 'api/portfolio/items')
  }

  getAllCVItems(): Observable<CV[]> {
    return this.http.get<CV[]>(this.baseUrl + 'api/cv/cvItems')
  }
}
