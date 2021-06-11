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
        // Prepare token 
        let token = sessionStorage.getItem('userToken');

        axios({
            method: 'post',
            url: 'https://localhost:44329/Group/CreateGroup',
            headers: {
                'Authorization': token
            },
            data: {
              title: this.state.groupName
            }
        });
    }

    render (){  
        return (
            <div className= "container-fluid w-50 mx-auto pt-4"> 
            <h3 className="text-center"> Create new group</h3>
                <form onSubmit={this.submitButton} onReset={() => this.props.cancelCallback()}>
                    <div className="form-group">
                        <label htmlFor="groupName">Group Name:</label>
                        <input id="groupName" value={this.state.groupName} onChange={this.groupNameChange} className="form-control" placeholder="Enter group name" ></input>
                    </div>
                    <div className="form-group text-center mb-2">
                        <button className="btn btn-success" type="submit">Submit </button>
                    </div>
                    <div className= "form-group text-center mb-2">
                        <button className="btn btn-warning" type="reset">Discard </button>
                    </div>
                </form>
            </div>
        );
    }
}
export default CreateGroupForm;


