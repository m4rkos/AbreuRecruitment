import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GalleryService } from '../gallery/gallery.service';
import { GalleryAndArtWorks } from '../gallery/models';
import { count } from 'rxjs';

@Component({
  selector: 'app-art-works',
  templateUrl: './art-works.component.html',
  styleUrls: ['./art-works.component.css']
})
export class ArtWorksComponent implements OnInit {
  gallery!: GalleryAndArtWorks;
  randomImg: string[] = []; 

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
        this.gallery = data[0];
        this.loadImage(this.gallery.artWorksOnDisplay?.length ?? 0);
      },
      error: (error) => {
        console.error('Erro ao carregar galeria', error);
      }
    });
  }

  loadImage(amount: number): void {
    let imgLinks = [
      'https://picsum.photos/200', 
      'https://random.imagecdn.app/500/150'
    ];

    for (let i = 0; i < amount; i++) {
      this.galleryService.getRandomImage(imgLinks[0]).subscribe(img => {
        if (!this.randomImg.includes(img.url as string)) {
          this.randomImg.push(img.url as string);
        } else {
          this.galleryService.getRandomImage(imgLinks[1]).subscribe(img2 => 
            this.randomImg.push(img2.url as string)
          )
        }
      });
    }
  }

}
