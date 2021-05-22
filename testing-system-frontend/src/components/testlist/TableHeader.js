import React from 'react'

class TableHeader extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="container">
                <div className="row p-3">
                    <div className="col-lg text-center">
                        Test Name: 
                    </div>
                    <div className="col-sm text-center">
                        Date: 
                    </div>
                    <div className="col-sm text-center">
                        Time: 
                    </div>
                    <div className="col-sm text-center">
                        Points: 
                    </div>
                    <div className="col-lg text-center">
                        Change Status: 
                    </div>
                    <div className="col-lg text-center">
                        Questions: 
                    </div>
                    <div className="col-lg text-center"> 
                        Edit:
                    </div>
                </div>
            </div>
        );
    }
}

export default TableHeader