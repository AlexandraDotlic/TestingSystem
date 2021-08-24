import axios from 'axios';
import React from 'react';
import Answer from '../createquestion/Answer'

class EditQuestion extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            questionId: this.props.id,
            testId: this.props.testId,
            answerType: "multiple",
            questionText: "",
            answers: undefined
        };
        this.answersMap = new Map();
        this.correctAnswer = [];
        this.questionTextChanged = this.questionTextChanged.bind(this);
        this.getAnswerText = this.getAnswerText.bind(this);
        this.setCorrectAnswer = this.setCorrectAnswer.bind(this);
        this.submit = this.submit.bind(this);
    }

    componentDidMount() {
        axios({
            type: 'GET',
            url: 'https://localhost:44329/Test/GetQuestionAndAnswers/',
            params: {
                testId: this.props.testId,
                questionId: this.props.id
            }
        }).then(response => {
            this.setState({questionText: response.data.questionText});
            if(response.data.answerOptions.length === 2) {
                this.setState({answerType: "yesNo"});
            }
            this.setState({answers: response.data.answerOptions})
        });
    }

    questionTextChanged(event) {
        this.setState({questionText: event.target.value});
    }

    getAnswerText(data) {
        this.answersMap.set(data.obj, data.val);
    }

    setCorrectAnswer(data) {
        if(this.state.answerType === "yesNo") {
            this.correctAnswer = [];
            this.correctAnswer.push(data);
        }
        if(this.state.answerType === "multiple") {
            if(this.correctAnswer.includes(data)) {
                let i = this.correctAnswer.indexOf(data);
                this.correctAnswer.splice(i, 1);
            }
            else {
                this.correctAnswer.push(data);
            }
        }
    }

    submit(event) {
        let answers = [];
        for(let answer of this.answersMap) {
            let correctCurrent = false;
            if(this.correctAnswer.includes(answer[0])) {
                correctCurrent = true;
            }
            answers.push({
                optionText: answer[1],
                isCorrect: correctCurrent
            });
        }
        
        let dataObject = {
        testId: this.state.testId,
        questionText: this.state.questionText,
        answerOptions: answers
        }

        debugger;
        axios.defaults.headers.post['Content-Type'] = 'application/json';
        axios.post(
            "https://localhost:44329/Test/AddQuestionToTest", 
            JSON.stringify(dataObject)
        );
    }

    render() {
        let answer;
        if(this.state.answers !== undefined) {
            let data = this.state.answers;
            if(this.state.answerType === "yesNo") {
                answer = (
                    <div>
                        <Answer value={data[0].optionText} isCorrect={data[0].isCorrect} callback={this.getAnswerText} correct={this.setCorrectAnswer} logo={"radio"} orderNumber={"0answer"} labelText={"Yes:"}></Answer>
                        <Answer value={data[1].optionText} isCorrect={data[1].isCorrect} callback={this.getAnswerText} correct={this.setCorrectAnswer} logo={"radio"} orderNumber={"1answer"} labelText={"No:"}></Answer>
                    </div>
                );
            }
            else if(this.state.answerType === "multiple") {
                answer = (
                    <div>
                        <Answer value={data[0].optionText} isCorrect={data[0].isCorrect} callback={this.getAnswerText} correct={this.setCorrectAnswer} logo={"checkbox"} orderNumber={"0answer"} labelText={"Option 1:"}></Answer>
                        <Answer value={data[1].optionText} isCorrect={data[1].isCorrect} callback={this.getAnswerText} correct={this.setCorrectAnswer} logo={"checkbox"} orderNumber={"1answer"} labelText={"Option 2:"}></Answer>
                        <Answer value={data[2].optionText} isCorrect={data[2].isCorrect} callback={this.getAnswerText} correct={this.setCorrectAnswer} logo={"checkbox"} orderNumber={"2answer"} labelText={"Option 3:"}></Answer>
                        <Answer value={data[3].optionText} isCorrect={data[3].isCorrect} callback={this.getAnswerText} correct={this.setCorrectAnswer} logo={"checkbox"} orderNumber={"3answer"} labelText={"Option 4:"}></Answer>
                    </div>
                );
            }
            else {
                answer = <p>ERROR WRONG TYPE</p>
            }
        }

        return (
        <div className="w-50 mx-auto pt-4">
            <h3 className="text-center"> Edit question: <em>{this.state.questionText}</em> </h3>
            <form onSubmit={this.submit}>
                <div className="form-group">
                    <label htmlFor="questionText">Question text: </label>
                    <textarea id="questionText" value={this.state.questionText} onChange={this.questionTextChanged} className="form-control" placeholder="Enter question text"></textarea>
                </div>

                {answer}

                <div className="text-center mb-2">
                    <button type="submit" className="btn btn-success">Update</button>
                </div>
            </form>
        </div>
        );
        
    }
}
export default EditQuestion;