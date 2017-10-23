
import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'student',
    templateUrl: '/student.component.html'
})

export class StudentComponent {
    public students :  StudentDetail[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl : string){
        http.get('http://localhost:5000/api/student').subscribe(result => {
            this.students = result.json() as StudentDetail[];
        }, error => console.error(error));
    }
}

interface StudentDetail {
    id: number;
    lastName: string;
    firstMidName: string;
    enrollmentDate: string;
}