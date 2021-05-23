import React from 'react'
import './header.css'

class Header extends React.Component {
    constructor(props) {
        super(props);
        const res = this.props.name;
        debugger;
        if(this.props.name) {
            this.state = { name: this.props.name };
        }
        else {
            this.state = {name: "Welcome" };
        }
    }


    render() {
        let menu = (
            <div className="dropdown-menu leftMoved" aria-labelledby="navbarDropdownMenuLink">
                <a className="dropdown-item" href="#">Action</a>
                <a className="dropdown-item" href="#">Another action</a>
                <a className="dropdown-item" href="#">Something else here</a>
            </div>
        );

        if(this.state.name == "Welcome") {
            menu = null;
        }

        return (
            <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
                <a className="navbar-brand" href="#"> Welcome to Testing System</a>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>


                <div className="collapse navbar-collapse" id="navbarNavDropdown">
                    <ul className="navbar-nav ml-auto">
                        <li className="nav-item dropleft">
                            <a className="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                { this.state.name }
                            </a>
                            { menu }
                        </li>
                    </ul>
                </div>

            </nav>
        );
    }
}
export default Header;