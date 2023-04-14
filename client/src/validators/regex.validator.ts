import { AbstractControl, ValidatorFn, ValidationErrors } from '@angular/forms';

export class RegexConstants {
  static OneCapitalLetter = /(?=.*?[A-Z])/;
  static OneLowerLetter = /(?=(.*[a-z]))/;
  static OneNumberLetter = /(?=(.*[\d]))/;
  static OneNonNumberLetterSymbol = /(?=(.*[^a-zA-Z0-9]))/;
}

export function RegexPasswordCapitalLetterValidator(
  reg: RegexConstants
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (control.value && !control.value.toString().match(reg)) {
      return { oneCapitalLetter: true };
    }

    return null;
  };
}

export function RegexPasswordLowerLetterValidator(
  reg: RegexConstants
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (control.value && !control.value.toString().match(reg)) {
      return { oneLowerLetter: true };
    }

    return null;
  };
}

export function RegexPasswordLeastNumberValidator(
  reg: RegexConstants
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (control.value && !control.value.toString().match(reg)) {
      return { oneNumber: true };
    }

    return null;
  };
}

export function RegexPasswordOneSymbolValidator(
  reg: RegexConstants
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (control.value && !control.value.toString().match(reg)) {
      return { oneSymbol: true };
    }

    return null;
  };
}
