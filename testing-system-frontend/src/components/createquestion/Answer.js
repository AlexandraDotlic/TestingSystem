import React from 'react'

class Answer extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            orderNumber: this.props.orderNumber,
            value: ""
        }
    }

    render() {
        return (
            <div className="form-group">
            <label for={this.props.orderNumber}>Question text: </label>
            <input id={this.props.orderNumber} className="form-control" placeholder="Enter answer text" value={this.state.value}></input>
        </div>
        );
    }
}

export default Answer;