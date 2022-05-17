import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserData } from 'src/app/models/user-data-model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {

  constructor(private dialogRef : MatDialogRef<CreateUserComponent>,public service:UserService,
    private snackBar : MatSnackBar) {
      this.service.formData = {
        UserDataId: 0,
        Name: '',
        Age: 1,
        Gender: '',
        Email: '',
        Address: '',
        FileName: '',
        MobileNumber: '',
        ProfilePictureBase64Data: ''
      };
  }
  
  ngOnInit(): void {
    this.resetForm();
  }

  resetForm(form?:NgForm){
    if(form!=null){
      form.resetForm();
      this.service.formData = new UserData();
    }
  }

  onSubmit(form?:NgForm) : void{
    this.service.addUser(this.service.formData).subscribe({
      next: (res) => {
        this.resetForm(form);
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

  onFileChange(event:any) : void{
    const file = event.target.files[0];
    const reader = new FileReader();
    
    reader.readAsDataURL(file);
    
    if(this.service.formData != undefined){
      this.service.formData.FileName = file.name;
    }

    reader.onload = () => {
      if(this.service.formData != undefined){
        this.service.formData.ProfilePictureBase64Data = reader.result?.toString();
      }
    };
  }

  onClose():void{
    this.dialogRef.close();
    this.service.filter("Register click");
  }

}
