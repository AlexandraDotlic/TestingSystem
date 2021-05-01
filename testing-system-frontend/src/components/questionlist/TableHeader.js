import React from 'react'

class TableHeader extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="container">
                <div className="row p-1">
                    <div className="col-lg">
                        Question text: 
                    </div>
                    <div className="col-sm">
                        Edit: 
                    </div>
                    <div className="col-sm">
                        Delete: 
                    </div>
                </div>
            </div>
        );
    }
}

export default TableHeader