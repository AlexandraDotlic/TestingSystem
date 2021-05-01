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
                    <div className="col-lg align-baseline">
                        { this.state.questionText}
                    </div>
                    <div className="col-sm">
                        <button className="btn btn-info"> Edit </button>
                    </div>
                    <div className="col-sm">
                        <button className="btn btn-info"> Delete </button>
                    </div>
                </div>
            </div>
        );
    }
}

export default Question