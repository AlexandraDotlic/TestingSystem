import React from 'react'

class TableHeader extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-xl text-center">
                        Title: 
                    </div>
                    <div className="col-lg text-center">
                        Date: 
                    </div>
                    <div className="col-lg text-center">
                        Time: 
                    </div>
                    <div className="col-sm text-center">
                        Pt's: 
                    </div>
                    <div className="col-sm text-center">
                        Options:
                    </div>
                </div>
            </div>
        );
    }
}

export default TableHeader