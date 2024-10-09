import { ChangeDetectionStrategy, Component, inject, model } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef, MatDialogTitle } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { GalleryRequest } from "./models";

@Component({
    selector: 'gallery-dialog',
    templateUrl: 'gallery-dialog.html',
    standalone: true,
    imports: [
        MatDialogTitle, 
        MatDialogContent, 
        MatDialogActions, 
        MatDialogClose, 
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        FormsModule],
    changeDetection: ChangeDetectionStrategy.OnPush,
  })
  export class GalleryDialog {
    readonly dialogRef = inject(MatDialogRef<GalleryDialog>);
    readonly data = inject<GalleryRequest>(MAT_DIALOG_DATA);
  
    readonly name = model(this.data.name);
    readonly manager = model(this.data.manager);
    readonly city = model(this.data.city);
  
    onNoClick(): void {
        this.dialogRef.close();
    }
  }