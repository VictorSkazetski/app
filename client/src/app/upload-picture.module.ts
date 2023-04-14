import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { UploadPictureComponent } from 'src/components/upload-picture/upload-picture.component';

@NgModule({
  declarations: [UploadPictureComponent],
  imports: [CommonModule],
  exports: [UploadPictureComponent],
})
export class UploadPictureModule {}
