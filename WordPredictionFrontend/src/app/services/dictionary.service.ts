import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Predictions } from '../models/predictions.model';

@Injectable({
  providedIn: 'root'
})
export class DictionaryService {

  constructor(private http: HttpClient) { }

  // Gets the word suggestions from the online resource
  getWebservicePredictions(text?: string): Observable<Predictions> {
    if (text == null)
      return EMPTY;

    return this.http.get<Predictions>(environment.webBaseUrl, {
      params: new HttpParams()
        .set('text', text)
    })
  }

  // Gets the word suggestions from the local DB
  getDictionaryPredictions(text?: string): Observable<Predictions> {
    if (text == null)
      return EMPTY;

    return this.http.get<Predictions>(environment.dictionaryBaseUrl, {
      params: new HttpParams()
        .set('text', text)
    }) 
  }
}
