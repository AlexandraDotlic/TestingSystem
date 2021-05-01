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
            allQuestions: []
        }
    }

    componentDidMount() {
        axios.get("https://localhost:44329/Test/GetAllQuestionsForTest/" + this.state.testId)
        .then(response => {
            this.setState({allQuestions: response.data.questions})
        });
    }

    render() {
        let questions = this.state.allQuestions.map(question => {
            return <Question key={question.id} questionId={question.id} questionText={question.questionText} ></Question>
        })

        return (
            <div className="w-50 mx-auto pt-3">
                <h5> Questions for test: <em>{this.state.testTitle}</em> </h5>
                <TableHeader></TableHeader>
                <hr></hr>
                <ul>
                    {questions}
                </ul>

            </div>

        );
    }
}

export default QuestionList;