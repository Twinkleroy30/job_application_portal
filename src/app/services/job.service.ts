import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Job } from '../models/job';
import { Favorite } from '../models/favorite'; // Ensure this path is correct

@Injectable({
  providedIn: 'root'
})
export class JobService {
  private apiUrl = 'http://localhost:5297/api/jobs';
  private favoritesApiUrl = 'http://localhost:5297/api/favorites';

  constructor(private http: HttpClient) {}

  getAllJobs(): Observable<Job[]> {
    return this.http.get<Job[]>(this.apiUrl);
  }

  getFavoriteJobs(userId: number): Observable<Job[]> {
    return this.http.get<Job[]>(`${this.favoritesApiUrl}/${userId}`);
  }

  removeFavoriteJob(userId: number, jobId: number): Observable<void> {
    return this.http.delete<void>(`${this.favoritesApiUrl}/${userId}/${jobId}`);
  }

  getJobById(jobId: number): Observable<Job> {
    return this.http.get<Job>(`${this.apiUrl}/${jobId}`);
  }

  updateJob(jobId: number, job: Job): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${jobId}`, job);
  }

  createJob(job: Job): Observable<Job> {
    return this.http.post<Job>(this.apiUrl, job);
  }

  applyForJob(jobId: number, application: any): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${jobId}/apply`, application);
  }

  updateFavoriteJobs(userId: number, favoriteIds: number[]): Observable<void> {
    return this.http.put<void>(`${this.favoritesApiUrl}/${userId}`, favoriteIds);
  }

  addToFavorites(userId: number, jobId: number): Observable<void> {
    const favorite: Favorite = { id: 0, userId: userId, jobId: jobId }; // Create a new Favorite object
    return this.http.post<void>(this.favoritesApiUrl, favorite);
  }

  getAppliedJobs(applicantEmail: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/applied/${applicantEmail}`);
  }
}
