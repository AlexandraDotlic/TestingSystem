import React from 'react'

class TableHeader extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="container text-center">
                <div className="row p-1">
                    <div className="col-lg text-center">
                        Question text: 
                    </div>
                    <div className="col-sm text-center">
                        Edit: 
                    </div>
                    <div className="col-sm text-center">
                        Delete: 
                    </div>
                </div>
            </div>
        );
    }
}

export default TableHeader