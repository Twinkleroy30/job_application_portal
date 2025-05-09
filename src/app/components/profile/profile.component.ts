import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JobService } from '../../services/job.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user: any;
  appliedJobs: any[] = [];

  constructor(private http: HttpClient, private jobService: JobService) {}

  ngOnInit(): void {
    const stored = localStorage.getItem('user');
    const id = stored ? JSON.parse(stored).id : null;
    console.log('User ID from localStorage:', id);
    if (id) {
      this.http.get(`http://localhost:5297/api/users/${id}`).subscribe(
        (data) => {
          console.log('User data:', data);
          this.user = data;
          if (this.user && this.user.email) {
            this.loadAppliedJobs(this.user.email);
          }
        },
        (err) => console.error('Failed to load profile:', err)
      );

    }
  }

  loadAppliedJobs(email: string): void {
    this.jobService.getAppliedJobs(email).subscribe({
      next: (jobs) => {
        this.appliedJobs = jobs;
      },
      error: (err) => {
        console.error('Failed to load applied jobs:', err);
      }
    });
  }
}
