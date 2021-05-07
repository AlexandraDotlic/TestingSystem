import React from 'react'
import axios from 'axios'
import 'bootstrap'

class CreateTestForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            testName: "",
            startDate: "",
            startTime: "",
            endDate: "",
            endTime: "",
            isSubmitted: false
        };

        this.testNameChange = this.testNameChange.bind(this);
        this.startDateChange = this.startDateChange.bind(this);
        this.startTimeChange = this.startTimeChange.bind(this);
        this.endDateChange = this.endDateChange.bind(this);
        this.endTimeChange = this.endTimeChange.bind(this);
        this.submitButton = this.submitButton.bind(this)

    }

    //resiti redundantnost koda!

    testNameChange(event) {
        this.setState({testName: event.target.value});
    }

    startDateChange(event) {
        this.setState({startDate: event.target.value});
    }

    startTimeChange(event) {
        this.setState({startTime: event.target.value});
    }

    endDateChange(event) {
        this.setState({endDate: event.target.value});
    }

    endTimeChange(event) {
        this.setState({endTime: event.target.value});
    }

    submitButton = (event) => {
        // Parse date from startDate and add hours and minutes converted to miliseconds
        let startDateParsed = Date.parse(this.state.startDate);
        startDateParsed += parseInt(this.state.startTime.substring(0, 2)) * 3600000;
        startDateParsed += parseInt(this.state.startTime.substring(3)) * 60000;
        // Recombine it back to Date object
        let startDateCombined = new Date(startDateParsed);

        // Parse date from endDate and add hours and minutes converted to miliseconds
        // Also check if end date is setted
        let endDateCombined;
        let endDateParsed = Date.parse(this.state.endDate);
        if(endDateParsed !== NaN) {
            endDateParsed += parseInt(this.state.endTime.substring(0, 2)) * 3600000;
            endDateParsed += parseInt(this.state.endTime.substring(3)) * 60000;
            endDateCombined = new Date(endDateParsed);
        } else {
            endDateCombined = null;
        }

        if(this.state.isSubmitted === false) {
            axios({
                method: 'post',
                url: 'https://localhost:44329/Test/CreateTest',
                data: {
                  title: this.state.testName,
                  startDate: startDateCombined,
                  endDate: endDateCombined
                }
              });
            this.setState({isSubmitted: true});
        }

    }

    render(){
        return (
        <div className = "container-fluid w-50 mx-auto pt-4"> 
            <h3 className="text-center"> Create a new test</h3>
            <form onSubmit={this.submitButton}>
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
                <div className="form-group">
                    <label htmlFor="endDate">End Date:</label>
                    <input id="endDate" value={this.state.endDate} onChange={this.endDateChange} className="form-control" type="date"></input>
                </div>
                <div className="form-group">
                    <label htmlFor="endTime">End Time</label>
                    <input id="endTime" value={this.state.endTime} onChange={this.endTimeChange} className="form-control" type="time" ></input>
                </div>
                <div className="text-center mb-2">
                    <button className="btn btn-success" type="submit" onClick={this.submitButton}>Submit </button>
                </div>
            </form>
        </div>
    );
    }
}


export default CreateTestForm;
