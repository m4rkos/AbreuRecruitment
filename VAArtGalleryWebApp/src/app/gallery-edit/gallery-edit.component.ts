import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-gallery-edit',
  templateUrl: './gallery-edit.component.html',
  styleUrl: './gallery-edit.component.css'
})
export class GalleryEditComponent implements OnInit {
  galleries: any[] = [];

  constructor() {}

  ngOnInit(): void {
    // this.artGalleryService.getGalleryData().subscribe((data: any[]) => {
    //   this.galleries = data;
    // });
  }

  deleteArtItem(galleryId: string, artItemId: string) {
    //this.artGalleryService.deleteArtItem(galleryId, artItemId);
  }
}
