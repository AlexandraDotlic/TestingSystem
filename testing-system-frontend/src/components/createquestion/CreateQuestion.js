import axios from 'axios';
import { data } from 'jquery';
import React, { createRef } from 'react'
import Answer from './Answer'
import MultipleAnswers from './MultipleAnswers'

class CreateQuestion extends React.Component {
    constructor(props) {
        super(props);
        this.multipleAnswers = React.createRef()
        this.state = {
            testId: this.props.testId, 
            answerType: "yesNo",
            questionText: ""
        };
        this.answersMap = new Map();
        this.correctAnswer = [];
        this.questionTextChanged = this.questionTextChanged.bind(this);
        this.answerTypeChanged = this.answerTypeChanged.bind(this);
        this.submit = this.submit.bind(this);
        this.getAnswerText = this.getAnswerText.bind(this);
        this.setCorrectAnswer = this.setCorrectAnswer.bind(this);
    }

    questionTextChanged(event) {
        this.setState({questionText: event.target.value});
    }

    answerTypeChanged(event) {
        this.setState({answerType: event.target.value});
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
        let dataObject = null;
        
        if(this.state.answerType === "multiple") {
            let data = this.multipleAnswers.current.returnData();
            
            dataObject = {
                testId: this.state.testId,
                questionText: this.state.questionText,
                answerOptions: data
            }
        }
        else if(this.state.answerType === "freeText") {
            dataObject = {
                testId: this.state.testId,
                questionText: this.state.questionText,
                answerOptions: [{optionText: "", isCorrect: true}]
            }
        }
        else {
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
    
            dataObject = {
                testId: this.state.testId,
                questionText: this.state.questionText,
                answerOptions: answers
            }

        }

        let token = sessionStorage.getItem('userToken');
        axios({
            method: 'post',
            url: 'https://localhost:44329/Test/AddQuestionToTest',
            headers: {
                'Authorization': token
            },
            data: dataObject
        }).then(() => {
            window.alert("Question created successfully.");
        }).catch(() => {
            window.alert("Failed to add question to test.");
        });
    }

    render() {
        let answer;
        if(this.state.answerType === "yesNo") {
            answer = (
                <div>
                    <Answer callback={this.getAnswerText} correct={this.setCorrectAnswer} logo={"radio"} orderNumber={"0answer"} labelText={"Yes:"}></Answer>
                    <Answer callback={this.getAnswerText} correct={this.setCorrectAnswer} logo={"radio"} orderNumber={"1answer"} labelText={"No:"}></Answer>
                </div>
            );
        }
        else if(this.state.answerType === "multiple") {
            answer = (<MultipleAnswers ref={this.multipleAnswers}></MultipleAnswers>)
        }
        else if(this.state.answerType === "freeText") {
            answer = (<div></div>)
        }
        else {
            answer = <p>ERROR WRONG TYPE</p>
        }

        return (
        <div className="w-50 mx-auto pt-4">
            <h3 className="text-center"> Create new question </h3>
            <form onSubmit={this.submit}>
                <div className="form-group">
                    <label htmlFor="questionText">Question text: </label>
                    <textarea id="questionText" value={this.state.questionText} onChange={this.questionTextChanged} className="form-control" placeholder="Enter question text"></textarea>
                </div>

                <div className="form-group">
                    <label htmlFor="questionType">Select answer type:</label>
                    <select className="form-control" id="questionType" value={this.state.answerType} onChange={this.answerTypeChanged}>
                        <option value="yesNo">Yes / No answer</option>
                        <option value="multiple">Multiple answers</option>
                        <option value="freeText"> Fill text </option>
                    </select>
                </div>

                {answer}

                <div className="text-center mb-2">
                    <button type="submit" className="btn btn-success">Submit</button>
                </div>
            </form>
        </div>
        );
        
    }
}
export default CreateQuestion;