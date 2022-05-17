import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from 'src/app/services/user.service';
import { CreateUserComponent } from '../create-user/create-user.component';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {

  constructor(private dialogRef : MatDialogRef<CreateUserComponent>,public service:UserService,
    private snackBar : MatSnackBar) {
  }

  ngOnInit(): void {
  }

  onSubmit() : void{
    this.service.editUser(this.service.formData).subscribe({
      next: (res) => {
        this.snackBar.open(res.message,'',{
            duration:3000,
            horizontalPosition:'right',
            verticalPosition:'top'
        });
      },
      error: (err) => {
        if(err.error.message != undefined){
          this.snackBar.open(err.error.message,'',{
            duration:3000,
            horizontalPosition:'right',
            verticalPosition:'top'
          });
        }
        else{
          this.snackBar.open(JSON.stringify(err.error.errors),'',{
            duration:3000,
            horizontalPosition:'right',
            verticalPosition:'top'
          });
        }
        console.log(err)
      }
    });
  }

  onClose():void{
    this.dialogRef.close();
    this.service.filter("Register click");
  }
}
