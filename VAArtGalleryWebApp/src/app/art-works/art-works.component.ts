import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GalleryService } from '../gallery/gallery.service';
import { GalleryAndArtWorks } from '../gallery/models';

@Component({
  selector: 'app-art-works',
  templateUrl: './art-works.component.html',
  styleUrls: ['./art-works.component.css']
})
export class ArtWorksComponent implements OnInit {
  gallery!: GalleryAndArtWorks;

  constructor(
    private route: ActivatedRoute,
    private galleryService: GalleryService
  ) {}

  ngOnInit(): void {
    const galleryId = this.route.snapshot.paramMap.get('id');
    if (galleryId) {
      this.loadGallery(galleryId);
    }
  }

  loadGallery(id: string): void {
    this.galleryService.getGalleryById(id).subscribe({
      next: (data) => {
        console.log('Dados recebidos:', data); // Verifique os dados recebidos no console
        this.gallery = data[0];
      },
      error: (error) => {
        console.error('Erro ao carregar galeria', error);
      }
    });
  }
}
