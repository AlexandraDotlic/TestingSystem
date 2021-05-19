import React from 'react'
import moment from 'moment';


class Test extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            testId: this.props.testId,
            testTitle: this.props.testTitle,
            testExaminerId: this.props.testExaminerId,
            testStartDateTime: this.props.testStartDate,
            testStartDate: moment(this.props.testStartDate).format("Do MMM 'YY"),
            testStartTime: moment(this.props.testStartDate).format("h:mm:ss a"),
            testIsActive: this.props.testIsActive,
            testScore: this.props.testScore
        }
    }

    render() {
        return (
            <div className="container">
                <div className="row p-3">
                    <div className="col-lg text-center">
                        { this.state.testTitle}
                    </div>
                    <div className="col-sm text-center">
                        { this.state.testStartDate}
                    </div>
                    <div className="col-sm text-center">
                        { this.state.testStartTime}
                    </div>
                    <div className="col-sm text-center">
                        { this.state.testScore}
                    </div>
                    <div className="col-lg text-center">
                        <button className="btn btn-success" onClick={this.props.activateOrDeactivateCallback} value={this.state.testId} name={Number(this.state.testIsActive)}> {(this.state.testIsActive ? "Deactivate" : "Activate")} </button>
                    </div> 
                    <div className="col-lg text-center">
                        <button className="btn btn-success" onClick={this.props.questionCallback} value={this.state.testId} name={this.state.testTitle}>Questions</button>
                    </div>                        
                    <div className="col-lg text-center">
                        <button className="btn btn-success" onClick={this.props.changeStartDateCallback} value={this.state.testId} name={this.state.testTitle}>Edit Date</button>
                    </div>
                </div>
            </div>
        );
    }
}

export default Test