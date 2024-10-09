import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Gallery, GalleryAndArtWorks, GalleryRequest, GalleryResponse } from './models';

@Injectable({
  providedIn: 'root'
})
export class GalleryService {
  private readonly baseUrl = 'https://localhost:7042/api/art-galleries'
  constructor(private readonly http: HttpClient) { }

  getGalleries(): Observable<Gallery[]> {
    return this.http.get<Gallery[]>(`${this.baseUrl}`);
  }

  getGalleryById(id: string): Observable<GalleryAndArtWorks[]> {
    return this.http.get<GalleryAndArtWorks[]>(`${this.baseUrl}/${id}`);
  }

  getRandomImage(urlImg: string): Observable<HttpResponse<Blob>> {
    return this.http.get(urlImg, { observe: 'response', responseType: 'blob' });
  }

  createGallery(data: GalleryRequest): Observable<GalleryResponse> {
    return this.http.post<GalleryResponse>(`${this.baseUrl}`, JSON.stringify(data), {
      headers: {'Content-Type':'application/json'}
    });
  }
}
