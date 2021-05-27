import React from 'react'
import axios from 'axios'

class StatisticsTable extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            testId: this.props.id,
            entries: null
        }
    }

    componentDidMount() {
        let token = sessionStorage.getItem('userToken');
        axios({
            method: 'get',
            url: 'https://localhost:44329/TestStatistic/GetAllStatisticsForTest/' + this.state.testId,
            headers: {
                'Authorization': token
            }
        }).then(response => {
            this.setState({entries: response.data.teststatistics})

        }).catch(() => {
            window.alert("Failed to get all statistics for test");
        });
    }

    render() {
        let rows;
        if(this.state.entries != null) {
            rows = this.state.entries.map(entry => {
                return (
                    <tr>
                        <th scope="row"> {entry.testId} </th>
                        <td>{entry.testTitle}</td>
                        <td>{entry.percentageOfStudentsWhoPassedTheTest}</td>
                        <td>{entry.numberOfStudentsWhoTookTheTest}</td>
                    </tr>
                )
            })
        }

        return (
        <div className="w-75 mx-auto pt-4 pb-4">
            <div className="table table-responsive">
                <table className="table table-striped w-100">
                    <thead>
                        <tr>
                        <th scope="col">Id</th>
                        <th scope="col">Title</th>
                        <th scope="col">% students that passed test </th>
                        <th scope="col">Total number of students</th>
                        </tr>
                    </thead>
                    <tbody>
                        {rows}
                    </tbody>
                </table>
            </div>
        </div>
        )
    }
}

export default StatisticsTable