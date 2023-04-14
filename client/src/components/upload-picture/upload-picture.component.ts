import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'upload-picture',
  templateUrl: './upload-picture.component.html',
  styleUrls: ['./upload-picture.component.sass'],
})
export class UploadPictureComponent implements OnChanges {
  loaded: boolean = false;
  imageSrc: string = '';
  @Input() form: FormGroup;
  @Input() userUploadImg: string;

  constructor() {
    this.loaded = true;
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.loaded = true;
    this.imageSrc = this.userUploadImg;
    if (this.imageSrc !== null) {
      this.form.patchValue({
        isUploadImg: false,
      });
    }
  }

  onFileChange(e: any) {
    let file = e.target.files[0] as File;
    let pattern = /image-*/;
    let reader = new FileReader();
    if (!file.type.match(pattern)) {
      alert('invalid format');
      return;
    }

    reader.readAsDataURL(file);
    reader.onload = (event) => {
      this.imageSrc = event.target.result as string;
      this.loaded = true;
      this.form.patchValue({
        uploadImg: file,
      });
      this.form.patchValue({
        isUploadImg: true,
      });
    };
  }

  delete() {
    this.imageSrc = null;
    this.loaded = false;
    this.form.patchValue({
      isUploadImg: '',
    });
  }
}
