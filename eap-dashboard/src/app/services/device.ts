import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
export interface Device { id:number; name:string; status:string; lastSeen:string; }
export interface CreateDevice { name:string; status:string; }

@Injectable({ providedIn: 'root' })
export class DeviceService {
  private apiUrl='https://localhost:7144/api/Devices';
  constructor(private http: HttpClient){}
  getAll(){ return this.http.get<Device[]>(this.apiUrl); }
  getOnline(){ return this.http.get<Device[]>(`${this.apiUrl}/online`); }
  add(d:CreateDevice){ return this.http.post<Device>(this.apiUrl,d); }
  delete(id:number){ return this.http.delete(`${this.apiUrl}/${id}`); }
}