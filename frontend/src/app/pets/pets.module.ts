import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddPetPageComponent } from './pages/add-pet/add-pet.component';
import { PetsRoutingModule } from './pets-routing.module';

@NgModule({
  declarations: [AddPetPageComponent],
  imports: [CommonModule, PetsRoutingModule],
})
export class PetsModule {}
