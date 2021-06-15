import React from 'react'
import axios from 'axios'

class StatisticsDetail extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            testId: this.props.testId,
            groupId: this.props.groupId,
            date: null,
            changed: false
        }
        this.createNow = this.createNow.bind(this);
        this.scheduleDate = this.scheduleDate.bind(this);
        this.scheduleMonthly = this.scheduleMonthly.bind(this);
        this.changedDate = this.changedDate.bind(this);
    }

    createNow(event) {
        event.preventDefault();
        let payload = {
            testId: parseInt(this.state.testId),
            groupId: parseInt(this.state.groupId),
            creationDate: new Date()
        }

        let token = sessionStorage.getItem('userToken');
        axios({
            method: 'post',
            url: 'https://localhost:44329/TestStatistic/CreateTestStatistic/',
            headers: {
                'Authorization': token
            },
            data: payload
        }).then(response => {
            // this.setState({changed: true});
            window.alert("Statistic created successfully");
        }).catch(() => {
            window.alert("Failed to create statistic");
        });
    }

    scheduleDate(event) {
        event.preventDefault();

        let token = sessionStorage.getItem('userToken');
        axios({
            method: 'post',
            url: 'https://localhost:44329/TestStatistic/ScheduleTestStatisticCreation/',
            headers: {
                'Authorization': token
            },
            data: {
                testId: parseInt(this.state.testId),
                groupId: parseInt(this.state.groupId),
                creationDate: new Date(this.state.date)
            }
        }).then(response => {
            window.alert("Statistic scheduled successfully.")
        }).catch(() => {
            window.alert("Failed to schedle date statistic");
        });
    }

    scheduleMonthly(event) {
        event.preventDefault();
        let payload = {
            testId: parseInt(this.state.testId),
            groupId: parseInt(this.state.groupId),
            creationDate: new Date()
        }

        let token = sessionStorage.getItem('userToken');
        axios({
            method: 'post',
            url: 'https://localhost:44329/TestStatistic/ScheduleMonthlyTestStatisticCreation/',
            headers: {
                'Authorization': token
            },
            data: payload
        }).then(response => {
            window.alert("Statistic scheduled montlhy successfully.")
        }).catch(() => {
            window.alert("Failed to schedule statistic");
        });
    }

    changedDate(event) {
        this.setState({date: event.target.value});
    }

    render() {
        return (
            <div>
                <form onSubmit={this.createNow} className="form-inline ml-auto">
                    <label className="form-check-label mr-sm-2"> Create statistics now:</label>
                    <button className="btn btn-primary mb-2">Create</button>
                </form>
                <hr></hr>
                <form onSubmit={this.scheduleDate} className="form-inline ml-auto">
                    <label className="form-check-label mr-sm-2"> Schedule statistics:</label>
                    <input type="date" className="form-control mb-2 mr-sm-2" id="schedule" onChange={this.changedDate}></input>
                    <button className="btn btn-primary mb-2">Schedule</button>
                </form>
                <hr></hr>
                <form onSubmit={this.scheduleMonthly}className="form-inline ml-auto">
                    <label className="form-check-label mr-sm-2"> Schedule monthly statistics:</label>
                    <button className="btn btn-primary mb-2">Schedule</button>
                </form>
                <hr></hr>
            </div>
            
        )
    }
}

export default StatisticsDetail