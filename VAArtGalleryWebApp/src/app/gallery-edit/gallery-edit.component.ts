import { Component, OnInit } from '@angular/core';
import { GalleryService } from '../gallery/gallery.service';
import { GalleryAndArtWorks } from '../gallery/models';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-gallery-edit',
  templateUrl: './gallery-edit.component.html',
  styleUrl: './gallery-edit.component.css'
})
export class GalleryEditComponent implements OnInit {
  galleryAndArtWorks!: GalleryAndArtWorks;
  galleryId: string | null = null;

  constructor(
    private galleryService: GalleryService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const galleryId = this.route.snapshot.paramMap.get('id');
    this.galleryId = galleryId;
    if (galleryId) {
      this.loadGallery(galleryId);
    }
  }

  backHome() {
    this.router.navigate(['/']);
  }

  loadGallery(id: string): void {
    this.galleryService.getGalleryById(id).subscribe({
      next: (data) => {
        console.log('Dados recebidos:', data);
        this.galleryAndArtWorks = data[0];
      },
      error: (error) => {
        console.error('Erro ao carregar galeria', error);
      }
    });
  }

  updateArtItem() {
    if (this.galleryId) {
      //this.galleryService.updateArtItem(this.galleryId, this.artItemId, this.artItem);
      this.router.navigate(['/']);
    }
  }

  deleteArtItem(galleryId: string, artItemId: string) {
    //this.galleryService.deleteArtItem(galleryId, artItemId);
  }
}
