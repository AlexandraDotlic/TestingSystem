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
            this.setState({questions: response.data.questions})
        }).catch(() => {
            window.alert("Failed to get questions for this test");
        });
    }

    render() {
        if(this.state.questions === null) {
            return (
                <div></div>
            )
        }
        else {
            let x = this.state.questions;
            debugger;
            const questionList = this.state.questions.map(question => {
                // Yes/No questions
                if(question.answerOptions.length == 2) {
                    return (
                        <div className="form-group">
                            <label className="font-weight-bold"> {question.questionText} </label>
                            <div className="form-group">
                                <div className="input-group">
                                    <div className="input-group-prepend">
                                        <div className="input-group-text">
                                            <input type="radio" name={question.id} value={question.answerOptions[0].optionText}></input>
                                        </div>
                                    </div>
                                    <input disabled className="form-control" value={question.answerOptions[0].optionText}></input>
                                </div>
                            </div>
                            <div className="form-group">
                                <div className="input-group">
                                    <div className="input-group-prepend">
                                        <div className="input-group-text">
                                            <input type="radio" name={question.id} value={question.answerOptions[1].optionText}></input>
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
                    <div className="form-group">
                        <label className="font-weight-bold"> {question.questionText} </label>
                        <div className="form-group">
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <div className="input-group-text">
                                        <input type="checkbox" name={question.id} value={question.answerOptions[0].optionText}></input>
                                    </div>
                                </div>
                                <input disabled className="form-control" value={question.answerOptions[0].optionText}></input>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <div className="input-group-text">
                                        <input type="checkbox" name={question.id} value={question.answerOptions[1].optionText}></input>
                                    </div>
                                </div>
                                <input disabled className="form-control" value={question.answerOptions[1].optionText}></input>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <div className="input-group-text">
                                        <input type="checkbox" name={question.id} value={question.answerOptions[2].optionText}></input>
                                    </div>
                                </div>
                                <input disabled className="form-control" value={question.answerOptions[2].optionText}></input>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <div className="input-group-text">
                                        <input type="checkbox" name={question.id} value={question.answerOptions[3].optionText}></input>
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
                    <form>
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