import {Component, OnInit, ViewChild} from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { Router } from "@angular/router";
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
    templateUrl: './user-reports.page.html',
    styleUrls: ['./user-reports.page.css']
})
export class UserReportsPage implements OnInit {
    reports: any[];
    displayedColumns: string[] = ['fullName', 'linkSent', 'started', 'finished', 'passedSubTasksCount', 'cheating'];
    dataSource = new MatTableDataSource<any>([]);
    filter = null;
    filterPredicate = null;

    @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
    @ViewChild(MatSort, {static: true}) sort: MatSort;

    constructor(private backendService: BackendService, private router: Router) {
    }

    ngOnInit(): void {
        this.backendService.getUserTaskReports().then((data) => {
            this.reports = data;
            this.dataSource = new MatTableDataSource(data);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        });
    }

    openReport(rep) {
        this.router.navigate(['user-task-report', encodeURI(rep.userEmail)]);
    }

  filterChanged(event) {
    if (!event) {
      this.filter = null;
      return;
    }
    this.filter = event.trim();
    if (this.filterPredicate) {
      this.dataSource.filterPredicate = this.filterPredicate;
    }
    this.dataSource.filter = this.filter;
  }

  setFilter(name: string) {
    const fullNameFilter =
      (data, filter) => filter !== 'none' ? data.fullName.toLowerCase().indexOf(filter.toLowerCase()) !== -1 : true;

    switch (name) {
    case 'all':
      this.dataSource.filterPredicate = (data, filter) => {
        return fullNameFilter(data, filter);
      };
      break;
    case 'expired':
      this.dataSource.filterPredicate = (data, filter) => {
        return data.isLinkExpired && !data.finishedDt && fullNameFilter(data, filter);
      };
      break;
    case 'notPassed':
      this.dataSource.filterPredicate = (data, filter) => {
        return !data.isAllPassed && (data.isFinished || data.isLinkExpired) && fullNameFilter(data, filter);
      };
      break;
    case 'inProgress':
      this.dataSource.filterPredicate = (data, filter) => {
        return data.isStarted && !data.isFinished && !data.isLinkExpired && fullNameFilter(data, filter);
      };
      break;
    case 'passed':
      this.dataSource.filterPredicate = (data, filter) => {
        return data.isAllPassed && data.isFinished && fullNameFilter(data, filter);
      };
      break;
    case 'cheating':
      this.dataSource.filterPredicate = (data, filter) => {
        return data.cheating && fullNameFilter(data, filter);
      };
      break;
    }

    this.filterPredicate = this.dataSource.filterPredicate;
    this.dataSource.filter = this.filter || 'none';
  }
}
