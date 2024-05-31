import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { PetsAddComponent } from './components/pets-add/pets-add.component';
import { PetsPageComponent } from './pages/pets-page/pets-page.component';
import { PetsService } from './services/pets.service';
import { PetsListComponent } from './components/pets-list/pets-list.component';
import { PetsCardComponent } from './components/pets-card/pets-card.component';

@NgModule({
  declarations: [
    PetsPageComponent,
    PetsAddComponent,
    PetsListComponent,
    PetsCardComponent,
  ],
  imports: [SharedModule],
  providers: [PetsService],
})
export class PetsModule {}
