import React, { useEffect, useState } from 'react'
import axios from 'axios'

function StatisticsTable(props) {
    const [entries, setEntries] = useState([]);

    useEffect(() => {
        let token = sessionStorage.getItem('userToken');
        axios({
            method: 'get',
            url: 'https://localhost:44329/TestStatistic/GetAllStatisticsForTest/' + props.id,
            headers: {
                'Authorization': token
            }
        }).then(response => {
            setEntries(response.data.testStatistics)

        }).catch(() => {
            window.alert("Failed to get all statistics for test");
        });
    }, [props.id]);

    let rows = null;

    if(entries != null) {
        rows = entries.map(entry => {
            let dateChopped = new Date(entry.date);

            return (
                <tr key={entry.id}>
                    <th scope="row"> {entry.testId} </th>
                    <td>{entry.testTitle}</td>
                    <td>{entry.percentageOfStudentsWhoPassedTheTest}</td>
                    <td>{entry.numberOfStudentsWhoTookTheTest}</td>
                    <td>{dateChopped.toDateString()}</td>
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
                        <th scope="col">TestId</th>
                        <th scope="col">Title</th>
                        <th scope="col">% passed </th>
                        <th scope="col">Num of students</th>
                        <th scope="col">Date</th>
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

export default StatisticsTable