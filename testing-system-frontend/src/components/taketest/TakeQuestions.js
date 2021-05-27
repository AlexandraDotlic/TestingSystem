import React from 'react'
import axios from 'axios'
import { data } from 'jquery';

class TakeQuestions extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            testId: this.props.testId,
            questions: null
        }
        this.answersMap = new Map();
        this.answeredYesNo = this.answeredYesNo.bind(this);
        this.answeredMultiple = this.answeredMultiple.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    componentDidMount() {
        let token = sessionStorage.getItem('userToken');
        axios({
            method: 'get',
            url: 'https://localhost:44329/Test/GetAllQuestionsForTest/' + this.state.testId,
            headers: {
                'Authorization': token
            }
        }).then(response => {
            for(let question of response.data.questions) {
                this.answersMap.set(question.id, []);
            }
            this.setState({questions: response.data.questions})
        }).catch(() => {
            window.alert("Failed to get questions for this test");
        });
    }

    answeredYesNo(questionId, questionAnswer) {
        this.answersMap.set(questionId, [questionAnswer]);
    }

    answeredMultiple(questionId, questionAnswer) {
        let currentSelection = this.answersMap.get(questionId);
        if(currentSelection.includes(questionAnswer)) {
            currentSelection = currentSelection.filter(function(value, index, arr){ 
                return value == questionAnswer;
            });
        }
        else {
            currentSelection.push(questionAnswer);
        }
    }

    onSubmit() {
        let mapaOdgovora = this.answersMap;
        debugger;
    }

    render() {
        if(this.state.questions === null) {
            return (
                <div></div>
            )
        }
        else {
            const questionList = this.state.questions.map(question => {
                // Yes/No questions
                if(question.answerOptions.length == 2) {
                    return (
                        <div key={question.id} className="form-group">
                            <label className="font-weight-bold"> {question.questionText} </label>
                            <div className="form-group">
                                <div className="input-group">
                                    <div className="input-group-prepend">
                                        <div className="input-group-text">
                                            <input type="radio" name={question.id} value={question.answerOptions[0].optionText} onChange={e => this.answeredYesNo(question.id, question.answerOptions[0].optionText)}></input>
                                        </div>
                                    </div>
                                    <input disabled className="form-control" value={question.answerOptions[0].optionText}></input>
                                </div>
                            </div>
                            <div className="form-group">
                                <div className="input-group">
                                    <div className="input-group-prepend">
                                        <div className="input-group-text">
                                            <input type="radio" name={question.id} value={question.answerOptions[1].optionText} onChange={e => this.answeredYesNo(question.id, question.answerOptions[1].optionText)}></input>
                                        </div>
                                    </div>
                                    <input disabled className="form-control" value={question.answerOptions[1].optionText}></input>
                                </div>
                            </div>
                            <hr></hr>
                        </div>
                    )
                }
                // Multiple questions
                else {
                    return (
                    <div key={question.id} className="form-group">
                        <label className="font-weight-bold"> {question.questionText} </label>
                        <div className="form-group">
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <div className="input-group-text">
                                        <input onChange={e => this.answeredMultiple(question.id, question.answerOptions[0].optionText)} type="checkbox" name={question.id} value={question.answerOptions[0].optionText}></input>
                                    </div>
                                </div>
                                <input disabled className="form-control" value={question.answerOptions[0].optionText}></input>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <div className="input-group-text">
                                        <input onChange={e => this.answeredMultiple(question.id, question.answerOptions[1].optionText)} type="checkbox" name={question.id} value={question.answerOptions[1].optionText}></input>
                                    </div>
                                </div>
                                <input disabled className="form-control" value={question.answerOptions[1].optionText}></input>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <div className="input-group-text">
                                        <input onChange={e => this.answeredMultiple(question.id, question.answerOptions[2].optionText)} type="checkbox" name={question.id} value={question.answerOptions[2].optionText}></input>
                                    </div>
                                </div>
                                <input disabled className="form-control" value={question.answerOptions[2].optionText}></input>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <div className="input-group-text">
                                        <input onChange={e => this.answeredMultiple(question.id, question.answerOptions[3].optionText)} type="checkbox" name={question.id} value={question.answerOptions[3].optionText}></input>
                                    </div>
                                </div>
                                <input disabled className="form-control" value={question.answerOptions[3].optionText}></input>
                            </div>
                        </div>
                        <hr></hr>
                    </div>
                )}
            });

            return (
                <div className="w-50 mx-auto pt-4">
                    <form onSubmit={this.onSubmit}>
                        {questionList}
                        <div className="text-center">
                            <input type="submit" className="btn btn-success mb-4"></input>
                        </div>
                    </form>
                </div>
            )
        }
    }
}

export default TakeQuestions