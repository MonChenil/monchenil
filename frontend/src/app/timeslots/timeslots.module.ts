import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { AdminTimeSlotsComponent } from './pages/admin-page/admin-timeslots.component';
import { TimeSlotsService } from './services/timeslots.service';
import { TimeSlotsRoutingModule } from './timeslots-routing.module';

@NgModule({
  declarations: [AdminTimeSlotsComponent],
  imports: [TimeSlotsRoutingModule, SharedModule],
  providers: [TimeSlotsService],
})
export class TimeSlotsModule {}
