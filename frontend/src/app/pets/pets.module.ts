import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { PetsAddComponent } from './components/pets-add/pets-add.component';
import { PetsPageComponent } from './pages/pets-page/pets-page.component';
import { PetsService } from './services/pets.service';
import { PetsCardComponent } from './components/pets-card/pets-card.component';
import { PetsListComponent } from './components/pets-list/pets-list.component';

@NgModule({
  declarations: [
    PetsPageComponent,
    PetsAddComponent,
    PetsListComponent,
    PetsCardComponent
  ],
  imports: [SharedModule],
  providers: [PetsService],
})
export class PetsModule {}