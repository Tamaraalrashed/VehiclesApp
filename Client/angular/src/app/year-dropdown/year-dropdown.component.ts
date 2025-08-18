import { CommonModule } from '@angular/common';
import { Component, EventEmitter,Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-year-dropdown',
  imports: [FormsModule,CommonModule,NgbDropdownModule],
  templateUrl: './year-dropdown.component.html',
  styleUrl: './year-dropdown.component.css'
})

export class YearDropdownComponent {
  @Output() SelectedYear = new EventEmitter<number>();
  startYear: number = 1980;
  endYear: number = new Date().getFullYear();

  years: number[] = this.generateYears();

  selectedYear: number = this.endYear;

  private generateYears(): number[] {
    const years = [];
    for (let year = this.endYear; year >= this.startYear; year--) {
      years.push(year);
    }
    return years;
  }

  onYearChange(year: number): void {
    this.selectedYear=year;
    this.SelectedYear.emit(year);
  }
}
