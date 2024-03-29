import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent implements OnInit {
  baseUrl ='https://localhost:7086/api';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
 get404Error(){
  this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
    next: response => console.log(response),
    error: error => console.log(error)

  })
 }

//  get404Error(){
//   this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
//     next: response => console.log(response),
//     error: error => console.log(error)

//   })
//  }

//  get404Error(){
//   this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
//     next: response => console.log(response),
//     error: error => console.log(error)

//   })
//  }

//  get404Error(){
//   this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
//     next: response => console.log(response),
//     error: error => console.log(error)

//   })
//  }

//  get404Error(){
//   this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
//     next: response => console.log(response),
//     error: error => console.log(error)

//   })
//  }
 
}
