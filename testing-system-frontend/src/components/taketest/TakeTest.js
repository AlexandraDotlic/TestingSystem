import axios from 'axios'
import React from 'react'
import TakeQuestions from './TakeQuestions'
import TakeResults from './TakeResults'

class TakeTest extends React.Component {
    constructor(props) {
        super(props) 
        this.state = {
            currentState: "selectTest",
            tests: null,
            selectedTestId: 0,
            totalScore: 0,
            studentScore: 0
        }
        this.selectedChange = this.selectedChange.bind(this);
        this.testResults = this.testResults.bind(this);
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
        this.setState({selectedTestId: event.target.value, currentState: "takeTest"})
    }

    testResults(totalScore, studentScore) {
        this.setState({selectedTestId: 0, totalScore: totalScore, studentScore: studentScore, currentState: "results"});
    }


    render() {
        let testOptions;
        if(this.state.tests !== null) {
            testOptions = this.state.tests.map(test => {
                return <option key={test.id} value={test.id}> {test.title} </option>
            })
        }

        let selectMenu = (
            <form>
                    <div className="form-group">
                        <label htmlFor="questionType">Select test: </label>
                        <select className="form-control" id="questionType" value={this.state.selectedTestId} onChange={this.selectedChange}>
                            <option > Select test to take:</option>
                            {testOptions}
                        </select>
                    </div>
            </form>
        )

        if(this.state.currentState === "selectTest") {
            return (
                <div className="w-50 mx-auto pt-4">
                    {selectMenu}
                    <hr/>
                    <TakeResults/>
                </div>
            )
        }
        else if(this.state.currentState === "results") {
            return (
                <div className="w-50 mx-auto pt-4">
                    {selectMenu}
                    <div className="p-3 mb-2 bg-primary text-white">
                        You scored {this.state.studentScore} out of {this.state.totalScore}. Congrats !
                    </div>
                    <hr/>
                    <TakeResults/>
                </div>
            )
        }
        else if(this.state.currentState === "takeTest") {
            return (
                <div>
                    <TakeQuestions testId={this.state.selectedTestId} finishedCallback={this.testResults}></TakeQuestions>
                </div>
            )
        }
    }
}

export default TakeTest;