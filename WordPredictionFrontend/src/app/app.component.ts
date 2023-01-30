import { Component, OnInit } from '@angular/core';
import { Predictions } from './models/predictions.model';
import { DictionaryService } from './services/dictionary.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private dictionaryService: DictionaryService) {

  }

  userInput: string = "";

  webPredictions?: Predictions;
  dictionaryPredictions?: Predictions;

  ngOnInit(): void {

  }

  getSuggestions() {
    // Splitting up userinput to get last word
    let lastWord = this.userInput.split(" ").pop();
    if (lastWord == "")
      return;

    this.dictionaryService.getWebservicePredictions(lastWord)
      .subscribe({
        next: (response) => {
          this.webPredictions = response;
        }
      });

    this.dictionaryService.getDictionaryPredictions(lastWord)
      .subscribe({
        next: (response) => {
          this.dictionaryPredictions = response;
        }
      });


  }


}
