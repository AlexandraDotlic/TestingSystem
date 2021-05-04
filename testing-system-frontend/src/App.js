import Header from './template/header.js'
import CreateQuestion from './components/createquestion/CreateQuestion'
import CreateTestForm from './components/CreateTestForm'
import CreateGroupForm from './components/CreateGroupForm'
import QuestionList from './components/questionlist/QuestionList.js'
import Register from './components/register/Register.js'
import Login from './components/login/Login.js'
import StudentList from './components/studentlist/StudentList.js'

function App() {
  return (
    <div className="App">
      <Header></Header>
      {/* 
      <CreateTestForm></CreateTestForm>
      <CreateQuestion></CreateQuestion>
      <CreateGroupForm />
      <Register></Register>
      <Login></Login> 
      <QuestionList></QuestionList>
      */}
      <StudentList></StudentList>
    </div>
  );
}

export default App;