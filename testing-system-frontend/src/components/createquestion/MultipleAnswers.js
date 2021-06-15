import React from 'react'
import AnswerWithDelete from './AnswerWithDelete'

class MultipleAnswers extends React.Component {
    constructor(props) {
        super(props);

        this.delete = this.delete.bind(this);
        this.addNewOption = this.addNewOption.bind(this);
        
        this.state = {
            options: [
                <AnswerWithDelete key={1} orderNumber={1} deleteCallback={this.delete}></AnswerWithDelete>,
                <AnswerWithDelete key={2} orderNumber={2} deleteCallback={this.delete}></AnswerWithDelete>,
                <AnswerWithDelete key={3} orderNumber={3} deleteCallback={this.delete}></AnswerWithDelete>
            ]
        }
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
        let n = newOptions.length + 1
        let newOption = (
            <AnswerWithDelete key={n} orderNumber={n} deleteCallback={this.delete}></AnswerWithDelete>
        )
        newOptions.push(newOption);
        this.setState({options: newOptions});
    }

    render() {
        debugger;
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