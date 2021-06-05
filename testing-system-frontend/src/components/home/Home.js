import React from 'react';
import './contentBoxes.css';
import CreateTestForm from '../CreateTestForm'
import CreateGroupForm from '../CreateGroupForm'
import TestList from '../testlist/TestList'
import GroupList from '../grouplist/GroupList'
import Statistics from '../statistics/Statistics';

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            clicked: null
        }
        this.goBackHome = this.goBackHome.bind(this);
    }

    clicked(value) {
        this.setState({clicked: value});
    }

    goBackHome() {
        this.setState({clicked:null});
    }

    render() {
        if(this.state.clicked === null) {
            return (
                <div className="w-50 mx-auto pt-5">
                    <div className="container">
                        <div className="row p-1">
                            <button onClick={() => this.clicked("newtest")} className="button" style={{backgroundColor: '#5027c2'}}><span>Create new test</span></button>
                            <button onClick={() => this.clicked("newgroup")} className="button" style={{backgroundColor: '#b0b0f5'}}><span>Create new group</span></button>
                            <button className="button" style={{backgroundColor: '#5027c2'}}><span> Connect group-test </span></button>
                        </div>
                    </div>
                    <div className="container">
                        <div className="row p-1">
                            <button onClick={() => this.clicked("listtest")} className="button" style={{backgroundColor: '#b0b0f5'}}><span>List all tests </span></button>
                            <button onClick={() => this.clicked("grouplist")} className="button" style={{backgroundColor: '#5027c2'}}><span>List all groups </span></button>
                            <button onClick={() => this.clicked("stats")} className="button" style={{backgroundColor: '#b0b0f5'}}><span>Statistics</span></button>
                        </div>
                    </div>
                </div>
            )
        }
        else if(this.state.clicked === "newtest") {
            return (
                <CreateTestForm cancelCallback={this.goBackHome}></CreateTestForm>
            )
        }
        else if(this.state.clicked === "newgroup") {
            return (
                <CreateGroupForm cancelCallback={this.goBackHome}></CreateGroupForm>
            )
        }
        else if(this.state.clicked === "listtest") {
            return (
                <TestList cancelCallback={this.goBackHome}></TestList>
            )
        }
        else if(this.state.clicked === "grouplist") {
            return (
                <GroupList backHomeCallback={this.goBackHome} cancelCallback={this.goBackHome}></GroupList>
            )
        }
        else if(this.state.clicked === "stats") {
            return (
                <Statistics cancelCallback={this.goBackHome}></Statistics>
            )
        }
        else {
            return ( <div> Error </div> )
        }

    }
}

export default Home