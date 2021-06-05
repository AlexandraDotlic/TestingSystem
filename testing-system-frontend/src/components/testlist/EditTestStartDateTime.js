import axios from 'axios';
import React from 'react';
import 'bootstrap'

class EditTestStartDateTime extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            testId: this.props.id,
            testTitle: this.props.title,
            startDate: "",
            startTime: ""
        };

        this.startDateChange = this.startDateChange.bind(this);
        this.startTimeChange = this.startTimeChange.bind(this);
        this.submitButton = this.submitButton.bind(this)

    }

    startDateChange(event) {
        this.setState({startDate: event.target.value});
    }

    startTimeChange(event) {
        this.setState({startTime: event.target.value});
    }

    submitButton = (event) => {
        let startDateParsed = Date.parse(this.state.startDate);

        startDateParsed += parseInt(this.state.startTime.substring(0, 2)) * 3600000;
        startDateParsed += parseInt(this.state.startTime.substring(3)) * 60000;

        let startDateCombined = new Date(startDateParsed);
        
        let token = sessionStorage.getItem('userToken');

        axios({
            method: 'post',
            url: 'https://localhost:44329/Test/ChangeStartDate',
            headers: {
                'Authorization': token
            },
            data: {
            testId: this.state.testId,
            startDate: startDateCombined,
            }
        }).then(() => {
        }).catch(() => {
            window.alert("Failed to change start date. Insert all values");
        });
    }

    render(){
        return (
        <div className = "container-fluid w-50 mx-auto pt-4"> 
            <h3 className="text-center"> Change Start Date and Start Time for test: <em>{this.state.testTitle}</em></h3>
            <form>
                <div className="form-group">
                    <label htmlFor="startDate">Start Date:</label>
                    <input id="startDate" value={this.state.startDate} onChange={this.startDateChange} className="form-control" type="date"></input>
                </div>
                <div className="form-group">
                    <label htmlFor="startTime">Start Time</label>
                    <input id="startTime" value={this.state.startTime} onChange={this.startTimeChange} className="form-control" type="time" ></input>
                </div>
                <div className="text-center mb-2">
                    <button className="btn btn-success" type="submit" onClick={this.submitButton}>Submit </button>
                </div>
            </form>
        </div>
    );
    }
}


export default EditTestStartDateTime;
