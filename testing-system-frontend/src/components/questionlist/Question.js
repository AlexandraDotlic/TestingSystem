import React from 'react'

class Question extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            questionId: this.props.questionId,
            questionText: this.props.questionText
        }
    }

    render() {
        return (
            <div className="container">
                <div className="row p-1">
                    <div className="col-lg text-center">
                        { this.state.questionText}
                    </div>
                    <div className="col-sm text-center">
                        {/* <button className="btn btn-info" onClick={this.props.editCallback} value={this.state.questionId}> Edit </button> */}
                    </div>
                    <div className="col-sm text-center">
                        <button className="btn btn-info" onClick={this.props.deleteCallback} value={this.state.questionId}> Delete </button>
                    </div>
                </div>
            </div>
        );
    }
}

export default Question