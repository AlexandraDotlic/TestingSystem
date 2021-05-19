import axios from 'axios';
import React from 'react';
import QuestionList from '../questionlist/QuestionList';
import Test from './Test';
import TableHeader from './TableHeader'
import EditTestStartDateTime from './EditTestStartDateTime'

class TestList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            examinerId: 1, //temp
            examinerName: "Andrew", //temp
            allTests: [],
            showQuestionId: null,
            showQuestionName: "",
            changeStartDateTestId: null,
            changeStartDateTestTitle: ""
        }
        this.activateOrDeactivateButton = this.activateOrDeactivateButton.bind(this);
        this.listAllQuestionsButton = this.listAllQuestionsButton.bind(this);
        this.changeStartDateButton = this.changeStartDateButton.bind(this);
    }

    componentDidMount() {
        axios.get("https://localhost:44329/Examiner/GetAllTestsForExaminer/" + this.state.examinerId)
        .then(response => {
            this.setState({allTests: response.data.tests})
        });
    }

    listAllQuestionsButton = (event) => {
        let showId = parseInt(event.target.value);
        let showName = event.target.name;
        this.setState({showQuestionId: showId});
        this.setState({showQuestionName: showName});
    }

    activateOrDeactivateButton = (event) => {
        let aodId = parseInt(event.target.value);
        let aodName = parseInt(event.target.name);

        if(aodName){
            axios({
                method: 'post',
                url: 'https://localhost:44329/Test/DeactivateTest/' + aodId
              });
        }
        else{
            axios({
                method: 'post',
                url: 'https://localhost:44329/Test/ActivateTest/' + aodId
              });
        }
    }

    changeStartDateButton = (event) => {
        let startDateTestId = parseInt(event.target.value);
        let startDateTestTitle = event.target.name;
        this.setState({changeStartDateTestId: startDateTestId});
        this.setState({changeStartDateTestTitle: startDateTestTitle});
    }

    render() {

        let tests = this.state.allTests.map(test => {
            return <Test key={test.id} testId={test.id} testTitle={test.title} testExaminerId={test.examinerId} testStartDate = {test.startDate} testIsActive={test.isActive} testScore = {test.testScore} 
            questionCallback={this.listAllQuestionsButton} activateOrDeactivateCallback={this.activateOrDeactivateButton} changeStartDateCallback={this.changeStartDateButton}></Test>
        })

        if(this.state.showQuestionId == null & this.state.changeStartDateTestId == null) {
            return (
                <div className="w-50 mx-auto pt-3">
                    <h5> Test for Examiner: <em>{this.state.examinerName}</em> </h5>
                    <ul>
                        <TableHeader></TableHeader>
                    </ul>
                    <hr></hr>
                    <ul>
                        {tests}
                    </ul>

                </div>
            );
        }
        else if(this.state.showQuestionId != null) {
            return (
                <QuestionList key={this.state.showQuestionId} id={this.state.showQuestionId} title={this.state.showQuestionName}></QuestionList>
            )
        }
        else if(this.state.changeStartDateTestId != null){
            return (
                <EditTestStartDateTime key={this.state.changeStartDateTestId} id={this.state.changeStartDateTestId} title={this.state.changeStartDateTestTitle}></EditTestStartDateTime>        
            )
        }
    }
}

export default TestList;