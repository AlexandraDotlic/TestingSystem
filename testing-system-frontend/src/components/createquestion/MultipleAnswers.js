import React from 'react'
import AnswerWithDelete from './AnswerWithDelete'

class MultipleAnswers extends React.Component {
    constructor(props) {
        super(props);

        this.delete = this.delete.bind(this);
        this.addNewOption = this.addNewOption.bind(this);
        this.correct = this.correct.bind(this);
        this.text = this.text.bind(this);

        this.correctAnswerMap = new Map();
        this.textAnswerMap = new Map();
        
        this.state = {
            options: [
                <AnswerWithDelete key={1} orderNumber={1} deleteCallback={this.delete} correctChanged={this.correct} valueChanged={this.text}></AnswerWithDelete>,
                <AnswerWithDelete key={2} orderNumber={2} deleteCallback={this.delete} correctChanged={this.correct} valueChanged={this.text}></AnswerWithDelete>,
                <AnswerWithDelete key={3} orderNumber={3} deleteCallback={this.delete} correctChanged={this.correct} valueChanged={this.text}></AnswerWithDelete>
            ],
            optionNumber: 3,
        }
    }

    correct(obj) {
        this.correctAnswerMap.set(obj.id, obj.val);
    }

    text(obj) {
        this.textAnswerMap.set(obj.id, obj.val);
    }

    delete(id) {
        let newOptions = this.state.options.filter(opt => {
            if(parseInt(opt.key) == parseInt(id))
                return false;
            return true;
        });
        this.setState({options: newOptions});
    }

    addNewOption() {
        let newOptions = this.state.options;
        let n = this.state.optionNumber + 1
        let newOption = (
            <AnswerWithDelete key={n} orderNumber={n} deleteCallback={this.delete} correctChanged={this.correct} valueChanged={this.text}></AnswerWithDelete>
        )
        newOptions.push(newOption);
        this.setState({options: newOptions, optionNumber: n});
    }

    returnData() {
        let optionsFinal = this.state.options;
        let resArray = []

        for(let option of optionsFinal) {
            let optionKey = parseInt(option.key);
            let res = {
                optionText: null,
                isCorrect: null
            }

            if(this.correctAnswerMap.has(optionKey))
                res.isCorrect = this.correctAnswerMap.get(optionKey);
            else 
                res.isCorrect = false;

            if(this.textAnswerMap.has(optionKey))
                res.optionText = this.textAnswerMap.get(optionKey);
            else 
                res.optionText = ""

            resArray.push(res);
        }

        return resArray;
    }

    render() {
        let x = this.state.options
        return (
            <div>
                <div className="form-group">
                    {this.state.options}
                </div>
                <span className="btn btn-info" onClick={this.addNewOption}> Add new option </span>
            </div>
        )
    }

}

export default MultipleAnswers;