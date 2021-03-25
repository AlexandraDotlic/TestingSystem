import React from 'react'
import 'bootstrap'

const CreateTestForm = () => {
    return (
        <div class = "container-fluid "> 
            <div class="form-group row">
                <label class="col-6 col-sm-4 col-form-label">Test Title</label>
                <div class="col-6 col-sm-4">
                    <input class="form-control" type="text" id="testName"></input>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-6 col-sm-4 col-form-label">Start Date</label>
                <div class="col-6 col-sm-4">
                    <input class="form-control" type="date" id="startDate"></input>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-6 col-sm-4 col-form-label">Start Time</label>
                <div class="col-6 col-sm-4 ">
                    <input class="form-control" type="time" id="startTime"></input>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-6 col-sm-4 col-form-label">End Date</label>
                <div class="col-6 col-sm-4">
                    <input class="form-control" type="date" id="endDate"></input>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-6 col-sm-4 col-form-label">End Time</label>
                <div class="col-6 col-sm-4 ">
                    <input class="form-control" type="time" id="endTime"></input>
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


export default CreateTestForm;
