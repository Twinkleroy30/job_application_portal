import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const stored = localStorage.getItem('user');
    const id = stored ? JSON.parse(stored).id : null;
    if (id) {
      this.http.get(`https://job-portal-yash.onrender.com/api/users/${id}`).subscribe({
        next: (data) => this.user = data,
        error: (err) => console.error('Failed to load profile:', err)
      });
    }
  }
}
