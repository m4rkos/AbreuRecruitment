import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Gallery, GalleryAndArtWorks } from './models';

@Injectable({
  providedIn: 'root'
})
export class GalleryService {
  private baseUrl = 'https://localhost:7042/api/art-galleries'
  constructor(private http: HttpClient) { }

  getGalleries(): Observable<Gallery[]> {
    return this.http.get<Gallery[]>(`${this.baseUrl}`);
  }

  getGalleryById(id: string): Observable<GalleryAndArtWorks[]> {
    return this.http.get<GalleryAndArtWorks[]>(`${this.baseUrl}/get-data-by-id/${id}`);
  }
}
