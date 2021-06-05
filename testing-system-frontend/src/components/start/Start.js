import React from 'react';
import Login from '../login/Login.js'
import Register from '../register/Register.js'
import Home from '../home/Home.js'
import Header from '../../template/header'
import TakeTest from '../taketest/TakeTest'

class Start extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            option: null,
            name: null
        };
        this.loginSuccessCallback = this.loginSuccessCallback.bind(this);
        this.loginFailureCallback = this.loginFailureCallback.bind(this);
        this.registerCallback = this.registerCallback.bind(this);
        this.registeredSuccessfully = this.registeredSuccessfully.bind(this);
        this.registeredCancelled = this.registeredCancelled.bind(this);
        this.logout = this.logout.bind(this);
    }

    componentDidMount() {
        if(sessionStorage.getItem("userName") !== undefined) {
            this.loginSuccessCallback();
        }
        
    }

    registeredSuccessfully() {
        window.alert("User registered successfuly.");
        this.setState({option: null});
    }

    registeredCancelled() {
        this.setState({option: null});
    }

    logout() {
        window.alert("User logged out.");
        sessionStorage.clear();
        this.setState({option: null, name: null});
    }

    loginSuccessCallback() {
        const nameStorage = sessionStorage.getItem('userName');
        if(sessionStorage.getItem('userType') === 'Examiner') {
            this.setState({option: "loginsuccessexaminer", name: nameStorage});
        }
        else if(sessionStorage.getItem('userType') === 'Student') {
            this.setState({option: "loginsuccessstudent", name: nameStorage});
        }
    }

    loginFailureCallback() {
        window.alert("Login failed. Please enter your credentials once again.");
    }

    registerCallback() {
        this.setState({option: "register"})
    }

    render() {
        if(this.state.option == null) {
            return (
                
                <div>
                    <Header></Header>
                    <Login loginSuccess={this.loginSuccessCallback} loginFailure={this.loginFailureCallback}></Login>
                    <hr></hr>
                    <div className="w-50 mx-auto pt-5 text-center">
                        If you don't have account:  
                        <button className="btn btn-success" style={{marginLeft: 7}} onClick={this.registerCallback}> Register </button> 
                    </div>
                </div> 
            )
        }
        else if(this.state.option === "loginsuccessexaminer") {
            return (
                <div >
                    <Header username={this.state.name} logoutCallback={this.logout}></Header>
                    <Home></Home>
                </div>
            )
        }
        else if(this.state.option === "loginsuccessstudent") {
            return (
                <div>
                <Header username={this.state.name} logoutCallback={this.logout}></Header>
                <TakeTest></TakeTest>
            </div>
            )
        }
        else if(this.state.option === "register") {
            return (
            <div>
                <Header></Header>
                <Register callback={this.registeredSuccessfully} cancelCallback={this.registeredCancelled}></Register>
            </div>
            )
        }
    }
}

export default Start