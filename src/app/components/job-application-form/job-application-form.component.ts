import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JobService } from '../../services/job.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Job } from '../../models/job';

@Component({
  selector: 'app-job-application-form',
  templateUrl: './job-application-form.component.html',
  styleUrls: ['./job-application-form.component.scss']
})
export class JobApplicationFormComponent implements OnInit {
  jobId: number | null = null;
  applicantName = '';
  applicantEmail = '';
  jobBrief: Job | null = null;
  selectedFileName: string | null = null;
  isSubmitting = false;

  constructor(
    private route: ActivatedRoute,
    private jobService: JobService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.jobId = +this.route.snapshot.paramMap.get('id')!;
    if (this.jobId) {
      this.jobService.getJobById(this.jobId).subscribe((job) => {
        this.jobBrief = job;
      });
    }
  }

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFileName = file.name;
    }
  }

  applyForJob(): void {
    if (!this.jobId || !this.applicantName || !this.applicantEmail || !this.selectedFileName) {
      this.snackBar.open('Please fill all required fields and select a resume.', 'Close', { duration: 3000 });
      return;
    }

    this.isSubmitting = true;

    const application = {
      JobId: this.jobId,
      ApplicantName: this.applicantName,
      ApplicantEmail: this.applicantEmail,
      ResumeUrl: this.selectedFileName
    };

    this.jobService.applyForJob(this.jobId, application).subscribe({
      next: (res: any) => {
        const message = res.message || '✅ Application submitted!';
        this.snackBar.open(message, 'Close', { duration: 3000 });
        this.router.navigate(['/jobs']);
      },
      error: (err) => {
        const errorMessage = err.error?.error || '❌ Failed to submit application.';
        this.snackBar.open(errorMessage, 'Close', { duration: 3000 });
      },
      complete: () => {
        this.isSubmitting = false;
      }
    });
  }
}
