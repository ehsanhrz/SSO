import { Component } from '@angular/core';
import { AuthService } from '../../auth.service';

@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html',
  styleUrl: './callback.component.css'
})
export class CallbackComponent {
  constructor(public authService: AuthService) {
    
  }
  //ngOnInit(): void {
  //  this.authService.handleAuthCallback().then(() => {
  //    if (this.authService.isLoggedIn) {
  //      // Redirect to home or any other page
  //      this.router.navigate(['/']);
  //    } else {
  //      // Handle login error
  //      console.error('Login failed or no user session present');
  //    }
  //  }).catch(err => {
  //    console.error('Error during login', err);
  //  });
  //}
}
