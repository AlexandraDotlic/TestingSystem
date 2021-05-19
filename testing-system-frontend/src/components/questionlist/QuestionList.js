import axios from 'axios';
import React from 'react'
import Question from './Question'
import TableHeader from './TableHeader'
import EditQuestion from './EditQuestion'
import DeleteQuestion from './DeleteQuestion'
import CreateQuestion from '../createquestion/CreateQuestion'

class QuestionList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            testId: this.props.id,
            testTitle: this.props.title,
            allQuestions: [],
            editQuestionId: null,
            deleteQuestionId: null,
            addQuestion: null,
        }
        this.editClicked = this.editClicked.bind(this);
        this.deleteClicked = this.deleteClicked.bind(this);
        this.addClicked = this.addClicked.bind(this);
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
        this.setState({deleteQuestionId: questionIdDelete});
    }

    addClicked(event) {
        console.log("CLicked");
        this.setState({addQuestion: true})
    }

    render() {
        let questions = this.state.allQuestions.map(question => {
            return <Question key={question.id} questionId={question.id} questionText={question.questionText} editCallback={this.editClicked} deleteCallback={this.deleteClicked}></Question>
        })

        if(this.state.editQuestionId == null && this.state.deleteQuestionId == null && this.state.addQuestion == null) {
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
                    <hr></hr>
                    <div className="text-center">
                        <button className="btn btn-success" onClick={this.addClicked}> Add new question </button>
                    </div>
    
                </div>
    
            );
        }
        else if(this.state.editQuestionId != null) {
            return (
                <EditQuestion id={this.state.editQuestionId} testId={this.state.testId}></EditQuestion>
            )
        }
        else if(this.state.deleteQuestionId != null) {
            return (
                <DeleteQuestion id={this.state.deleteQuestionId}></DeleteQuestion>
            )
        }
        else if(this.state.addQuestion != null) {
            debugger;
            return (
                <CreateQuestion></CreateQuestion>
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