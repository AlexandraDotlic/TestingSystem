import React from 'react'
import axios from 'axios';

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
        window.alert(event.target.value);
        this.setState({selectedTestId: event.target.value})
    }

    render() {
        let testOptions;
        if(this.state.tests !== null) {
            testOptions = this.state.tests.map(test => {
                return <option value={test.id}> {test.title} </option>
            })
        }

        return (
            <div className="w-50 mx-auto pt-4">
                <form>
                    <div className="form-group">
                        <label htmlFor="questionType">Select test: </label>
                        <select className="form-control" id="questionType" value={this.state.selectedTestId} onChange={this.selectedChange}>
                            {testOptions}
                        </select>
                    </div>
                </form>
            </div>
        )
    }
}

export default Statistics;