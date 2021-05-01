import axios from 'axios';
import React from 'react'
import Question from './Question'
import TableHeader from './TableHeader'

class QuestionList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            testId: 1, //temp
            testTitle: "TestName", //should be getted 
            allQuestions: [],
            editQuestionId: null,
            deleteQuestionId: null
        }
        this.editClicked = this.editClicked.bind(this);
        this.deleteClicked = this.deleteClicked.bind(this);
    }

    componentDidMount() {
        axios.get("https://localhost:44329/Test/GetAllQuestionsForTest/" + this.state.testId)
        .then(response => {
            this.setState({allQuestions: response.data.questions})
        });
    }

    editClicked(event) {
        let questionIdEdit = parseInt(event.target.value);
        this.setState({editQuestionId: questionIdEdit});
    }

    deleteClicked(event) {
        let questionIdDelete = parseInt(event.target.value);
        debugger;
        this.setState({deleteQuestionId: questionIdDelete});
    }

    render() {
        let questions = this.state.allQuestions.map(question => {
            return <Question key={question.id} questionId={question.id} questionText={question.questionText} editCallback={this.editClicked} deleteCallback={this.deleteClicked}></Question>
        })

        if(this.state.editQuestionId == null && this.state.deleteQuestionId == null) {
            return (
                <div className="w-50 mx-auto pt-3">
                    <h5> Questions for test: <em>{this.state.testTitle}</em> </h5>
                    <ul>
                        <TableHeader></TableHeader>
                    </ul>
                    <hr></hr>
                    <ul>
                        {questions}
                    </ul>
    
                </div>
    
            );
        }
        else if(this.state.editQuestionId != null) {
            return (
                <h2> Treba da editujem pitanje sa IDom: {this.state.editQuestionId}</h2> 
            )
        }
        else if(this.state.deleteQuestionId != null) {
            return (
                <h2> Treba da obirsem pitanje sa IDom: {this.state.deleteQuestionId}</h2> 
            )
        }
        else {
            return (
                <h2> Ovde nismo trebali da dodjemo </h2>
            )
        }
    }
}

export default QuestionList;