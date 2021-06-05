import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { render } from '@testing-library/react';

function TakeResults() {
    const [results, setResults] = useState([]);

    useEffect(() => {
        const token = sessionStorage.getItem('userToken');
        axios({
            method: 'get',
            url: 'https://localhost:44329/Student/GetStudentResultsForAllTakenTests',
            headers: {
                'Authorization': token
            }
        }).then(response => {
            setResults(response.data.studentResults);
        }).catch(() => {
            window.alert("Failed to get student results.");
        });
    }, []);

    const resultList = results.map(result => {
        return (
            <tr key={result.testId}>
                <td> {result.testName} </td>
                <td> {result.studentTestScore} </td>
                <td> {result.totalTestScore} </td>
            </tr>
        )
    });

    return (
        <div>
            <table class="table">
                <thead>
                    <tr>
                    <th scope="col">Test title:</th>
                    <th scope="col">Result:</th>
                    <th scope="col">Out of:</th>
                    </tr>
                </thead>
                <tbody>
                    {resultList}
                </tbody>
            </table>
        </div>
    )
}

export default TakeResults;