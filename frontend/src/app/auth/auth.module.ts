import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginPageComponent } from './pages/login/login.component';
import { RegisterPageComponent } from './pages/register/register.component';
import { AuthService } from './services/auth.service';

@NgModule({
  declarations: [RegisterPageComponent, LoginPageComponent],
  imports: [AuthRoutingModule, SharedModule, HttpClientModule],
  providers: [AuthService],
})
export class AuthModule {}
