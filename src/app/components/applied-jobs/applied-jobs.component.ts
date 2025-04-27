import { Component, OnInit } from '@angular/core';
import { JobService } from '../../services/job.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-applied-jobs',
  templateUrl: './applied-jobs.component.html',
  styleUrls: ['./applied-jobs.component.scss']
})
export class AppliedJobsComponent implements OnInit {
  appliedJobs: any[] = [];
  user: any;

  constructor(private http: HttpClient, private jobService: JobService, private router: Router) {}

  ngOnInit(): void {
    const stored = localStorage.getItem('user');
    const id = stored ? JSON.parse(stored).id : null;
    if (id) {
      this.http.get(`http://localhost:5297/api/users/${id}`).subscribe(
        (data) => {
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

  viewDetails(jobId: number): void {
    this.router.navigate(['/jobs', jobId]);
  }
}
