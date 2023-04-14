import { AbstractControl, ValidatorFn } from '@angular/forms';

export function userSelectSliderValue(isUserSelected: boolean): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null =>
    isUserSelected ? null : { notSelected: true };
}
