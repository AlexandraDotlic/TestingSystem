import axios from 'axios'
import React from 'react'
import TakeQuestions from './TakeQuestions'

class TakeTest extends React.Component {
    constructor(props) {
        super(props) 
        this.state = {
            currentState: "selectTest",
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
        this.setState({selectedTestId: event.target.value, currentState: "takeTest"})
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
                </div>
            )
        }
        else if(this.state.currentState === "takeTest") {
            return (
                <div>
                    <TakeQuestions testId={this.state.selectedTestId}></TakeQuestions>
                </div>
            )
        }
    }
}

export default TakeTest;