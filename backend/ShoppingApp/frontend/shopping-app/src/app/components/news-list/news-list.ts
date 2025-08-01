import { Component } from '@angular/core';
import { NewsService } from '../../services/news-service';
import { News } from '../../models/News';
import { DatePipe } from '@angular/common';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-news-list',
  imports: [DatePipe],
  templateUrl: './news-list.html',
  styleUrl: './news-list.css'
})
export class NewsList {
  newsList: News[] = [];
  ImageUrl = `${environment.apiBaseUrl}/Image/`;
  constructor(private newsService: NewsService) {}

  ngOnInit(): void {
    this.newsService.getAllNews().subscribe({
      next: (data) => this.newsList = data,
      error: (err) => console.error('Failed to load news:', err)
    });
  }

  downloadExport(): void {
    this.newsService.exportNews().subscribe({
      next: (blob) => {
        const a = document.createElement('a');
        const url = window.URL.createObjectURL(blob);
        a.href = url;
        a.download = `NewsExport_${new Date().toISOString()}.csv`;
        a.click();
        window.URL.revokeObjectURL(url);
      },
      error: (err) => console.error('Download failed:', err)
    });
  }
}