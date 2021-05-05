import React from 'react';
import './contentBoxes.css';
import CreateTestForm from '../CreateTestForm'
import CreateGroupForm from '../CreateGroupForm'

{/* 
<CreateTestForm></CreateTestForm>
<CreateQuestion></CreateQuestion>
<CreateGroupForm />
<Register></Register>
<Login></Login> 
<QuestionList></QuestionList>
<StudentList></StudentList>
*/}

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            clicked: null
        }
    }

    clicked(value) {
        this.setState({clicked: value})
    }

    render() {
        if(this.state.clicked === null) {
            return (
                <div className="w-50 mx-auto pt-5">
                    <div className="container">
                        <div className="row p-1">
                            <button onClick={() => this.clicked("newtest")} className="button" style={{backgroundColor: '#5027c2'}}><span>Create new test</span></button>
                            <button onClick={() => this.clicked("newgroup")} className="button" style={{backgroundColor: '#b0b0f5'}}><span>Create new group</span></button>
                            <button onClick={() => this.clicked("stats")} className="button" style={{backgroundColor: '#5027c2'}}><span>Statistics</span></button>
                        </div>
                    </div>
                    <div className="container">
                        <div className="row p-1">
                            <button className="button" style={{backgroundColor: '#b0b0f5'}}><span>List all tests </span></button>
                            <button className="button" style={{backgroundColor: '#5027c2'}}><span>List all groups </span></button>
                            <button className="button" style={{backgroundColor: '#b0b0f5'}}><span> EMPTY </span></button>
                        </div>
                    </div>
                </div>
            )
        }
        else if(this.state.clicked === "newtest") {
            return (
                <CreateTestForm></CreateTestForm>
            )
        }
        else if(this.state.clicked === "newgroup") {
            return (
                <CreateGroupForm></CreateGroupForm>
            )
        }
        else {
            return ( <div> Error </div> )
        }

    }
}

export default Home