import React from 'react';
import axios from 'axios';
import Student from './Student'

class StudentList extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="w-50 mx-auto pt-3">
                    <h5> Select students from list to add to group: </h5>
                    <hr></hr>
                    <ul>
                        <Student id={1} firstName={"Marko"} lastName={"Markovic"}></Student>
                        <Student id={2} firstName={"Petar"} lastName={"Petrovic"}></Student>
                        <Student id={3} firstName={"Nikola"} lastName={"Nikolic"}></Student>
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