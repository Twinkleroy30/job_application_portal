import { Component, OnInit } from '@angular/core';
import { JobService } from '../../services/job.service';
import { Job } from '../../models/job';
import { Router } from '@angular/router';

@Component({
  selector: 'app-job-list',
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.scss']
})
export class JobListComponent implements OnInit {
  jobs: Job[] = [];
  filteredJobs: Job[] = [];
  hovered: number | null = null;

  searchText: string = '';
  selectedJobType: string = '';
  selectedLocation: string = '';

  currentPage: number = 1;
  jobsPerPage: number = 6;
  isDarkMode: boolean = false;

  favoriteJobIds: number[] = [];
  userId: number = 1; // Assuming logged-in user ID is 1 for demo, replace with actual user ID logic

  constructor(private jobService: JobService, private router: Router) {}

  ngOnInit(): void {
    this.loadJobs();
    this.loadFavoriteJobs();
  }

  toggleDarkMode(): void {
    this.isDarkMode = !this.isDarkMode;
  }

  loadJobs(): void {
    this.jobService.getAllJobs().subscribe(
      (data: Job[]) => {
        this.jobs = data;
        this.filteredJobs = data;
      },
      (error) => {
        console.error('Error fetching jobs:', error);
      }
    );
  }

  loadFavoriteJobs(): void {
    this.jobService.getFavoriteJobs(this.userId).subscribe(
      (favJobs: Job[]) => {
        this.favoriteJobIds = favJobs.map(job => job.id);
      },
      (error) => {
        console.error('Error fetching favorite jobs:', error);
      }
    );
  }

  isFavorite(jobId: number): boolean {
    return this.favoriteJobIds.includes(jobId);
  }

  toggleFavorite(jobId: number): void {
    if (this.isFavorite(jobId)) {
      this.jobService.removeFavoriteJob(this.userId, jobId).subscribe({
        next: () => {
          this.favoriteJobIds = this.favoriteJobIds.filter(id => id !== jobId);
        },
        error: (err) => {
          console.error('Error removing favorite:', err);
        }
      });
    } else {
      this.jobService.addToFavorites(this.userId, jobId).subscribe({
        next: () => {
          this.favoriteJobIds.push(jobId);
        },
        error: (err) => {
          console.error('Error adding favorite:', err);
        }
      });
    }
  }

  filterJobs(): void {
    this.filteredJobs = this.jobs.filter(job => {
      const matchesSearch = job.job_title.toLowerCase().includes(this.searchText.toLowerCase()) ||
                            job.company_name.toLowerCase().includes(this.searchText.toLowerCase());
      const matchesType = this.selectedJobType ? job.job_type === this.selectedJobType : true;
      const matchesLocation = this.selectedLocation ? job.location.toLowerCase().includes(this.selectedLocation.toLowerCase()) : true;
      return matchesSearch && matchesType && matchesLocation;
    });
    this.currentPage = 1; // Reset to first page on filter
  }

  get paginatedJobs(): Job[] {
    const start = (this.currentPage - 1) * this.jobsPerPage;
    return this.filteredJobs.slice(0, start + this.jobsPerPage);
  }

  get totalPages(): number {
    return Math.ceil(this.filteredJobs.length / this.jobsPerPage);
  }

  get hasMorePages(): boolean {
    return this.paginatedJobs.length < this.filteredJobs.length;
  }

  loadMoreJobs(): void {
    if (this.hasMorePages) {
      this.currentPage++;
    }
  }

  changePage(page: number): void {
    this.currentPage = page;
  }

  navigateToCreateJob(): void {
    this.router.navigate(['/jobs/new']);
  }

  navigateToJobDetails(id: number): void {
    this.router.navigate(['/jobs', id]);
  }

  navigateToEditJob(id: number): void {
    this.router.navigate(['/jobs/edit', id]);
  }

  navigateToJobApplicationForm(id: number): void {
    this.router.navigate(['/jobs', id, 'apply']);
  }
}
