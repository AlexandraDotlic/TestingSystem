import React from 'react'
import axios from 'axios';
import StatisticsDetail from './StatisticsDetail';

class CreateStatistics extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tests: null,
            groups: null,
            selectedTestId: 0,
            selectedGroupId: 0
        }
        this.selectedChange = this.selectedChange.bind(this);
        this.selectedGroupChange = this.selectedGroupChange.bind(this);
    }

    componentDidMount() {
        let token = sessionStorage.getItem('userToken');
        axios({
            method: 'get',
            url: 'https://localhost:44329/Test/GetAllTests/',
            headers: {
                'Authorization': token
            }
        }).then(responseTest => {
            axios({
                method: 'get',
                url: 'https://localhost:44329/Examiner/GetAllGroupsForExaminer',
                headers: {
                    'Authorization': token
                }
            }).then(responseGroup => {
                this.setState({tests: responseTest.data, groups: responseGroup.data.groups})
            }).catch(() => {
                window.alert("Failed to get all groups");
            });
        }).catch(() => {
            window.alert("Failed to get all tests");
        });
    }

    selectedChange(event) {
        this.setState({selectedTestId: event.target.value})
    }

    selectedGroupChange(event) {
        this.setState({selectedGroupId: event.target.value})
    }

    render() {
        let testOptions;
        if(this.state.tests !== null) {
            testOptions = this.state.tests.map(test => {
                return <option key={test.id} value={test.id}> {test.title} </option>
            })
        }

        let groupOptions;
        if(this.state.groups !== null) {
            groupOptions = this.state.groups.map(group => {
                return <option key={group.id} value={group.id}> {group.title} </option>
            })
        }

        if(this.state.selectedTestId === 0 || this.state.selectedGroupId === 0) {
            return (
                <div className="w-50 mx-auto pt-4">
                    <form>
                        <div className="form-group">
                            <label htmlFor="testID">Select test: </label>
                            <select className="form-control" id="testID" value={this.state.selectedTestId} onChange={this.selectedChange}>
                                <option> -- Select test from list: --</option>
                                {testOptions}
                            </select>
                            <label htmlFor="groupID">Select group: </label>
                            <select className="form-control" id="groupID" value={this.state.selectedGroupId} onChange={this.selectedGroupChange}>
                                <option> -- Select group from list: --</option>
                                {groupOptions} 
                            </select>
                        </div>
                        <div className="text-center mt-3">
                            <button className="btn btn-warning" onClick={() => this.props.cancelCallback()}> Back </button>
                        </div>
                    </form>
                </div>
            )
        }
        else {
            return (
                <div className="w-50 mx-auto pt-4">
                    <StatisticsDetail testId={this.state.selectedTestId} groupId={this.state.selectedGroupId}></StatisticsDetail>
                    <div className="text-center mt-3">
                        <button className="btn btn-warning" onClick={() => this.props.cancelCallback()}> Back </button>
                    </div>
                </div>
            )
        }
    }
}

export default CreateStatistics;