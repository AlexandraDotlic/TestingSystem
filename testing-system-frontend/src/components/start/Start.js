import React from 'react';
import Login from '../login/Login.js'
import Register from '../register/Register.js'
import Home from '../home/Home.js'

class Start extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            option: null
        };
        this.loginSuccessCallback = this.loginSuccessCallback.bind(this);
        this.loginFailureCallback = this.loginFailureCallback.bind(this);
    }

    loginSuccessCallback() {
        // This is place to check if student or professor checked in
        // This is place to update name in Header component
        window.alert("Uspeh");
        this.setState({option: "loginsuccess"})
    }

    loginFailureCallback() {
        window.alert("Login failed. Please enter your credentials once again.");
    }

    render() {
        if(this.state.option == null) {
            return (
                <div>
                    <Login loginSuccess={this.loginSuccessCallback} loginFailure={this.loginFailureCallback}></Login>
                    <hr></hr>
                    <div className="w-50 mx-auto pt-5 text-center">
                        If you don't have account:  
                        <button className="btn btn-success" style={{marginLeft: 7}} onClick={this.registerCallback}> Register </button> 
                    </div>
                </div> 
            )
        }
        else if(this.state.option === "loginsuccess") {
            return (
                <Home></Home>
            )
        }
    }
}

export default Start