import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';

// Bootstrap and it's dependencies
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/js/bootstrap.js';

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);