import React from 'react'
import Answer from './Answer'

class CreateQuestion extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            answerType: "yesNo",
            questionText: ""
        };
        this.questionTextChanged = this.questionTextChanged.bind(this);
        this.answerTypeChanged = this.answerTypeChanged.bind(this);
    }

    questionTextChanged(event) {
        this.setState({questionText: event.target.value});
    }

    answerTypeChanged(event) {
        this.setState({answerType: event.target.value});
    }

    render() {
        let answer;
        if(this.state.answerType === "yesNo") {
            answer = (
                <div>
                    <Answer logo={"radio"} orderNumber={"0answer"} labelText={"Yes:"}></Answer>
                    <Answer logo={"radio"} orderNumber={"1answer"} labelText={"No:"}></Answer>
                </div>
            );
        }
        else if(this.state.answerType === "multiple") {
            answer = (
                <div>
                    <Answer logo={"checkbox"} orderNumber={"0answer"} labelText={"Option 1:"}></Answer>
                    <Answer logo={"checkbox"} orderNumber={"1answer"} labelText={"Option 2:"}></Answer>
                    <Answer logo={"checkbox"} orderNumber={"2answer"} labelText={"Option 3:"}></Answer>
                    <Answer logo={"checkbox"} orderNumber={"3answer"} labelText={"Option 4:"}></Answer>
                </div>
            );
        }
        else {
            answer = <p>ERROR WRONG TYPE</p>
        }

        return (
        <div className="w-50 mx-auto pt-4">
            <h3 className="text-center"> Create new question </h3>
            <form>
                <div className="form-group">
                    <label for="questionText">Question text: </label>
                    <textarea id="questionText" value={this.state.questionText} onChange={this.questionTextChanged} className="form-control" placeholder="Enter question text"></textarea>
                </div>

                <div class="form-group">
                    <label for="questionType">Select answer type:</label>
                    <select class="form-control" id="questionType" value={this.state.answerType} onChange={this.answerTypeChanged}>
                        <option value="yesNo">Yes / No answer</option>
                        <option value="multiple">Multiple answers</option>
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