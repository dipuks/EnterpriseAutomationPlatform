import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

export interface Device { id:number; name:string; status:string; lastSeen:string; }
export interface CreateDevice { name:string; status:string; }

@Injectable({ providedIn: 'root' })
export class DeviceService {
  // If you keep getting cert errors, change to http://localhost:5144/api/Devices 
  // (check EAP.Api/Properties/launchSettings.json for your http port)
  private apiUrl='https://localhost:7144/api/Devices';
  
  constructor(private http: HttpClient){}

  getAll(search: string = ''){ 
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    return this.http.get<Device[]>(this.apiUrl, { params }); 
  }

  // keep old names for backward compatibility
  getDevices(search:string=''){ return this.getAll(search); }

  getOnline(){ return this.http.get<Device[]>(`${this.apiUrl}/online`); }
  
  add(d:CreateDevice){ return this.http.post<Device>(this.apiUrl,d); }
  addDevice(d:CreateDevice){ return this.add(d); }

  update(id:number, d:CreateDevice){ return this.http.put<Device>(`${this.apiUrl}/${id}`, d); }
  updateDevice(id:number, d:CreateDevice){ return this.update(id,d); }

  delete(id:number){ return this.http.delete(`${this.apiUrl}/${id}`); }
  deleteDevice(id:number){ return this.delete(id); }
}