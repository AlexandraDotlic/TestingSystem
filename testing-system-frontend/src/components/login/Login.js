import React from 'react'
import axios from 'axios'

class Login extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: "",
            rememberMe: false
        }
        this.emailChanged = this.emailChanged.bind(this);
        this.passwordChanged = this.passwordChanged.bind(this);
        this.rememberMeChanged = this.rememberMeChanged.bind(this);
        this.submit = this.submit.bind(this);
    }

    emailChanged(event) {
        this.setState({email: event.target.value});
    }

    passwordChanged(event) {
        this.setState({password: event.target.value});
    }

    rememberMeChanged(event) {
        if(this.state.rememberMe === false) {
            this.setState({rememberMe: true});
        }
        else {
            this.setState({rememberMe: false});
        }
    }

    submit() {
        let dataObject = {
            email: this.state.email,
            password: this.state.password,
            remember: this.state.rememberMe
        };
        debugger;
        
        axios.defaults.headers.post['Content-Type'] = 'application/json';
        axios.post(
            "https://localhost:44329/Account/Register", 
            dataObject
        ).then(response => {
            debugger;
            console.log("User successfuly registered.");
        }
        ).catch(error => {
            debugger;
            console.log(error);
        });

    }

    render() {
        return (
            <div className="w-50 mx-auto pt-4">
                <h3 className="text-center"> Login </h3>
                <form onSubmit={this.submit}>
                    <div className="form-group">
                        <label htmlFor="email"> E-mail address: </label>
                        <input id="email" value={this.state.email} onChange={this.emailChanged} type="email" className="form-control" placeholder="Enter your e-mail address"></input>
                    </div>

                    <div className="form-group">
                        <label htmlFor="password"> Password: </label>
                        <input id="password" value={this.state.password} onChange={this.passwordChanged} type="password" className="form-control"></input>
                    </div>

                    <div className="form-check">
                        <input id="remember" type="checkbox" className="form-check-input" value={this.state.rememberMe} onChange={this.rememberMeChanged}></input>
                        <label className="form-check-label" htmlFor="remember"> Remember me</label>
                    </div>

                    <div className="text-center mb-2">
                        <button type="submit" className="btn btn-success">Submit</button>
                    </div>
                </form>
            </div>
        );
    }
}

export default Login;