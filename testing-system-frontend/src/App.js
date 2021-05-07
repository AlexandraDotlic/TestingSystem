import Header from './template/header.js'
import StudentList from './components/studentlist/StudentList'
import Home from './components/home/Home.js'

function App() {
  return (
    <div className="App">
      <Header></Header>
      {/* <Home></Home> */}
      <StudentList></StudentList>
    </div>
  );
}

export default App;