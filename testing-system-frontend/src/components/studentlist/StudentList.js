import React from 'react';
import axios from 'axios';
import Student from './Student'

class StudentList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            students : []
        }
    }

    componentDidMount() {
        axios.get("https://localhost:44329/Student/GetAllStudents").then( response => {
            this.setState({students: response.data.students})
        });
    }

    render() {
        let students = this.state.students.map(student => {
            return <Student key={student.id} id={student.id} firstName={student.firstName} lastName={student.lastName}></Student>
        })

        return (
            <div className="w-50 mx-auto pt-3">
                    <h5> Select students from list to add to group: </h5>
                    <hr></hr>
                    <ul>
                        {students}
                    </ul>
                    <hr></hr>
                    <div className="text-center">
                        <button className="btn btn-success"> Finish </button>
                    </div>
    
                </div>
        )

    }

}

export default StudentList;