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
            testStartTime: moment(this.props.testStartDate).format("hh:mm:ss a"),
            testIsActive: this.props.testIsActive,
            testScore: this.props.testScore,
            value: this.props.testId,
            onClick: null,
            name: ""
        }
    }    

    render() {
        
        return (
            <div className="container">
                <div className="row">
                    <div className="col-lg text-center">
                        { this.state.testTitle}
                    </div>
                    <div className="col-lg text-center">
                        { this.state.testStartDate}
                    </div>
                    <div className="col-lg text-center">
                        { this.state.testStartTime}
                    </div>
                    <div className="col-sm text-center">
                        { this.state.testScore}
                    </div>
                    <div className="col-sm text-center">
                        <div className="dropdown">
                            <button className="btn btn-primary btn-sm dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                            <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                <a className="dropdown-item" id={this.state.testId} name={Number(this.state.testIsActive)} onClick={this.props.activateOrDeactivateCallback}>{(this.state.testIsActive ? "Deactivate" : "Activate")}</a>
                                <a className="dropdown-item" id={this.state.testId} name={this.state.testTitle} onClick={this.props.questionCallback} >Questions</a>
                                <a className="dropdown-item" id={this.state.testId} name={this.state.testTitle} onClick={this.props.changeStartDateCallback}>Change Start Date</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Test