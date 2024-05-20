import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { TimeSlotsAddComponent } from './components/timeslots-add/timeslots-add.component';
import { AdminTimeSlotsComponent } from './pages/admin-page/admin-timeslots.component';
import { TimeSlotsService } from './services/timeslots.service';
import { TimeslotsCardComponent } from './components/timeslots-card/timeslots-card.component';
import { TimeslotsListComponent } from './components/timeslots-list/timeslots-list.component';
import { TimeSlotsRoutingModule } from './timeslots-routing.module';

@NgModule({
  declarations: [
    AdminTimeSlotsComponent,
    TimeSlotsAddComponent,
    TimeslotsListComponent,
    TimeslotsCardComponent,
  ],
  imports: [TimeSlotsRoutingModule, SharedModule],
  providers: [TimeSlotsService],
})
export class TimeSlotsModule {}
