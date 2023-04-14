import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { BikeAd } from 'src/models/BikeAd';
import { BikeAdService } from 'src/services/bike-ad.service';
import { ProfilePickPictureService } from 'src/services/profilePickPicture.service';

@Component({
  selector: 'bike-ad',
  templateUrl: './bike-ad.component.html',
  styleUrls: ['./bike-ad.component.sass'],
})
export class BikeAdComponent implements OnInit {
  bikeAdEnity: BikeAd;
  userAvatar: string;

  constructor(
    private bikeAd: BikeAdService,
    private router: ActivatedRoute,
    private profilePickPicture: ProfilePickPictureService
  ) {}

  ngOnInit(): void {
    this.bikeAd
      .getBikeAd(this.getUrlToRequest(this.router.snapshot))
      .subscribe((bikeAd: BikeAd) => {
        this.bikeAdEnity = bikeAd;
        this.userAvatar = this.bikeAdEnity.userAvatarImg;
        if (!this.userAvatar) {
          this.userAvatar = this.profilePickPicture.getImgPathByKey(
            this.bikeAdEnity.userAvatarPickImg as number
          );
        }
      });
  }

  getUrlToRequest(snapshot: ActivatedRouteSnapshot): string {
    return `${this.router.snapshot.url[0].path}/${this.router.snapshot.url[1].path}/${this.router.snapshot.url[2].path}`;
  }
}
