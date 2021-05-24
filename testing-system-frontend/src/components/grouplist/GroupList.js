import axios from 'axios';
import React from 'react'
import Group from './Group'
import StudentList from '../studentlist/StudentList'

class GroupList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            examinerId: null, //temp
            allGroups: [],
            listStudentsGroupId: null
        }
        this.listAllStudentsButton = this.listAllStudentsButton.bind(this);
        this.goBackHome = this.goBackHome.bind(this);
    }

    componentDidMount() {
        let token = sessionStorage.getItem('userToken');

        axios({
            method: 'get',
            url: 'https://localhost:44329/Examiner/GetAllGroupsForExaminer',
            headers: {
                'Authorization': token
            }
        }).then(response => {
            this.setState({allGroups: response.data.groups});
        }).catch(() => {
            window.alert("Failed to get all tests for examiner");
        });
    }

    listAllStudentsButton = (event) => {
        let studentListGroupId = parseInt(event.target.value);
        this.setState({listStudentsGroupId: studentListGroupId});
        //mozda treba da se doda jos nesto sto treba da se salje listi studenata!
    }

    goBackHome() {
        this.props.backHomeCallback();
    }

    render() {
        let groups = this.state.allGroups.map(group => {
            return <Group key={group.id} groupId={group.id} groupTitle={group.title} groupExaminerId={group.examinerId} listAllStudentsCallback={this.listAllStudentsButton} editCallback={this.editButton}></Group>
        })

        if(this.state.listStudentsGroupId == null) {
            return (
                <div className="w-50 mx-auto pt-3">
                    <h5> Groups for examiner: <em>{this.state.examinerName}</em> </h5>
                    <ul>
                        {groups}
                    </ul>
                </div>
    
            );
        }
        else if(this.state.listStudentsGroupId != null) {
            return (
                <StudentList groupId={this.state.listStudentsGroupId} backHomeCallback={this.goBackHome}></StudentList>
            )
        }
    }
}

export default GroupList;