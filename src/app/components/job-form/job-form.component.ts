import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JobService } from '../../services/job.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Job } from '../../models/job';

@Component({
  selector: 'app-job-form',
  templateUrl: './job-form.component.html',
  styleUrls: ['./job-form.component.scss']
})
export class JobFormComponent implements OnInit {
  job: Job = {
    id: 0,
    job_title: '',
    company_name: '',
    location: '',
    job_type: 'Full-time',
    salary_range: '',
    description: '',
    application_deadline: '',
    posted_date: ''
  };

  isEditMode = false;
  jobId: number | null = null;

  constructor(
    private jobService: JobService,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar 
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.jobId = +idParam;
      if (this.jobId) {
        this.isEditMode = true;
        this.jobService.getJobById(this.jobId).subscribe((job: Job) => {
          if (job.application_deadline && job.application_deadline.includes('T')) {
            job.application_deadline = job.application_deadline.split('T')[0];
          }
          this.job = job;
        });
      }
    }
  }

  saveJob(): void {
    const deadline = this.job.application_deadline as any;
    if (deadline instanceof Date) {
      this.job.application_deadline = deadline.toISOString().split('T')[0];
    } else if (typeof deadline === 'string' && deadline.includes('T')) {
      this.job.application_deadline = deadline.split('T')[0];
    }

    if (!this.job.salary_range) {
      this.job.salary_range = null;
    }

    if (this.isEditMode && this.jobId) {
      this.jobService.updateJob(this.jobId, this.job).subscribe(() => {
        this.snackBar.open('✅ Job updated successfully!', 'Close', { duration: 3000 });
        this.router.navigate(['/jobs']);
      });
    } else {
      this.jobService.createJob(this.job).subscribe(() => {
        this.snackBar.open('✅ Job created successfully!', 'Close', { duration: 3000 });
        this.router.navigate(['/jobs']);
      });
    }
  }
}
