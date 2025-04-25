import { Component, OnInit } from '@angular/core';
import { Job } from '../../models/job';
import { JobService } from '../../services/job.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss']
})
export class FavoritesComponent implements OnInit {
  favoriteJobs: Job[] = [];

  constructor(private jobService: JobService, private router: Router) {}

  ngOnInit(): void {
    this.loadFavoriteJobs();
  }

  loadFavoriteJobs(): void {
    const stored = localStorage.getItem('user');
    const userId = stored ? JSON.parse(stored).id : null;
    if (userId) {
      this.jobService.getFavoriteJobs(userId).subscribe({
        next: (jobs: Job[]) => {
          this.favoriteJobs = jobs;
        },
        error: (err) => {
          console.error('Failed to load favorite jobs', err);
        }
      });
    } else {
      console.error('No user logged in, cannot load favorite jobs');
    }
  }

  viewDetails(jobId: number): void {
    this.router.navigate(['/jobs', jobId]);
  }

  removeFromFavorites(jobId: number): void {
    const stored = localStorage.getItem('user');
    const userId = stored ? JSON.parse(stored).id : null;
    if (userId) {
      this.jobService.removeFavoriteJob(userId, jobId).subscribe({
        next: () => {
          this.favoriteJobs = this.favoriteJobs.filter(job => job.id !== jobId);
          console.log(`Job ${jobId} removed from favorites`);
        },
        error: (err) => {
          console.error('Failed to remove job from favorites', err);
        }
      });
    } else {
      console.error('No user logged in, cannot remove favorite job');
    }
  }
}
