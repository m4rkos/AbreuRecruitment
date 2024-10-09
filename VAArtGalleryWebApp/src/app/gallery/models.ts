export interface Gallery {
  id: string;
  name: string;
  city: string;
  manager: string;
  nbrOfArtWorksOnDisplay: number;
}

// Gallery and Art Work list
export interface GalleryAndArtWorks {
  id: string
  name: string
  city: string
  manager: string
  artWorksOnDisplay?: ArtWorksOnDisplay[]
}

export interface ArtWorksOnDisplay {
  id: string
  name: string
  author: string
  creationYear: number
  askPrice: number
}

// Create Gallery
export interface GalleryRequest {
  name: string;
  city: string;
  manager: string;
}

export interface GalleryResponse {
  id: string;
  name: string;
  city: string;
  manager: string;
}