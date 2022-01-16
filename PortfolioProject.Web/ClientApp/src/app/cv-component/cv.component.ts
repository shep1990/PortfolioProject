import { Component, OnInit } from '@angular/core';
import { PortfolioService } from '../services/portfolio.service';

@Component({
  selector: 'app-cv-component',
  templateUrl: './cv.component.html',
  styleUrls: ['./cv.component.css']
})
export class CvComponent implements OnInit {

  cvList: any[];
  errorMessage: string = '';

  constructor(
    private portfolioService: PortfolioService
  ) {
  }

  ngOnInit() {
    this.getCvItems();
  }

  getCvItems() {
    this.portfolioService.getAllCVItems().subscribe((res: any) => {
      this.cvList = res.data;
    },
      (error) => {
        "There was an error"
      })
  }
}
