import React from 'react'
import axios from 'axios'

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
        this.answeredFreeText = this.answeredFreeText.bind(this);
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
            this.setState({questions: response.data.questions})
        }).catch(() => {
            window.alert("Failed to get questions for this test");
        });
    }

    answeredYesNo(questionId, questionAnswer) {
        this.answersMap.set(questionId, [questionAnswer]);
    }

    answeredFreeText(questionId, e) {
        this.answersMap.set(questionId, [e.target.value]);
    }

    answeredMultiple(questionId, questionAnswer) {
        if(this.answersMap.has(questionId)) {
            let currentSelection = this.answersMap.get(questionId);
            if(currentSelection.includes(questionAnswer)) {
                currentSelection = currentSelection.filter(f => f == questionAnswer ? false : true);
            }
            else {
                currentSelection.push(questionAnswer);
            }
            this.answersMap.set(questionId, currentSelection);
        }
        else {
            this.answersMap.set(questionId, [questionAnswer]);
        }
    }

    onSubmit(event) {
        event.preventDefault();
        let responsesArray = [];
        for(let questionId of this.answersMap.keys()) {
            responsesArray.push( {questionId: questionId, responses: this.answersMap.get(questionId)});
        }
        debugger;
        let requestPayload = {
            testId: parseInt(this.state.testId),
            response: responsesArray
        }

        let token = sessionStorage.getItem('userToken');
        axios({
            method: 'post',
            url: 'https://localhost:44329/Test/TakeTheTest/',
            headers: {
                'Authorization': token
            },
            data: requestPayload
        }).then(response => {
            this.props.finishedCallback(response.data.totalTestScore, response.data.studentScore);
        }).catch(err => {
            window.alert("Failed to take test");
        });
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
                if(question.answerOptions.length === 2) {
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
                // Free text
                else if(question.answerOptions.length === 1 && question.answerOptions[0].optionText === "") {
                    return (
                        <div key={question.id} className="form-group">
                        <label className="font-weight-bold"> {question.questionText} </label>
                        <div className="form-group">
                            <div className="input-group">
                                <textarea className="form-control" onChange={e => this.answeredFreeText(question.id, e)}></textarea>
                            </div>
                        </div>
                        <hr></hr>
                    </div>
                    )
                }
                // Multiple questions
                else {
                    const questionOptionsNumber = question.answerOptions.length;
                    let answerOptions = [];
                    for(let i = 0; i < questionOptionsNumber; i = i + 1) {
                        const answerOption = (
                            <div className="form-group" key={question.id + "-" + i}>
                                <div className="input-group">
                                    <div className="input-group-prepend">
                                        <div className="input-group-text">
                                            <input onChange={e => this.answeredMultiple(question.id, question.answerOptions[i].optionText)} type="checkbox" name={question.id} value={question.answerOptions[i].optionText}></input>
                                        </div>
                                    </div>
                                    <input disabled className="form-control" value={question.answerOptions[i].optionText}></input>
                                </div>
                            </div>
                        );
                        answerOptions.push(answerOption);
                    }

                    return (
                        <div key={question.id} className="form-group">
                            <label className="font-weight-bold"> {question.questionText} </label>
                                {answerOptions}
                            <hr></hr>
                        </div>
                    )
                }
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