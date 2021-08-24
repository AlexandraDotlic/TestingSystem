import axios from 'axios';
import React from 'react';

class DeleteQuestion extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            questionId: this.props.id,
            testId: this.props.testId
        };
        this.delete = this.delete.bind(this);
    }

    componentDidMount() {
    }

    delete() {
        let token = sessionStorage.getItem('userToken');
        axios({
            method: 'post',
            url: 'https://localhost:44329/Test/RemoveQuestionFromTest/',
            headers: {
                'Authorization': token
            },
            data: {
                testId: this.state.testId,
                questionId: this.state.questionId
            }
        }).then(() => {
            this.props.cancelCallback();
        }).catch(() => {
            window.alert("Failed to delete question");
        });
    }

    render() {  
        return (
            <div className="w-50 mx-auto pt-4">
                <h3 className="text-center"> Confirm deleting this question? </h3>
                <div className="text-center">
                    <button className="btn btn-success m-2" onClick={this.delete}> Confirm </button>
                    <button className="btn btn-danger"> Discard </button>
                </div>
            </div>
        )      
    }
}
export default DeleteQuestion;