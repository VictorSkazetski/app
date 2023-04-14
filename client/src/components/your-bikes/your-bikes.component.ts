import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SellBike } from 'src/models/SellBike';
import { BikeSellService } from 'src/services/bikeSell.service';

@Component({
  selector: 'your-bikes',
  templateUrl: './your-bikes.component.html',
  styleUrls: ['./your-bikes.component.sass'],
})
export class YourBikesComponent implements OnInit {
  yourBikes: SellBike[];

  constructor(
    private bikeSell: BikeSellService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.bikeSell
      .getYourBikes(this.route.snapshot.url[0].path)
      .subscribe((bikes: SellBike[]) => {
        this.yourBikes = bikes;
      });
  }

  deleteBike(id: number): void {
    this.bikeSell
      .deleteYourBike(this.route.snapshot.url[0].path + `/${id}`)
      .subscribe(_ => {
        this.yourBikes = this.yourBikes.filter(bike => bike.id !== id);
      })
  }
}
