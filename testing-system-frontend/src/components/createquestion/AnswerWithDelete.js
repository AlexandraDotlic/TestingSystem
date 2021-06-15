import React from 'react'
import trash from 'bootstrap-icons/icons/trash.svg'

class AnswerWithDelete extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            orderNumber: this.props.orderNumber,
            value: "",
            logo: "checkbox",
            isCorrect: false
        }
        this.valueChanged = this.valueChanged.bind(this);
        this.correctChanged = this.correctChanged.bind(this);
        this.deleteChanged = this.deleteChanged.bind(this);
    }

    valueChanged(event) {
        this.setState({value: event.target.value});
        //this.props.valueChanged({id: this.state.orderNumber, val: event.target.value});
    }

    correctChanged(event) {
        if(this.state.isCorrect == false) {
            this.setState({isCorrect: true})
            //this.props.correctChanged({id: this.state.orderNumber, val: true})
        }
        else {
            this.setState({isCorrect: false})
            //this.props.correctChanged({id: this.state.orderNumber, val: false})
        }
    }

    deleteChanged(event) {
        this.props.deleteCallback(this.state.orderNumber);
    }

    render() {

        return (
            <div className="form-group">
                <label htmlFor={this.state.orderNumber}> Option: </label>

                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <div className="input-group-text">
                            <input defaultChecked={this.state.isCorrect} type="checkbox" name={"group"} value={this.state.orderNumber} onChange={this.correctChanged}></input>
                        </div>
                    </div>

                    <input id={this.props.orderNumber} className="form-control" placeholder="Enter answer text" value={this.state.value} onChange={this.valueChanged}></input>
                    
                    <div className="input-group-append">
                        <span className="input-group-text">
                            <img src={trash} onClick={this.deleteChanged}></img>
                        </span>
                    </div>
                </div>
            </div>    
        );
    }
}

export default AnswerWithDelete;