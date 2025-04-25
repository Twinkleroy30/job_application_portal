import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user: any;
  appliedJobs: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const stored = localStorage.getItem('user');
    const id = stored ? JSON.parse(stored).id : null;
    console.log('User ID from localStorage:', id);
    if (id) {
      this.http.get(`http://localhost:5297/api/users/${id}`).subscribe(
        (data) => {
          console.log('User data:', data);
          this.user = data;
        },
        (err) => console.error('Failed to load profile:', err)
      );

    }
  }
}
