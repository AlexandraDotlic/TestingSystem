import React from 'react'

class Answer extends React.Component {
    constructor(props) {
        super(props);

        let sValue;
        if(this.props.value !== undefined) {
            sValue = this.props.value;
        }
        else {
            sValue = "";
        }

        let sIsCorrect;
        if(this.props.isCorrect !== undefined) {
            sIsCorrect = this.props.isCorrect;
        }
        else {
            sIsCorrect = false;
        }

        this.state = {
            orderNumber: this.props.orderNumber,
            value: sValue,
            logo: this.props.logo,
            isCorrect: sIsCorrect
        }
        this.valueChanged = this.valueChanged.bind(this);
        this.correctChanged = this.correctChanged.bind(this);
    }

    valueChanged(event) {
        this.setState({value: event.target.value});
        this.props.callback({obj: this.state.orderNumber, val: event.target.value});
    }

    correctChanged(event) {
        this.props.correct(event.target.value)
    }

    render() {

        return (
        <div className="form-group">
            <label htmlFor={this.props.orderNumber}> {this.props.labelText} </label>
            <div className="input-group">
                <div className="input-group-prepend">
                    <div className="input-group-text">
                        <input defaultChecked={this.state.isCorrect} type={this.props.logo} name={"group"} value={this.state.orderNumber} onChange={this.correctChanged}></input>
                    </div>
                </div>
                <input id={this.props.orderNumber} className="form-control" placeholder="Enter answer text" value={this.state.value} onChange={this.valueChanged}></input>
            </div>
        </div>
        );
    }
}

export default Answer;