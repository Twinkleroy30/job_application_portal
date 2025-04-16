export interface Job {
  id: number;
  job_title: string;
  company_name: string;
  location: string;
  description: string;
  posted_date: string;
  job_type?: string;
  salary_range?: string | null;
  application_deadline?: string;
}
