import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DeviceService, Device, CreateDevice } from '../../services/device';

@Component({
  selector: 'app-device-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div style="padding:20px; font-family: sans-serif;">
      <h1>Enterprise Automation - Devices (Day 6)</h1>
      <div style="margin:20px 0; padding:15px; border:1px solid #ccc; border-radius:8px;">
        <h3>Add Device</h3>
        <input [(ngModel)]="newDevice.name" placeholder="Name: Pump-02" style="margin-right:10px; padding:8px;">
        <select [(ngModel)]="newDevice.status" style="margin-right:10px; padding:8px;">
          <option>Online</option><option>Offline</option><option>Maintenance</option>
        </select>
        <button (click)="addDevice()" style="padding:8px 15px; background:#007bff; color:white; border:none; border-radius:4px;">Add</button>
      </div>
      <button (click)="loadAll()" style="margin-right:10px; padding:8px;">All Devices</button>
      <button (click)="loadOnline()" style="padding:8px;">Online Only</button>
      <table border="1" cellpadding="10" style="margin-top:20px; border-collapse:collapse; width:100%;">
        <tr style="background:#f0f0f0;"><th>ID</th><th>Name</th><th>Status</th><th>LastSeen</th><th>Action</th></tr>
        <tr *ngFor="let d of devices">
          <td>{{d.id}}</td><td>{{d.name}}</td><td [style.color]="d.status==='Online'?'green':'red'">{{d.status}}</td>
          <td>{{d.lastSeen | date:'short'}}</td><td><button (click)="deleteDevice(d.id)" style="color:red;">Delete</button></td>
        </tr>
      </table>
    </div>
  `
})
export class DeviceListComponent implements OnInit {
  devices: Device[] = [];
  newDevice: CreateDevice = { name: '', status: 'Online' };
  constructor(private deviceService: DeviceService) {}
  ngOnInit() { this.loadAll(); }
  loadAll() { this.deviceService.getAll().subscribe(data => this.devices = data); }
  loadOnline() { this.deviceService.getOnline().subscribe(data => this.devices = data); }
  addDevice() {
    if(!this.newDevice.name) return;
    this.deviceService.add(this.newDevice).subscribe(() => {
      this.newDevice = { name: '', status: 'Online' }; this.loadAll();
    });
  }
  deleteDevice(id: number) {
    if(confirm('Delete device?')) this.deviceService.delete(id).subscribe(() => this.loadAll());
  }
}