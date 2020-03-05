import { Component, OnInit } from '@angular/core';
import { HackerNewsService } from './services/hacker-news/hacker-news.service';
import { Story } from './models/hacker-news/story.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public appTitle = 'Nextech - Hacker News';

  public bestStories: Story[] = [];

  constructor(
    private readonly hackerNewsService: HackerNewsService
  ) { }

  ngOnInit(): void {
    this.hackerNewsService.getBestStories().subscribe(
      stories => this.bestStories = stories,
      console.error
    );
  }
}
