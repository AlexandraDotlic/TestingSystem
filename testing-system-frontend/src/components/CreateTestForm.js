import React from 'react'
import axios from 'axios'
import 'bootstrap'

class CreateTestForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            testName: "",
            startDate: "",
            startTime: ""
        };

        this.testNameChange = this.testNameChange.bind(this);
        this.startDateChange = this.startDateChange.bind(this);
        this.startTimeChange = this.startTimeChange.bind(this);
        this.submitButton = this.submitButton.bind(this)

    }

    testNameChange(event) {
        this.setState({testName: event.target.value});
    }

    startDateChange(event) {
        this.setState({startDate: event.target.value});
    }

    startTimeChange(event) {
        this.setState({startTime: event.target.value});
    }

    submitButton = () => {
        // Parse date from startDate and add hours and minutes converted to miliseconds
        let startDateParsed = Date.parse(this.state.startDate);
        startDateParsed += parseInt(this.state.startTime.substring(0, 2)) * 3600000;
        startDateParsed += parseInt(this.state.startTime.substring(3)) * 60000;
        // Recombine it back to Date object
        let startDateCombined = new Date(startDateParsed);
        // Prepare token 
        let token = sessionStorage.getItem('userToken');

        axios({
            method: 'post',
            url: 'https://localhost:44329/Test/CreateTest',
            headers: {
                'Authorization': token
            },
            data: {
              title: this.state.testName,
              startDate: startDateCombined,
            }
        }).then(() => {
            window.alert("Test successfuly created.");
            this.props.cancelCallback();
        })
        .catch(() => {
            window.alert("Failed to create test.");
            this.props.cancelCallback();
        });
    }

    render(){
        return (
        <div className = "container-fluid w-50 mx-auto pt-4"> 
            <h3 className="text-center"> Create a new test</h3>
                <div className="form-group">
                    <label htmlFor="testName">Test Name:</label>
                    <input id="testName" value={this.state.testName} onChange={this.testNameChange} className="form-control" placeholder="Enter test name"></input>
                </div>
                <div className="form-group">
                    <label htmlFor="startDate">Start Date:</label>
                    <input id="startDate" value={this.state.startDate} onChange={this.startDateChange} className="form-control" type="date"></input>
                </div>
                <div className="form-group">
                    <label htmlFor="startTime">Start Time</label>
                    <input id="startTime" value={this.state.startTime} onChange={this.startTimeChange} className="form-control" type="time" ></input>
                </div>
                <div className="form-group text-center mb-2">
                    <button className="btn btn-success" onClick={this.submitButton}>Submit </button>
                </div>
                <div className="form-group text-center mb-2">
                    <button className="btn btn-warning" onClick={this.props.cancelCallback}>Discard </button>
                </div>
        </div>
    );
    }
}


export default CreateTestForm;
