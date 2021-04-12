import React from 'react'
import 'bootstrap'

class CreateTestForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            testName: "",
            startDate: null,
            startTime: null,
            endDate: null,
            endTime: null
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
        alert("Saved")
        event.preventDefault();
    }

    render(){
        return (
        <div class = "container-fluid w-50 mx-auto pt-4"> 
            <h3 className="text-center"> Create a new test</h3>
            <form onSubmit={this.submitButton}>
                <div class="form-group">
                    <label for="testName">Test Name:</label>
                    <input id="testName" value={this.state.testName} onChange={this.testNameChange} class="form-control" placeholder="Enter test name"></input>
                </div>
                <div class="form-group">
                    <label for="startDate">Start Date:</label>
                    <input id="startDate" value={this.state.startDate} onChange={this.startDateChange} class="form-control" type="date"></input>
                </div>
                <div class="form-group">
                    <label for="startTime">Start Time</label>
                    <input id="startTime" value={this.state.startTime} onChange={this.startTimeChange} class="form-control" type="time" ></input>
                </div>
                <div class="form-group">
                    <label for="endDate">End Date:</label>
                    <input id="endDate" value={this.state.endDate} onChange={this.endDateChange} class="form-control" type="date"></input>
                </div>
                <div class="form-group">
                    <label for="endTime">End Time</label>
                    <input id="endTime" value={this.state.endTime} onChange={this.endTimeChange} class="form-control" type="time" ></input>
                </div>
                <div class="text-center mb-2">
                    <button class="btn btn-success" type="submit" onClick={this.submitButton}>Submit </button>
                </div>
            </form>
        </div>
    );
    }
}


export default CreateTestForm;
