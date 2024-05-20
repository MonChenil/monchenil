import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminTimeSlotsComponent } from './pages/admin-page/admin-timeslots.component';

const routes: Routes = [
  {
    path: 'admin',
    component: AdminTimeSlotsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TimeSlotsRoutingModule {}
