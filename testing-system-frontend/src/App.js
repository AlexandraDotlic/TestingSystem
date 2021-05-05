import Header from './template/header.js'
import CreateQuestion from './components/createquestion/CreateQuestion'
import CreateTestForm from './components/CreateTestForm'
import CreateGroupForm from './components/CreateGroupForm'
import TestList from './components/testlist/TestList.js'
import Register from './components/register/Register.js'
import Login from './components/login/Login.js'

function App() {
  return (
    <div className="App">
      <Header></Header>
      {/* 
      <CreateTestForm></CreateTestForm>
      <CreateQuestion></CreateQuestion>
      <CreateGroupForm />
      <Register></Register>
      <Login></Login> */}
      <TestList></TestList>
    </div>
  );
}

export default App;