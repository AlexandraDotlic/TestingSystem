import React from 'react'

class QuestionList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            testTitle: "TestExample" //temp
        }
    }

    render() {
        return (
            <div className="w-50 mx-auto pt-3">
                <h5> Questions for test: <em>{this.state.testTitle}</em> </h5>
            </div>

        );
    }
}

export default QuestionList;