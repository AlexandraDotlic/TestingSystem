import React from 'react'
import axios from 'axios';
import StatisticsTable from './StatisticsTable'
import StatisticsDetails from './StatisticsDetail'
import StatisticsDetail from './StatisticsDetail';

class Statistics extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tests: null,
            selectedTestId: 0
        }
        this.selectedChange = this.selectedChange.bind(this);
    }

    componentDidMount() {
        axios({
            method: 'get',
            url: 'https://localhost:44329/Test/GetAllTests/',
        }).then(response => {
            this.setState({tests: response.data})
        }).catch(() => {
            window.alert("Failed to get all tests");
        });
    }

    selectedChange(event) {
        this.setState({selectedTestId: event.target.value})
    }

    render() {
        let testOptions;
        if(this.state.tests !== null) {
            testOptions = this.state.tests.map(test => {
                return <option key={test.id} value={test.id}> {test.title} </option>
            })
        }

        if(this.state.selectedTestId == 0) {
            return (
                <div className="w-50 mx-auto pt-4">
                    <form>
                        <div className="form-group">
                            <label htmlFor="questionType">Select test: </label>
                            <select className="form-control" id="questionType" value={this.state.selectedTestId} onChange={this.selectedChange}>
                                <option> -- Select test from list: --</option>
                                {testOptions}
                            </select>
                        </div>
                        <div className="text-center mt-3">
                            <button className="btn btn-warning" onClick={() => this.props.cancelCallback()}> Discard </button>
                        </div>
                    </form>
                </div>
            )
        }
        else {
            return (
                <div className="w-50 mx-auto pt-4">
                    <StatisticsTable id={this.state.selectedTestId}></StatisticsTable>
                    <StatisticsDetail id={this.state.selectedTestId}></StatisticsDetail>
                    <div className="text-center mt-3">
                        <button className="btn btn-warning" onClick={() => this.props.cancelCallback()}> Discard </button>
                    </div>
                </div>
            )
        }
    }
}

export default Statistics;