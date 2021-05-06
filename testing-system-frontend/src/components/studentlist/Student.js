import React from 'react'

class Student extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            id: this.props.id,
            firstName: this.props.firstName,
            lastName: this.props.lastName,
            checked: false,
        }

        this.checkChange = this.checkChange.bind(this);
    }

    checkChange() {
        let isChecked = this.state.checked ? false : true;
        this.setState({checked: isChecked})
        // Inform somebody that state is changed
    }

    render() {
        return (
            <div className="container">
                <div className="row p-1">
                    <div className="col-sm text-center">
                        { this.state.id}
                    </div>
                    <div className="col-sm text-center">
                        { this.state.firstName}
                    </div>
                    <div className="col-sm text-center">
                        { this.state.lastName}
                    </div>
                    <div className="col-sm text-center">
                        <input type="checkbox" value={this.state.checked} onChange={this.checkChange}></input> 
                    </div>
                </div>
            </div>
        )
    }

}

export default Student