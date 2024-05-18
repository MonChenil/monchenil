import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginPageComponent } from './pages/login/login.component';
import { RegisterPageComponent } from './pages/register/register.component';

@NgModule({
  declarations: [RegisterPageComponent, LoginPageComponent],
  imports: [AuthRoutingModule, ReactiveFormsModule],
  providers: [],
})
export class AuthModule {}
