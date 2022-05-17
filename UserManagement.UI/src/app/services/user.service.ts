import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { UserData } from '../models/user-data-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }

  formData!: UserData;
  readonly baseUrl = "https://localhost:44397/"
  readonly apiUrl = this.baseUrl + "api";

  getUserList(): Observable<UserData[]>{
    return this.http.get<UserData[]>(this.apiUrl + "/User");
  }

  addUser(user:UserData){
    return this.http.post<any>(this.apiUrl + "/User", user); 
  }

  deleteUser(userId:number){
    return this.http.delete<any>(this.apiUrl + "/User/" + userId);
  }

  editUser(user:UserData){
    return this.http.put<any>(this.apiUrl + "/User",user);
  }

  private _listners = new Subject<any>();
  listen(): Observable<any>{
    return this._listners.asObservable();
  }

  filter(filterBy:string){
    this._listners.next(filterBy);
  }
}
