import {inject, Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import { Make,Type,Vehicle } from '../Models/Vehicle';

declare global {
  interface Window { _env_: any; }
}
@Injectable({
  providedIn: 'root'
})
export class AppService {
  private baseurl = window._env_.API_URL;

  private http=inject(HttpClient);
  getVehicleMakes(){
    return this.http.get<Make[]>(this.baseurl+'GetAllMakes');
  }
  getVehicleTypes(makeId:number){

    let params=new HttpParams();
    params=params.append('makeId',makeId.toString());

    return this.http.get<Type[]>(this.baseurl+'GetAllTypes',{params:params})

  }
  getVehicleModels(makeId:number,year:number){
    let params=new HttpParams();
    params=params.append('makeId',makeId.toString());
    params=params.append('modelYear',year.toString());
    return this.http.get<Vehicle[]>(this.baseurl+'GetModelsForMake',{params:params})
  }
}
