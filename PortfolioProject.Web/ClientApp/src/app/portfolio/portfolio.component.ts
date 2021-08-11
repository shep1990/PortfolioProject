import { Component, OnInit } from '@angular/core';
import { PortfolioService } from '../services/portfolio.service';

@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.css']
})
export class PortfolioComponent implements OnInit {

  portfolioList: any[];
  errorMessage: string = '';

  constructor(
    private portfolioService: PortfolioService
  )
  {
  }

  ngOnInit() {
    this.getPortfolioItems();
  }

  getPortfolioItems() {
    this.portfolioService.getAllPortfolioItems().subscribe((res: any) => {
      this.portfolioList = res.data;
    },
    (error) => {
      "There was an error"
    })
  }

}
