import React from 'react';
import axios from 'axios';
import 'bootstrap';


class CreateGroupForm extends React.Component{
    constructor(props){
        super(props);
        this.state = {
            groupName: ""        
        };
        this.groupNameChange = this.groupNameChange.bind(this)
        this.submitButton = this.submitButton.bind(this)
    }

    groupNameChange(event) {
        this.setState({groupName: event.target.value});
    }

    submitButton = (event) => {
        let dataObject = {
            title: this.state.groupName
        }
        
        axios.defaults.headers.post['Content-Type'] = 'application/json';
        axios.post(
            "https://localhost:44329/Group/CreateGroup",  
            JSON.stringify(dataObject)
        );
        event.preventDefault();
    }

    render (){  
        return (
            <div className= "container-fluid w-50 mx-auto pt-4"> 
            <h3 className="text-center"> Create new group</h3>
                <form onSubmit={this.submitButton}>
                    <div className="form-group">
                        <label htmlFor="groupName">Group Name:</label>
                        <input id="groupName" value={this.state.groupName} onChange={this.groupNameChange} className="form-control" placeholder="Enter group name" ></input>
                    </div>
                    <div className="text-center mb-2">
                        <button className="btn btn-success" type="submit" onClick={this.submitButton}>Submit </button>
                    </div>
                </form>
            </div>
        );
    }
}
export default CreateGroupForm;


