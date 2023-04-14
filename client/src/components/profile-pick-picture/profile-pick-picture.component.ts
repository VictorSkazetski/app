import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  ViewChild,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ProfilePickPictureService } from 'src/services/profilePickPicture.service';

@Component({
  selector: 'profile-pick-picture',
  templateUrl: './profile-pick-picture.component.html',
  styleUrls: ['./profile-pick-picture.component.sass'],
})
export class ProfilePickPictureComponent implements AfterViewInit {
  @ViewChild('slideContainer')
  slideContainer: ElementRef<HTMLElement>;
  @ViewChild('slideContainer')
  slider: ElementRef<HTMLElement>;
  @ViewChild('prevButton') prevButton: ElementRef<HTMLElement>;
  @ViewChild('nextButton') nextButton: ElementRef<HTMLElement>;
  @Input() form: FormGroup;
  Object = Object;

  constructor(protected profilePickPicture: ProfilePickPictureService) {}

  ngAfterViewInit(): void {
    let isDown = false;
    let startX = 0;
    let scrollLeft = 0;
    this.nextButton.nativeElement.addEventListener('click', () => {
      const slideWidth = this.slider.nativeElement.clientWidth;
      this.slideContainer.nativeElement.scrollLeft += slideWidth;
    });
    this.prevButton.nativeElement.addEventListener('click', () => {
      const slideWidth = this.slider.nativeElement.clientWidth;
      this.slideContainer.nativeElement.scrollLeft -= slideWidth;
    });
    this.slider.nativeElement.addEventListener('mousedown', (e) => {
      isDown = true;
      this.slider.nativeElement.classList.add('active');
      startX = e.pageX - this.slider.nativeElement.offsetLeft;
      scrollLeft = this.slider.nativeElement.scrollLeft;
    });
    this.slider.nativeElement.addEventListener('mouseleave', (_) => {
      isDown = false;
      this.slider.nativeElement.classList.remove('active');
    });
    this.slider.nativeElement.addEventListener('mouseup', (_) => {
      isDown = false;
      this.slider.nativeElement.classList.remove('active');
    });
    this.slider.nativeElement.addEventListener('mousemove', (e) => {
      if (!isDown) {
        return;
      }

      e.preventDefault();
      const x = e.pageX - this.slider.nativeElement.offsetLeft;
      const SCROLL_SPEED = 3;
      const walk = (x - startX) * SCROLL_SPEED;
      this.slider.nativeElement.scrollLeft = scrollLeft - walk;
    });
  }
}
