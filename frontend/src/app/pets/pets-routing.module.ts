import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddPetPageComponent } from './pages/add-pet/add-pet.component';

const routes: Routes = [
  {
    path: 'add-pet',
    component: AddPetPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PetsRoutingModule {}
