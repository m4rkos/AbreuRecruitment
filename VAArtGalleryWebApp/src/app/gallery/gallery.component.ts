import { ChangeDetectorRef, Component, inject, OnInit, signal, ViewChild } from '@angular/core';
import { Gallery, GalleryRequest } from './models';
import { GalleryService } from './gallery.service';
import { Router } from '@angular/router';
import {
  MatDialog,
} from '@angular/material/dialog';

import { GalleryDialog } from './gallery-dialog';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrl: './gallery.component.css'
})

export class GalleryComponent implements OnInit {
  galleries = new MatTableDataSource<Gallery>([]);
  displayedColumns: string[] = ['name', 'city', 'manager', 'nbrWorks', 'actions'];

  readonly dialog = inject(MatDialog);
  readonly name = signal('');
  readonly manager = signal('');
  readonly city = signal('');

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private readonly galleryService: GalleryService, 
    private readonly router: Router,
    private readonly cdr: ChangeDetectorRef
  ) {
    // Customiza o filtro para levar em conta a propriedade 'city'
    this.galleries.filterPredicate = (data: Gallery, filter: string) => {
      return data.city.toLowerCase().includes(filter.toLowerCase());
    };
  }

  ngOnInit(): void {
    console.log('cenas');
    this.galleryService.getGalleries()
      .subscribe(galleries => {
        this.galleries.data = galleries; 
        console.log(this.galleries);
      });
  }

  ngAfterViewInit() {
    this.galleries.paginator = this.paginator;
    this.galleries.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.galleries.filter = filterValue.trim().toLowerCase();
  }

  editGalleryClick(galleryId: string) {
    this.router.navigate(['/art-galleries-edit', galleryId]);
  }

  removeGalleryClick(id: string) {
    try {
      this.galleryService.removeGallery(id).subscribe();

      this.galleries.data = this.galleries.data.filter(a=>a.id !== id);;
      this.galleries.data = [...this.galleries.data];
        
      // Forçar a detecção de mudanças
      this.cdr.detectChanges();
    } catch (error) {
      this.openCreateGalleryModal();
      alert(`Erro: ${error}`);
    }
  }

  openArtWorksList(galleryId: string): void {
    this.router.navigate(['/artworks', galleryId]);
  }

  openCreateGalleryModal(): void {
    const dialogRef = this.dialog.open(GalleryDialog, {
      data: {name: this.name(), manager: this.manager(), city: this.city()},
    });

    dialogRef.afterClosed().subscribe((result: GalleryRequest) => {
      console.log('The dialog was closed');
      if (result !== undefined) {
        this.name.set(result.name);
        this.manager.set(result.manager);
        this.city.set(result.city);

        this.createGallery();
      }
    });
  }

  createGallery () {
    try {
      const payload: GalleryRequest = {
        name: this.name(),
        manager: this.manager(),
        city: this.city()
      };
      this.galleryService.createGallery(payload)
        .subscribe(gallery => {
          const result: Gallery = {
            id: gallery.id,
            name: gallery.name,
            manager: gallery.manager,
            city: gallery.city,
            nbrOfArtWorksOnDisplay: 0
          } 
          this.galleries.data = [...this.galleries.data, result];
          
          // Forçar a detecção de mudanças
          this.cdr.detectChanges();
        });
      this.cleanFields();
    } catch (error) {
      this.openCreateGalleryModal();
      alert(`Erro: ${error}`);
    }
  }

  cleanFields() {
    this.name.set('');
    this.manager.set('');
    this.city.set('');
  }
}
