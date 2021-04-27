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
                <div className="row">
                    <div className="col-sm">
                        { this.state.questionId}
                    </div>
                    <div className="col-sm">
                        { this.state.questionText}
                    </div>
                </div>
            </div>
        );
    }
}

export default Question