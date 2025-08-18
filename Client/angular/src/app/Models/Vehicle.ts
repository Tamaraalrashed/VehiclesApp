export interface Make {
  id: number;
  name: string;
}
export interface Type {
  id: number;
  name: string;
}
export interface Model {
  id: number;
  name: string;
}

export interface Vehicle {
 make :Make;
model : Model
}
