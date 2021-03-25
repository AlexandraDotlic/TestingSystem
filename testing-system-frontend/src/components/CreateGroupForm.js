import React from 'react'
import 'bootstrap'

const CreateGroupForm = () => {
    return (
        <div class = "container-fluid "> 
            <div class="form-group row">
                <label class="col-6 col-sm-4 col-form-label">Group Title</label>
                <div class="col-6 col-sm-4">
                    <input class="form-control" type="text" id="groupName"></input>
                </div>
            </div>
            <div class="form-group row">
                <div class="col-6 col-sm-4">
                    <button class="btn btn-outline-dark" type="button">Save</button>
                </div>
            </div>
        </div>
    );
}


export default CreateGroupForm;
