import Header from './template/header.js'
import CreateQuestion from './components/createquestion/CreateQuestion'
import CreateTestForm from './components/CreateTestForm'
import CreateGroupForm from './components/CreateGroupForm'
import QuestionList from './components/questionlist/QuestionList.js'

function App() {
  return (
    <div className="App">
      <Header></Header>
      {/* <CreateTestForm></CreateTestForm>
      <CreateQuestion></CreateQuestion>
      <CreateGroupForm /> */}
      <QuestionList></QuestionList>
    </div>
  );
}

export default App;