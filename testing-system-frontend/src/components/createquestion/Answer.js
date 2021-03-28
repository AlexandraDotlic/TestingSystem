import React from 'react'

class Answer extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            orderNumber: this.props.orderNumber,
            value: "",
            logo: this.props.logo
        }
        this.valueChanged = this.valueChanged.bind(this);
    }

    valueChanged(event) {
        this.setState({value: event.target.value});
    }

    render() {
        return (
        <div className="form-group">
            <label for={this.props.orderNumber}> {this.props.labelText} </label>
            <div className="input-group">
                <div class="input-group-prepend">
                    <div class="input-group-text">
                        <input type={this.props.logo} checked disabled></input>
                    </div>
                </div>
                <input id={this.props.orderNumber} className="form-control" placeholder="Enter answer text" value={this.state.value} onChange={this.valueChanged}></input>
            </div>
        </div>
        );
    }
}

export default Answer;