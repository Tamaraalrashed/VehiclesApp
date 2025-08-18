import { Component,inject,OnInit } from '@angular/core';

import { AppService } from './Services/app.service';
import { Make, Type,Vehicle } from './Models/Vehicle';
import { CommonModule} from '@angular/common';
import {catchError, Observable, of, shareReplay } from 'rxjs';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { YearDropdownComponent } from './year-dropdown/year-dropdown.component';
import {FormsModule} from '@angular/forms';
export enum SelectionMode{

  Type='type',
  ModelYear='ModelYear',
}
@Component({
  selector: 'app-root',
  imports: [  CommonModule,ScrollingModule,NgbDropdownModule,YearDropdownComponent,FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})


export class AppComponent  implements  OnInit{

title = 'VehilcesApp';

SelectionMode=SelectionMode;
// SelectedMode:SelectionMode=SelectionMode.Type;
  vehicleMakes$: Observable<Make[]>= of([]);
SelectedParts:any={
  SelectedMake:null,
  SelectedType:null,
  SelectedYear:0,
  IsMakeSelected:false,
  IsTypeSelected:false,
  IsModelYearSelected:false,
}
  public  vehicleTypes:Type[]=[];
public  vehicles:Vehicle[]=[];
  public SelectedModelYear:number=0;
  private appsService= inject(AppService);
  private _selectedMode: SelectionMode = SelectionMode.Type;
  get SelectedMode(): SelectionMode {
    return this._selectedMode;
  }

  set SelectedMode(mode: SelectionMode) {
    if (this._selectedMode !== mode) {
      this._selectedMode = mode;
      this.vehicles = [];
  }}
  ngOnInit(): void {
   this.vehicleMakes$ = this.appsService.getVehicleMakes().pipe(
      shareReplay(1) ,
    catchError(error => {
      console.error('Error loading makes', error);
      return of([]);
    }));

  }

  onMakeSelect(make:Make){
    this.vehicleTypes=[];
    this.SelectedParts.SelectedMake=null;
    this.appsService.getVehicleTypes(make.id).subscribe(data=>{
     this.SelectedParts.IsMakeSelected=true;
     this.SelectedParts.SelectedMake=make;
    this.vehicleTypes=data;
    });
  }

  onTypeSelect(type:Type){
    this.SelectedParts.SelectedType=null;
      this.SelectedParts.IsTypeSelected=true;
      this.SelectedParts.SelectedType=type;

  }

  onSelectYear(year:any){
    this.SelectedParts.IsModelYearSelected=true;
    this.SelectedModelYear=year;
    this.appsService.getVehicleModels(this.SelectedParts.SelectedMake.id,year).subscribe(
      data=>{
        this.vehicles=[];
        this.vehicles=data;
      }
    )
  }

}
