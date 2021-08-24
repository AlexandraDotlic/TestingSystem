import React from 'react'

class Group extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            groupId: this.props.groupId,
            groupTitle: this.props.groupTitle
        }
    }

    render() {
        return (
            <div className="container">
                <div className="row p-1">
                    <div className="col-lg text-center">
                        {this.state.groupTitle}
                    </div>
                    <div className="col-sm text-center">
                        <button className="btn btn-info" onClick={this.props.listAllStudentsCallback} value={this.state.groupId} name={this.state.groupTitle}> Students </button>
                    </div>
                </div>
            </div>
        );
    }
}

export default Group