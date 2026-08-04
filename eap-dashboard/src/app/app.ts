import { DeviceListComponent } from './components/device-list/device-list';

import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [DeviceListComponent],
  template: `<app-device-list></app-device-list>`
})
export class App {}