import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { UserData } from 'src/app/models/user-data-model';
import { UserService } from 'src/app/services/user.service';
import { CreateUserComponent } from '../create-user/create-user.component';
import { UpdateUserComponent } from '../update-user/update-user.component';

@Component({
  selector: 'app-show-user',
  templateUrl: './show-user.component.html',
  styleUrls: ['./show-user.component.css']
})
export class ShowUserComponent implements OnInit {

  constructor(private service: UserService,private dialog : MatDialog,
    private snackBar : MatSnackBar) { 
      this.service.listen().subscribe((res:any) =>
        {
            this.refreshUserList();
        });
    }

  listUserData : MatTableDataSource<any> = new MatTableDataSource();
  displayedColumns: string[] = ['Options','UserDataId','Image','Name','Age','Gender','Email', 'MobileNumber'];

  @ViewChild(MatSort)
  sort!: MatSort;

  ngOnInit(): void {
    this.refreshUserList();
  }

  onDelete(id: number):void{
    if(confirm("Are you sure you want to delete user?")){
      this.service.deleteUser(id).subscribe({
        next: res=>
        {
            this.refreshUserList();
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
  }

  onEdit(row: any):void{
    console.log(row.userDataId);  
    this.service.formData = new UserData();
    this.service.formData.UserDataId = row.userDataId;
    this.service.formData.Name = row.name;
    this.service.formData.Age = row.age;
    this.service.formData.Email = row.email;
    this.service.formData.Address = row.address;
    this.service.formData.Gender = row.gender;
    this.service.formData.MobileNumber = row.mobileNumber;
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus= true;
    dialogConfig.width="70%";
    this.dialog.open(UpdateUserComponent,dialogConfig);
  }

  refreshUserList(): void{
    this.service.getUserList().subscribe(data=> {
      this.listUserData = new MatTableDataSource(data);
      this.listUserData.sort = this.sort;
    });
  }

  onAdd() : void{
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus= true;
    dialogConfig.width="70%";
    this.dialog.open(CreateUserComponent,dialogConfig);
  }  
}
