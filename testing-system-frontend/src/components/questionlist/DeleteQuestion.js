import axios from 'axios';
import React from 'react';

class DeleteQuestion extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            questionId: this.props.id,
        };
    }

    componentDidMount() {
    }

    render() {  
        return (
            <div className="w-50 mx-auto pt-4">
                <h3 className="text-center"> Confirm deleting this question? </h3>
                <div className="text-center">
                    <button className="btn btn-success m-2"> Confirm </button>
                    <button className="btn btn-danger"> Discard </button>
                </div>
            </div>
        )      
    }
}
export default DeleteQuestion;