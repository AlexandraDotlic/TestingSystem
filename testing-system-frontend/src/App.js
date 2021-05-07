import Header from './template/header.js'
import Home from './components/home/Home.js'
import Register from './components/register/Register'

function App() {
  return (
    <div className="App">
      <Header></Header>
      {/* <Home></Home> */}
      <Register></Register>
    </div>
  );
}

export default App;