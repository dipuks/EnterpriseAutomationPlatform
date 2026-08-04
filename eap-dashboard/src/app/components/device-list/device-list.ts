import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DeviceService, Device, CreateDevice } from '../../services/device';

@Component({
  selector: 'app-device-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="p-6 max-w-6xl mx-auto font-sans">
      <h1 class="text-2xl font-bold mb-4">Enterprise Automation - Devices (Day 7)</h1>
      
      <!-- Add + Search -->
      <div class="flex flex-wrap gap-3 mb-4 p-4 border rounded-lg bg-gray-50">
        <input [(ngModel)]="newDevice.name" placeholder="Name: Pump-02" class="border p-2 rounded w-48">
        <select [(ngModel)]="newDevice.status" class="border p-2 rounded">
          <option>Online</option><option>Offline</option><option>Maintenance</option>
        </select>
        <button (click)="addDevice()" class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">Add</button>
        
        <input [(ngModel)]="search" (ngModelChange)="onSearch($event)" placeholder="Search devices..." class="border p-2 rounded ml-auto w-64">
        <button (click)="loadAll()" class="border px-3 py-2 rounded">All</button>
        <button (click)="loadOnline()" class="border px-3 py-2 rounded">Online Only</button>
      </div>

      <!-- Table -->
      <div class="bg-white shadow rounded overflow-hidden">
        <table class="w-full text-left">
          <thead class="bg-gray-100">
            <tr><th class="p-3">ID</th><th>Name</th><th>Status</th><th>LastSeen</th><th>Action</th></tr>
          </thead>
          <tbody>
            <tr *ngFor="let d of devices" class="border-t hover:bg-gray-50">
              <td class="p-3">{{d.id}}</td>
              <td class="font-medium">{{d.name}}</td>
              <td><span class="px-2 py-1 rounded text-xs font-semibold" [ngClass]="d.status==='Online'?'bg-green-100 text-green-700': d.status==='Offline'?'bg-red-100 text-red-700':'bg-yellow-100 text-yellow-700'">{{d.status}}</span></td>
              <td class="text-sm text-gray-600">{{d.lastSeen | date:'short'}}</td>
              <td class="flex gap-3 p-3">
                <button (click)="startEdit(d)" class="text-blue-600 hover:underline">Edit</button>
                <button (click)="deleteDevice(d.id)" class="text-red-600 hover:underline">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
        <div *ngIf="devices.length===0" class="p-8 text-center text-gray-500">No devices found</div>
      </div>

      <!-- Edit Modal -->
      <div *ngIf="editing" class="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
        <div class="bg-white p-6 rounded shadow w-96">
          <h3 class="font-bold text-lg mb-3">Edit Device #{{editing.id}}</h3>
          <input [(ngModel)]="editing.name" class="border p-2 w-full rounded mb-2">
          <select [(ngModel)]="editing.status" class="border p-2 w-full rounded mb-4">
            <option>Online</option><option>Offline</option><option>Maintenance</option>
          </select>
          <div class="flex justify-end gap-2">
            <button (click)="editing=null" class="px-4 py-2 border rounded">Cancel</button>
            <button (click)="saveEdit()" class="bg-blue-600 text-white px-4 py-2 rounded">Save</button>
          </div>
        </div>
      </div>

      <!-- Toast -->
      <div *ngIf="toast" class="fixed bottom-4 right-4 bg-gray-900 text-white px-4 py-2 rounded shadow-lg">{{toast}}</div>
    </div>
  `
})
export class DeviceListComponent implements OnInit {
  devices: Device[] = [];
  newDevice: CreateDevice = { name: '', status: 'Online' };
  search = '';
  editing: Device | null = null;
  toast = '';
  private searchTimeout: any;

  constructor(private deviceService: DeviceService) {}
  
  ngOnInit() { this.loadAll(); }
  
  loadAll() { 
    this.deviceService.getAll(this.search).subscribe(data => this.devices = data); 
  }
  
  loadOnline() { 
    this.deviceService.getOnline().subscribe(data => this.devices = data); 
  }

  onSearch(val: string){
    clearTimeout(this.searchTimeout);
    this.searchTimeout = setTimeout(() => {
      this.search = val;
      this.loadAll();
    }, 300);
  }

  addDevice() {
    if(!this.newDevice.name) return;
    this.deviceService.add(this.newDevice).subscribe(() => {
      this.newDevice = { name: '', status: 'Online' }; 
      this.showToast('Device added!');
      this.loadAll();
    });
  }

  deleteDevice(id: number) {
    if(confirm('Delete device?')) 
      this.deviceService.delete(id).subscribe(() => {
        this.showToast('Deleted');
        this.loadAll();
      });
  }

  startEdit(d: Device){ this.editing = {...d}; }

  saveEdit(){
    if(!this.editing) return;
    this.deviceService.update(this.editing.id, { name: this.editing.name, status: this.editing.status }).subscribe(() => {
      this.editing = null;
      this.showToast('Updated!');
      this.loadAll();
    });
  }

  showToast(msg: string){
    this.toast = msg;
    setTimeout(() => this.toast = '', 2000);
  }
}