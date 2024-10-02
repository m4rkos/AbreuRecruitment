import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GalleryComponent } from './gallery/gallery.component';
import { ArtWorksComponent } from './art-works/art-works.component';
import { GalleryEditComponent } from './gallery-edit/gallery-edit.component';

const routes: Routes = [
  { path: '', component: GalleryComponent },
  { path: 'art-galleries', component: GalleryComponent },
  { path: 'artworks/:id', component: ArtWorksComponent },
  { path: 'art-galleries-edit/:id', component: GalleryEditComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
