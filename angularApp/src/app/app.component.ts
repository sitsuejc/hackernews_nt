import { Component, OnInit, OnDestroy } from '@angular/core';
import { HackerNewsService } from './services/hacker-news/hacker-news.service';
import { Story } from './models/hacker-news/story.model';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  public appTitle = 'Nextech - Hacker News';
  public bestStories: Story[] = [];
  public searchField: FormControl = new FormControl();
  public loading = false;

  private searchStoriesSubscription: Subscription | undefined;

  constructor(
    private readonly hackerNewsService: HackerNewsService
  ) { }

  ngOnInit(): void {
    // Load all the best stories initially
    this.searchStoriesSubscription = this.showBestStories();

    // Set up the listener for the search input field
    this.initSearchFieldControl();
  }

  ngOnDestroy(): void {
    // Clean up subscription
    this.unsubscribeFromSearchSubscription();
  }

  private initSearchFieldControl(): void {
    // Fire the search, after a 500ms debounce, when the search input value changes
    this.searchField.valueChanges.pipe(
      debounceTime(500)
    ).subscribe(value => this.searchBestStories(value));
  }

  private searchBestStories(searchText: string): void {
    // Clean up previous subscription
    this.unsubscribeFromSearchSubscription();
    this.searchStoriesSubscription = this.showBestStories(searchText);
  }

  private unsubscribeFromSearchSubscription(): void {
    if (this.searchStoriesSubscription) {
      this.searchStoriesSubscription.unsubscribe();
    }
  }

  private showBestStories(searchText: string = ''): Subscription {
    this.loading = true;
    return this.hackerNewsService.searchBestStories(searchText).subscribe(
      this.onStoriesLoaded.bind(this),
      console.error
    );
  }

  private onStoriesLoaded(stories: Story[]): void {
    this.bestStories = stories;
    this.loading = false;
  }

}
