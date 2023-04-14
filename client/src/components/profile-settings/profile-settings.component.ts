import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserProfile } from 'src/models/UserProfile';
import { ProfileService } from 'src/services/profile.service';
import { ProfilePickPictureService } from 'src/services/profilePickPicture.service';
import { ShareImgService } from 'src/services/shareImg.service';

@Component({
  selector: 'profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.sass'],
})
export class ProfileSettingsComponent {
  form: FormGroup;
  readonly minAge = 18;
  maxDob: Date;
  userUploadImg: string;
  userAvatar: string;
  pickImg: string | number;

  constructor(
    private profilePickPicture: ProfilePickPictureService,
    private profile: ProfileService,
    private router: ActivatedRoute,
    private shareImg: ShareImgService
  ) {}

  ngOnInit() {
    const today = new Date();
    this.maxDob = new Date(
      today.getFullYear() - this.minAge,
      today.getMonth(),
      today.getDate()
    );
    this.profile
      .getUserProfile(this.router.snapshot.url[0].path)
      .subscribe((userProfile: UserProfile) => {
        this.updateUserProfile(userProfile);
        this.buildForm(this.pickImg, userProfile.birthday, userProfile.phone);
      });
  }

  buildForm(pickImg: string | number, birthday: string, phone: string) {
    this.form = new FormGroup({
      pickImg: new FormControl(pickImg),
      uploadImg: new FormControl(''),
      isUploadImg: new FormControl(''),
      birthDay: new FormControl(birthday ? new Date(birthday) : ''),
      phone: new FormControl(phone),
    });
  }

  onSubmit(): void {
    this.profile
      .saveProfile(this.router.snapshot.url[0].path, this.getFormData())
      .subscribe((userProfile: UserProfile) => {
        this.updateUserProfile(userProfile);
      });
  }

  getFormData(): FormData {
    let dateOfBirth = this.form.controls['birthDay']?.value
      ? this.form.controls['birthDay']?.value.toDateString()
      : '';
    let formData = new FormData();
    formData.set('pickImg', this.form.controls['pickImg'].value);
    formData.append('uploadImg', this.form.controls['uploadImg']?.value);
    formData.set('isUploadImg', this.form.controls['isUploadImg'].value);
    formData.set('birthDay', dateOfBirth);
    formData.set('phone', this.form.controls['phone'].value);

    return formData;
  }

  updateUserProfile(userProfile: UserProfile): void {
    this.userUploadImg = userProfile.img;
    this.pickImg = userProfile.pickImg === null ? '' : userProfile.pickImg;
    if (this.userUploadImg && this.pickImg === '') {
      this.userAvatar = this.userUploadImg;
    } else if (this.userUploadImg === null && this.pickImg !== '') {
      this.userAvatar = this.profilePickPicture.getImgPathByKey(
        this.pickImg as number
      );
    } else if (this.userUploadImg !== null && this.pickImg !== '') {
      this.userAvatar = this.userUploadImg;
    }

    this.shareImg.shareImgPath(this.userAvatar);
  }
}
