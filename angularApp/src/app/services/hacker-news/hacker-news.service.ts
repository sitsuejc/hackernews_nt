import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Story } from 'src/app/models/hacker-news/story.model';

@Injectable({
  providedIn: 'root'
})
export class HackerNewsService {

  constructor(
    private readonly httpClient: HttpClient
  ) { }

  private get baseAPIUrl(): string {
    return `${window.location.origin}/coreapi/api/hackernews`;
  }

  private get bestStoriesUrl(): string {
    return `${this.baseAPIUrl}/beststories`;
  }

  /**
   * Return an observable that contains the Best Stories from Hacker News.
   */
  public getBestStories(): Observable<Story[]> {
    // TODO: Handle errors
    return this.httpClient.get(this.bestStoriesUrl) as Observable<Story[]>;
  }

}