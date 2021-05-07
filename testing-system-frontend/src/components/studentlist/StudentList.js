import React from 'react';
import axios from 'axios';
import Student from './Student'

class StudentList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            students : [],
            studentsAddList: []
        }
        this.finishClick = this.finishClick.bind(this);
        this.getChecked = this.getChecked.bind(this);
    }

    finishClick() {
        let result = this.state.studentsAddList
        // Backend should be hitted with each of elements from result
        
    }

    getChecked(object) {
        let newList = this.state.studentsAddList;
        if(!newList.includes(parseInt(object.id))) {
            newList.push(object.id);
        }
    }

    componentDidMount() {
        axios.get("https://localhost:44329/Student/GetAllStudents").then( response => {
            let studentsObjects = response.data.students.map(student => {
                return <Student key={student.id} id={student.id} firstName={student.firstName} lastName={student.lastName} checked={this.getChecked}></Student>
            })

            this.setState({students: studentsObjects});
        });
    }

    render() {

        return (
            <div className="w-50 mx-auto pt-3">
                <h5> Select students from list to add to group: </h5>
                    <hr></hr>
                <ul>
                    {this.state.students}
                </ul>
                    <hr></hr>
                <div className="text-center">
                    <button className="btn btn-success" onClick={this.finishClick}> Finish </button>
                </div>
    
            </div>
        )

    }

}

export default StudentList;