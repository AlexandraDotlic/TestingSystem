import React from 'react'
import axios from 'axios'

class Register extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            firstName: "",
            lastName: "",
            email: "",
            password: "",
            type: 2
        }
        this.firstNameChanged = this.firstNameChanged.bind(this);
        this.lastNameChanged = this.lastNameChanged.bind(this);
        this.emailChanged = this.emailChanged.bind(this);
        this.passwordChanged = this.passwordChanged.bind(this);
        this.typeChanged = this.typeChanged.bind(this);
        this.submit = this.submit.bind(this);
    }

    firstNameChanged(event) {
        this.setState({firstName: event.target.value});
    }

    lastNameChanged(event) {
        this.setState({lastName: event.target.value});
    }

    emailChanged(event) {
        this.setState({email: event.target.value});
    }

    passwordChanged(event) {
        this.setState({password: event.target.value});
    }

    typeChanged(event) {
        if(event.target.value === "student") {
            this.setState({type: 2});
        }
        else {
            this.setState({type: 1});
        }
    }

    submit(event) {
        event.preventDefault();
        let dataPayload = {
            firstName: this.state.firstName,
            lastName: this.state.lastName,
            email: this.state.email,
            password: this.state.password,
            userRoleType: this.state.type
        }
        
        axios({
            method: 'post',
            url: "https://localhost:44329/Account/Register", 
            data: dataPayload
        }).then(() => {
            console.log("User successfuly registered.");
            axios({
                method: 'post',
                url: "https://localhost:44329/Mail/SendWelcomeMail", 
                data: {
                    toEmail: this.state.email,
                    username: this.state.email
                }
            })
            .then(() => {
                this.props.callback();
            })
            .catch(error => {
                window.alert(error);
            });
        }
        ).catch(error => {
            window.alert(error);
        });
        
    }

    render() {
        return (
            <div className="w-50 mx-auto pt-4">
                <h3 className="text-center"> Register new user </h3>
                <form onSubmit={this.submit} onReset={() => this.props.cancelCallback()}>
                    <div className="form-group">
                        <label htmlFor="firstName"> First name: </label>
                        <input id="firstName" value={this.state.firstName} onChange={this.firstNameChanged} className="form-control" placeholder="Enter your first name" required></input>
                    </div>

                    <div className="form-group">
                        <label htmlFor="lastName"> Last name: </label>
                        <input id="lastName" value={this.state.lastName} onChange={this.lastNameChanged} className="form-control" placeholder="Enter your last name" required></input>
                    </div>

                    <div className="form-group">
                        <label htmlFor="email"> E-mail address: </label>
                        <input id="email" value={this.state.email} onChange={this.emailChanged} type="email" className="form-control" placeholder="Enter your e-mail address" required></input>
                    </div>

                    <div className="form-group">
                        <label htmlFor="password"> Password: </label>
                        <input id="password" value={this.state.password} onChange={this.passwordChanged} type="password" className="form-control" pattern="(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^*-]).{8,}$" title="Password must contain at least one uppercase, one lowercase, one special, one number, minimum 8 chars" required></input>
                    </div>

                    <div className="form-group">
                        <label htmlFor="questionType">Select user type:</label>
                        <select className="form-control" onChange={this.typeChanged} id="userType">
                            <option value="student">Student</option>
                            <option value="examiner">Examiner</option>
                        </select>
                    </div>

                    <div className="form-group text-center mb-2">
                        <button type="submit" className="btn btn-success">Submit</button>
                    </div>
                    <div className="form-group text-center mb-2">
                        <button type="reset" className="btn btn-warning">Discard</button>
                    </div>
                </form>
            </div>
        );
    }
}

export default Register;