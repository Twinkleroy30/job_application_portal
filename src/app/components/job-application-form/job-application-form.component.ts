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
  selectedFile: File | null = null;
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
      this.selectedFile = file;
      this.selectedFileName = file.name;
    }
  }

  applyForJob(): void {
    if (!this.jobId || !this.applicantName || !this.applicantEmail || !this.selectedFile) {
      this.snackBar.open('Please fill all required fields and select a resume.', 'Close', { duration: 3000 });
      return;
    }

    this.isSubmitting = true;

    const formData = new FormData();
    formData.append('applicantName', this.applicantName);
    formData.append('applicantEmail', this.applicantEmail);
    formData.append('resume', this.selectedFile, this.selectedFile.name);

    this.jobService.applyForJob(this.jobId, formData).subscribe({
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
