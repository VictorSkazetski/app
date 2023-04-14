import { Component, OnInit } from '@angular/core';
import { MatSelectChange } from '@angular/material/select';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { BikeAd } from 'src/models/BikeAd';
import { BikeAdFilter } from 'src/models/BikeAdFilter';
import { AllBikeAdsService } from 'src/services/all-bike-ads.service';

@Component({
  selector: 'all-bike-ads',
  templateUrl: './all-bike-ads.component.html',
  styleUrls: ['./all-bike-ads.component.sass'],
})
export class AllBikeAdsComponent implements OnInit {
  allBikeAdsTable: MatTableDataSource<BikeAd>;
  dataSource: MatTableDataSource<BikeAd>;
  dataSourceFilters: MatTableDataSource<BikeAd>;
  filterDictionary = new Map<string, string>();
  columnsToDisplay: string[] = [
    'brand',
    'type',
    'frameSize',
    'gender',
    'uploadImgPath',
    'id',
  ];
  allBikeAdsFilters: BikeAdFilter[] = [];
  brands: string[] = ['-', 'Aist', 'Cube', 'Fuji', 'Trek'];
  type: string[] = ['-', 'city', 'gravel', 'hybrid', 'mountain'];
  frameSize: string[] = [
    '-',
    '43',
    '44',
    '45',
    '46',
    '47',
    '48',
    '49',
    '50',
    '51',
    '52',
    '53',
    '54',
    '55',
    '56',
    '57',
    '58',
    '59',
    '60',
    '61',
    '62',
    '63',
    '64',
    '65',
    '67',
    '68',
    '69',
    '70',
    '71',
    '72',
    '73',
  ];
  genders: string[] = ['-', 'Мужской', 'Женский', 'Унисекс'];
  defaultValue = '-';

  constructor(
    private allBikeAds: AllBikeAdsService,
    private router: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.allBikeAdsFilters.push({
      name: 'brand',
      options: this.brands,
      defaultValue: this.defaultValue,
    });
    this.allBikeAdsFilters.push({
      name: 'type',
      options: this.type,
      defaultValue: this.defaultValue,
    });
    this.allBikeAdsFilters.push({
      name: 'frameSize',
      options: this.frameSize,
      defaultValue: this.defaultValue,
    });
    this.allBikeAdsFilters.push({
      name: 'gender',
      options: this.genders,
      defaultValue: this.defaultValue,
    });
    this.allBikeAds
      .getBikesAds(this.router.snapshot.url[0].path)
      .subscribe((allBikeAds: BikeAd[]) => {
        console.log(allBikeAds);
        this.dataSource = new MatTableDataSource<BikeAd>(allBikeAds);
        this.dataSourceFilters = new MatTableDataSource<BikeAd>(allBikeAds);
        this.dataSourceFilters.filterPredicate = function (record, filter) {
          let map = new Map(JSON.parse(filter));
          let isMatch = false;
          for (let [key, value] of map) {
            isMatch = value === '-' || record[key as keyof BikeAd] == value;
            if (!isMatch) return false;
          }
          return isMatch;
        };
      });
  }

  applyBikeFilter(ob: MatSelectChange, bikeFilter: BikeAdFilter) {
    this.filterDictionary.set(bikeFilter.name, ob.value);
    let jsonString = JSON.stringify(
      Array.from(this.filterDictionary.entries())
    );
    this.dataSourceFilters.filter = jsonString;
  }
}
