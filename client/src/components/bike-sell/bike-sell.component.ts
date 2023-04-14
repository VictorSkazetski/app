import { Options } from '@angular-slider/ngx-slider';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BikeSellService } from 'src/services/bikeSell.service';
import { BikeSellPickPictureService } from 'src/services/bikeSellPickPicture.service';
import { userSelectSliderValue } from 'src/validators/slider.validator';

@Component({
  selector: 'bike-sell',
  templateUrl: './bike-sell.component.html',
  styleUrls: ['./bike-sell.component.sass'],
})
export class BikeSellComponent {
  Object = Object;
  value: number = 0;
  options: Options = {
    floor: 43,
    ceil: 75,
  };
  form: FormGroup;
  isFormSubmitted: boolean = false;

  constructor(
    protected bikePickPicture: BikeSellPickPictureService,
    private bikeSell: BikeSellService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.buildForm();
  }

  get formControls() {
    return this.form.controls;
  }

  buildForm(): void {
    this.form = new FormGroup({
      bikeBrand: new FormControl('', [Validators.required]),
      bikeType: new FormControl('', [Validators.required]),
      bikeFrameSize: new FormControl('', userSelectSliderValue(false)),
      gender: new FormControl('', [Validators.required]),
      uploadImg: new FormControl(''),
      price: new FormControl('', [
        Validators.required,
        Validators.maxLength(10),
        Validators.pattern(/^\d+$/),
      ]),
      isUploadImg: new FormControl(''),
      description: new FormControl('', Validators.required),
    });
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    if (!this.form.valid) {
      return;
    }

    if (!this.formControls['isUploadImg'].value) {
      return;
    }

    let formData = new FormData();
    formData.set('brand', this.form.controls['bikeBrand'].value);
    formData.set('type', this.form.controls['bikeType'].value);
    formData.set('frameSize', this.form.controls['bikeFrameSize'].value);
    formData.set('gender', this.form.controls['gender'].value);
    formData.set('description', this.form.controls['description'].value);
    formData.set('price', this.form.controls['price'].value);
    formData.append('uploadImg', this.form.controls['uploadImg'].value);
    this.bikeSell
      .postBikeAd(this.route.snapshot.url[0].path, formData)
      .subscribe((_) => {
        this.router.navigateByUrl('/bikes');
      });
  }

  userChangeSlider() {
    this.formControls['bikeFrameSize'].setErrors(null);
  }
}
